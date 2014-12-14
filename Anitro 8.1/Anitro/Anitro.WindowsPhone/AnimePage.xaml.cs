﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Anitro.APIs;
using Anitro.Data_Structures;
using Anitro.Data_Structures.API_Classes;
using Anitro.Data_Structures.Structures;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Anitro.APIs.Events;
using Anitro.Data_Structures.Enumerators;
using Windows.UI.StartScreen;
using Anitro.APIs.Hummingbird;
using Anitro.Converters;
using Microsoft.Advertising.Mobile.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Anitro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnimePage : Page
    {
        public static AnimePage me;

        public static Windows.UI.Core.CoreDispatcher dispatcher;
        private bool contentLoaded = false;
        
        private bool savingAndUpdatingAnime = false;
        private bool objectChanged = false;
        private bool animeRemoved = false;

        private string originalRatingText = "";

        AnimePageParameter pageParameter;
        LibraryObject libraryObject;

        public AnimePage()
        {
            this.InitializeComponent();

            Loaded += AnimePage_Loaded;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("OnNavigatedTo(): AnimePage");
            base.OnNavigatedTo(e);

            // Subscribe to the back button event as to not close the page
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            // Save the Parameters
            pageParameter = e.Parameter as AnimePageParameter;

            // Set the page loaded event
            Loaded += PageLoaded;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // Start the process of setting the content
            Debug.WriteLine("Setting Content");

            // Save the Dispatcher
            dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;

            // Set'ze CONTENT!
            SetContent(pageParameter);
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            bool loop = true;
            if (Consts.LoggedInUser.IsLoggedIn)
            {
                if (objectChanged)
                {
                    Debug.WriteLine("Posting Update");
                    if (animeRemoved) RemoveFromLibrary();
                    else UpdateLibrary();
                    Consts.forceLibrarySave = true;

                    //await APIv1.Post.LibraryUpdate(libraryObject, false);
                    //Debug.WriteLine("Library Updated");
                }
                else Consts.LoggedInUser.animeLibrary.savingOrUpdatingLibrary = false;

                while (loop)
                {
                    if (APIs.StorageTools.isSavingComplete &&
                        !savingAndUpdatingAnime &&
                        !Consts.LoggedInUser.animeLibrary.savingOrUpdatingLibrary)
                    {
                        loop = false;
                    }
                }
            }
            else
            {
                while (loop)
                {
                    if (APIs.StorageTools.isSavingComplete &&
                        !savingAndUpdatingAnime)
                    {
                        loop = false;
                    }
                }
            }

            e.Handled = true;
            GoBack();
        }

        private void GoBack()
        {
            Debug.WriteLine("Going Back");
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.GoBack();
        }

        private async void BindAndDisplay(AnimePageParameter _parameter)
        {
            Debug.WriteLine("BindAndDisplay(): Begun Bind");

            //-- First, Add the Library Object to our Libraries Recent
            if (Consts.LoggedInUser.IsLoggedIn)
            {
                Consts.LoggedInUser.animeLibrary.AddToRecent(libraryObject, false);
                await Consts.LoggedInUser.Save();
            }

            //-- Next, Bind the Data
            // Set the Background
            try
            {
                Debug.WriteLine("Background Image");
                BackgroundImage.Source = new BitmapImage(new Uri(libraryObject.anime.cover_image, UriKind.Absolute));
            }
            catch (Exception) { }

            // Set the AppBar
            Debug.WriteLine("AppBar: Favourites");
            if (Consts.LoggedInUser.animeLibrary.DoesExistInLibrary(LibrarySelection.Favourites, libraryObject))
            {

                favouriteButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed; // Set this to Collapsed
                unfavouriteButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed; // Set this this to Visible
            }
            else
            {
                favouriteButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed; // Set this to Visible
                unfavouriteButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed; // Set this this to Collapsed
            }

            // Check if it is pinned or not
            if (SecondaryTileHelper.DoesTileExist(libraryObject.anime.slug))
            {
                pinButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                unpinButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                pinButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
                unpinButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            // Set the TextBlocks
            #region TextBlocks
            try
            {
                Debug.WriteLine("Anime Title");
                animeTitle.Text = libraryObject.anime.title;
            }
            catch (Exception) { animeTitle.Text = ""; }

            try
            {
                Debug.WriteLine("Alternate Title");
                animeSecondaryHeader.Text = libraryObject.anime.alternate_title;
            }
            catch (Exception) { animeSecondaryHeader.Text = ""; };

            try
            {
                string genreInfo = "";
                for (int i = 0; i < libraryObject.anime.genres.Count; i++)
                {
                    try
                    {
                        Debug.WriteLine("Genre");
                        genreInfo += libraryObject.anime.genres[i].name;
                        genreInfo += ", ";
                    }
                    catch (Exception) { }
                }

                Debug.WriteLine("GenreTextBlock");
                if (genreInfo[genreInfo.Length - 2] == ',') genreInfo = genreInfo.Substring(0, genreInfo.Length - 2);
                animeGenres.Text = genreInfo;
                animeGenres.TextWrapping = TextWrapping.Wrap;
                animeGenresBar.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            }
            catch (Exception) { animeGenres.Text = ""; }

            #region Stats Bar
            try
            {
                Debug.WriteLine("Episode Count");
                if (libraryObject.anime.episode_count == "0") { animeEpisodeCount.Text = "Episodes: " + "?"; }
                else { animeEpisodeCount.Text = "Episodes: " + libraryObject.anime.episode_count; }
                animeEpisodeCount.TextWrapping = TextWrapping.Wrap;
            }
            catch (Exception) { }

            try
            {
                Debug.WriteLine("Anime Status");
                animeStatus.Text = libraryObject.anime.status;
                animeStatus.TextWrapping = TextWrapping.Wrap;
            }
            catch (Exception) { }

            try
            {
                Debug.WriteLine("Show Type");
                animeShowType.Text = libraryObject.anime.show_type;
                animeShowType.TextWrapping = TextWrapping.Wrap;
            }
            catch (Exception) { }

            animeStatsBar.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            #endregion

            try
            {
                Debug.WriteLine("Synopsis");
                animeSynopsis.Text = libraryObject.anime.synopsis;
                animeSynopsis.TextWrapping = TextWrapping.Wrap;
            }
            catch (Exception) { }

            /// 
            /// Library Set
            ///

            if (!(string.IsNullOrEmpty(libraryObject.last_watched) || string.IsNullOrWhiteSpace(libraryObject.last_watched)))
            {
                Debug.WriteLine("AnimeLastWatched");

                string animeLastWatched = libraryObject.last_watched.Substring(0, libraryObject.last_watched.Length - 1);
                DateTime dateTimeLastWatched = DateTime.Parse(animeLastWatched); // string[] last_watchedSplit = animeLastWatched.Split('T');

                libraryLastWatched.Text = "last watched: " + RelativeDateTimeConverter.CalculateConversion(dateTimeLastWatched); // last_watchedSplit[0] + " at " + last_watchedSplit[1];
            }
            else
            {
                libraryLastWatched.Text = "last watched: never";
            }

            Debug.WriteLine("Rating");
            if (!(string.IsNullOrEmpty(libraryObject.rating.value)))
            {
                libraryRating.Text = libraryObject.rating.value + "/5";
            }
            else
            {
                libraryObject.rating.valueAsDouble = 0.0;
                libraryRating.Text = libraryObject.rating.value + "/5";
            }
            originalRatingText = libraryObject.rating.value;
            

            Debug.WriteLine("ListPicker");
            switch (libraryObject.status)
            {
                case "currently-watching":
                    LibraryPicker.SelectedIndex = 1;
                    break;
                case "plan-to-watch":
                    LibraryPicker.SelectedIndex = 2;
                    break;
                case "completed":
                    LibraryPicker.SelectedIndex = 3;
                    break;
                case "on-hold":
                    LibraryPicker.SelectedIndex = 4;
                    break;
                case "dropped":
                    LibraryPicker.SelectedIndex = 5;
                    break;
                case "":
                case "favourites":
                default:
                    LibraryPicker.SelectedIndex = 0;
                    break;
            }


            Debug.WriteLine("Episodes Watched: wCount");
            int wCount = Convert.ToInt32(libraryObject.episodes_watched);

            Debug.WriteLine("Episodes Watched: epCount");
            if (libraryObject.anime.episode_count == "?")
            {
                libraryEpisodesWatched.Text = wCount + "/" + "?";
            }
            else
            {
                int epCount = Convert.ToInt32(libraryObject.anime.episode_count);
                libraryEpisodesWatched.Text = wCount + "/" + epCount;
            }


            Debug.WriteLine("Rewatched Times");
            int rewatchedTimes = Convert.ToInt32(libraryObject.rewatched_times);
            libraryRewatchedTimes.Text = libraryObject.rewatched_times.ToString();

            Debug.WriteLine("Private");
            libraryPrivate.IsOn = libraryObject.@private;

            Debug.WriteLine("Notes");
            if (libraryObject.notes == null) { libraryObject.notes = ""; }
            notesTextBox.Text = libraryObject.notes.ToString();
            #endregion


            if (!Consts.LoggedInUser.IsLoggedIn)
            {
                Debug.WriteLine("User not logged in, locking pivot");
                pagePivot.SelectedIndex = 0;
                pagePivot.IsLocked = true;
            }
            else
            {
                // Unlock the page
                pagePivot.IsLocked = false;
            }


            // Phew, we made it. Lets turn off the progress bar now; We're Done!
            XamlControlHelper.ChangeProgressIndicator(ApplicationProgressBar, false);

            // Reenable the ability to leave
            savingAndUpdatingAnime = false;
            contentLoaded = true;

            Debug.WriteLine("BindAndDisplay(): Bind Completed!");
        }

        

        #region Loading
        private async void SetContent(AnimePageParameter _pageParameter)
        {
            Debug.WriteLine("SetContent(): Begun Bind");

            // Disable the ability to leave
            savingAndUpdatingAnime = true;

            // First, lets enable the Progress Bar, cause this might take awhile.
            XamlControlHelper.ChangeProgressIndicator(ApplicationProgressBar, true);
            
            // Lock the page
            pagePivot.SelectedIndex = 0;
            pagePivot.IsLocked = true;

            // Check if we are logged in, If we aren't, Login.
            if (!Consts.LoggedInUser.IsLoggedIn)
                Consts.LoggedInUser = await Anitro.Data_Structures.User.Load();

            // With that out of the way, Check our connection to the internet and go into each's respective method call
            if (Consts.IsConnectedToInternet()) { ConnectedToInternet(_pageParameter); }
            else { NotConnectedToInternet(_pageParameter); }
        }

        async void ConnectedToInternet(AnimePageParameter _pageParameter)
        {
            Debug.WriteLine("ConnectedToInternet()");
            if (Consts.LoggedInUser.IsLoggedIn) { LoadFromLibrary(_pageParameter); }
            else
            {
                LoadFromServer(_pageParameter);
            }
        }
        async void NotConnectedToInternet(AnimePageParameter _pageParameter)
        {
            Debug.WriteLine("NotConnectedToInternet()");

            // If we still aren't logged in, there isn't much we can do. So notify the user and leave.
            if (!Consts.LoggedInUser.IsLoggedIn)
            {
                await NotConnectedToInternetMessageBox();
                return;
            }

            // If we are logged in though, search for and grab the users library
            LoadFromLibrary(_pageParameter);
        }

        async void LoadFromLibrary(AnimePageParameter _pageParameter)
        {
            Debug.WriteLine("LoadFromLibrary()");
            LibrarySelection location = Consts.LoggedInUser.animeLibrary.FindWhereExistsInLibrary(_pageParameter.slug);

            // Check if its in the library
            if (location == LibrarySelection.None) {
                // Uh-oh, See if we can attempt to save this train
                if (Consts.IsConnectedToInternet()) { LoadFromServer(_pageParameter); }
                else { await NotConnectedToInternetMessageBox(); }
            }
            else
            {
                libraryObject = Consts.LoggedInUser.animeLibrary.GetObjectInLibrary(location, _pageParameter.slug);

                // Now that we have the library objcet, lets see if it has genres on it.
                if (libraryObject.anime.genres.Count == 0 || string.IsNullOrEmpty(libraryObject.anime.genres[0].name))
                {
                    APIv1.APICompletedEventHandler += GetGenres_Completed;

                    this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        APIv1.Get.Anime(libraryObject.anime.slug);
                    });
                }
                else
                {
                    // Bind the Data
                    BindAndDisplay(pageParameter);
                }
            }

        }
        async void LoadFromServer(AnimePageParameter _pageParameter)
        {
            Debug.WriteLine("LoadFromServer()");
            APIv1.APICompletedEventHandler += ServerLoad_Completed;

            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                APIv1.Get.Anime(_pageParameter.slug);
            });
        }
        #endregion

        #region Events
        private void ServerLoad_Completed(object sender, APIs.Events.APICompletedEventArgs e)
        {
            Debug.WriteLine("ServerLoad_Completed()");

            // Grab the Anime Object that the API returns
            Anime animeObj = (sender as Anime);

            libraryObject = new LibraryObject(animeObj);

            // Remove the Event Handler
            APIv1.APICompletedEventHandler -= ServerLoad_Completed;

            // Bind the Data
            BindAndDisplay(pageParameter);
        }
        private async void GetGenres_Completed(object sender, APIs.Events.APICompletedEventArgs e)
        {
            Debug.WriteLine("GetGenres_Completed()");

            try
            {
                // Grab the Anime Object that the API returns
                Anime animeObj = (sender as Anime);

                // Grab the location once more
                LibrarySelection location = Library.GetLibrarySelectionFromStatus(libraryObject.status);

                // Save the Genres
                libraryObject.anime = animeObj;
                Consts.LoggedInUser.animeLibrary.UpdateLibrary(location, libraryObject, false);
                //await Consts.LoggedInUser.Save();
            }
            catch (Exception) { }

            // Remove the Event Handler
            APIv1.APICompletedEventHandler -= GetGenres_Completed;

            // Bind the Data
            BindAndDisplay(pageParameter);
        }
        #endregion
    }
}