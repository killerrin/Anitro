﻿using AnimeTrackingServiceWrapper;
using AnimeTrackingServiceWrapper.Implementation.HummingbirdV1.Models;
using AnimeTrackingServiceWrapper.UniversalServiceModels;
using AnimeTrackingServiceWrapper.UniversalServiceModels.ActivityFeed;
using Anitro.Models;
using Anitro.Models.Enums;
using Anitro.Models.Page_Parameters;
using Anitro.Pages.Hummingbird;
using Anitro.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Anitro.ViewModels.Hummingbird
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class HummingbirdAnimeLibraryViewModel : AnitroViewModelBase 
    {
        private HummingbirdUser m_user = new HummingbirdUser();
        public HummingbirdUser User
        {
            get { return m_user; }
            set
            {
                if (m_user == value) return;
                m_user = value;
                RaisePropertyChanged(nameof(User));
            }
        }

        /// <summary>
        /// Initializes a new instance of the HummingbirdAnimeLibraryViewModel class.
        /// </summary>
        public HummingbirdAnimeLibraryViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                User = new HummingbirdUser();
                User.UserInfo.Username = "Design Time";
                User.UserInfo.AvatarUrl = new System.Uri("https://static.hummingbird.me/users/avatars/000/007/415/thumb/TyrilCropped1.png?1401236074", System.UriKind.Absolute);
                User.HummingbirdUserInfo.cover_image = "https://static.hummingbird.me/users/cover_images/000/007/415/thumb/Zamma_resiz.jpg?1401237213";

                User.LoginInfo.Username = "Design Time";
                User.LoginInfo.AuthToken = "AuthToken";

                AnimeObject anime = new AnimeObject();
                anime.RomanjiTitle = "Gate: Jieitai Kanochi nite, Kaku Tatakaeri";
                anime.EnglishTitle = "GATE";
                anime.CoverImageUrl = new Uri("https://static.hummingbird.me/anime/poster_images/000/010/085/large/85a5d8cc2972ae422158be7069076be41435868848_full.jpg?1435924413", UriKind.Absolute);
                anime.EpisodeCount = 24;

                // Recent
                User.AnimeLibrary.Recent.Add(anime);
                User.AnimeLibrary.Recent.Add(anime);
                User.AnimeLibrary.Recent.Add(anime);
                User.AnimeLibrary.Recent.Add(anime);
                User.AnimeLibrary.Recent.Add(anime);
                User.AnimeLibrary.Recent.Add(anime);

                // Library
                LibraryObject currentlyWatchinglibraryObject = new LibraryObject();
                currentlyWatchinglibraryObject.Anime = anime;
                currentlyWatchinglibraryObject.EpisodesWatched = 6;
                currentlyWatchinglibraryObject.Section = LibrarySection.CurrentlyWatching;

                LibraryObject completedLibraryObject = new LibraryObject();
                completedLibraryObject.Anime = anime;
                completedLibraryObject.Rating = 3.5;
                completedLibraryObject.Section = LibrarySection.Completed;

                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(currentlyWatchinglibraryObject);
                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);

                // Favourites
                User.AnimeLibrary.Favourites.Add(anime);
                User.AnimeLibrary.Favourites.Add(anime);
                User.AnimeLibrary.Favourites.Add(anime);
                User.AnimeLibrary.Favourites.Add(anime);
                User.AnimeLibrary.Favourites.Add(anime);
                User.AnimeLibrary.Favourites.Add(anime);
            }
            else
            {
                // Code runs "for real"
            }
        }

        public override void Loaded()
        {

        }

        public override void OnNavigatedTo()
        {
            MainViewModel.Instance.CurrentNavigationLocation = NavigationLocation.AnimeLibrary;

            if (User.AnimeLibrary.LibraryCollection.UnfilteredCollection.Count == 0)
                RefreshLibrary();
            if (User.AnimeLibrary.Favourites.Count == 0)
                RefreshFavourites();
        }

        public override void OnNavigatedFrom()
        {
            if (User.LoginInfo.IsUserLoggedIn)
                HummingbirdUser.Save(User);
        }

        public override void ResetViewModel()
        {

        }

        #region LibraryObject Manipulation Commands
        #region Increment Episode Watched Favourites
        public RelayCommand<object> DecrementEpisodeWatchedCommand { get { return new RelayCommand<object>((o) => { IncrementEpisodeWatched((LibraryObject)o, -1); }); } }
        public RelayCommand<object> IncrementEpisodeWatchedCommand { get { return new RelayCommand<object>((o) => { IncrementEpisodeWatched((LibraryObject)o, 1); }); } }

        public void IncrementEpisodeWatched(LibraryObject libraryObject, int modifier)
        {
            Debug.WriteLine("Incrementing Episodes Watched");

            if (modifier > 0)
            {
                if (libraryObject.AreAllEpisodesWatched)
                    return;
            }
            else if (modifier < 0)
            {
                if (libraryObject.AreNoEpisodesWatched)
                    return;
            }

            libraryObject.EpisodesWatched += modifier;

            HummingbirdAnimeDetailsViewModel vm = ViewModelLocator.Instance.vm_HummingbirdAnimeDetailsViewModel;
            vm.LibraryObject = libraryObject;
            vm.UpdateAnime();
        }
        #endregion
        #endregion

        #region Refresh Commands
        #region Clear Recent
        public RelayCommand ClearRecentCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ClearRecent();
                });
            }
        }

        public void ClearRecent()
        {
            Debug.WriteLine("Clear Recent");
            User.AnimeLibrary.Recent.Clear();
            
            if (User.LoginInfo.IsUserLoggedIn)
                HummingbirdUser.Save(User);
        }
        #endregion

        #region Refresh Library
        public RelayCommand RefreshLibraryCommand { get { return new RelayCommand(() => { RefreshLibrary(); }); } }
        public void RefreshLibrary()
        {
            Debug.WriteLine("Refreshing Library");
            Progress<APIProgressReport> m_refreshLibraryProgress = new Progress<APIProgressReport>();
            m_refreshLibraryProgress.ProgressChanged += M_refreshLibraryProgress_ProgressChanged;
            APIServiceCollection.Instance.HummingbirdV1API.AnimeAPI.GetAnimeLibrary(User.UserInfo.Username, LibrarySection.All, m_refreshLibraryProgress);
        }

        private void M_refreshLibraryProgress_ProgressChanged(object sender, APIProgressReport e)
        {
            ProgressService.SetIndicatorAndShow(true, e.Percentage, e.StatusMessage);
            if (e.CurrentAPIResonse == APIResponse.Successful)
            {
                ProgressService.Reset();
                Debug.WriteLine("Anime Library: " + e.ToString());

                List<LibraryObject> libraryObjects = e.Parameter.Converted as List<LibraryObject>;
                var groupedQuery = from lO in libraryObjects
                                   group lO by lO.Section into grouped
                                   orderby grouped.Key
                                   select grouped;

                List<LibraryObject> groupedLibraryObjects = new List<LibraryObject>();
                foreach (var group in groupedQuery)
                {
                    foreach (var lO in group)
                    {
                        groupedLibraryObjects.Add(lO);
                    }
                }

                User.AnimeLibrary.LibraryCollection.UnfilteredCollection = new ObservableCollection<LibraryObject>(groupedLibraryObjects);

                if (User.LoginInfo.IsUserLoggedIn)
                    HummingbirdUser.Save(User);

            }
            else if (APIResponseHelpers.IsAPIResponseFailed(e.CurrentAPIResonse))
            {
                ProgressService.Reset();
                Debug.WriteLine("Anime Library: " + e.ToString());
            }
        }
        #endregion

        #region Refresh Favourites
        public RelayCommand RefreshFavouritesCommand { get { return new RelayCommand(() => { RefreshFavourites(); }); } }

        public void RefreshFavourites()
        {
            Debug.WriteLine("Refreshing Favourites");
            Progress<APIProgressReport> m_refreshFavouritesProgress = new Progress<APIProgressReport>();
            m_refreshFavouritesProgress.ProgressChanged += M_refreshFavouritesProgress_ProgressChanged;
            APIServiceCollection.Instance.HummingbirdV1API.AnimeAPI.GetAnimeFavourites(User.UserInfo.Username, m_refreshFavouritesProgress);
        }

        private void M_refreshFavouritesProgress_ProgressChanged(object sender, APIProgressReport e)
        {
            ProgressService.SetIndicatorAndShow(true, e.Percentage, e.StatusMessage);
            if (e.CurrentAPIResonse == APIResponse.Successful)
            {
                ProgressService.Reset();
                Debug.WriteLine("Anime Favourites: " + e.ToString());

                List<AnimeObject> animeObjects = e.Parameter.Converted as List<AnimeObject>;
                User.AnimeLibrary.Favourites = new ObservableCollection<AnimeObject>(animeObjects);

                if (User.LoginInfo.IsUserLoggedIn)
                    HummingbirdUser.Save(User);
            }
            else if (APIResponseHelpers.IsAPIResponseFailed(e.CurrentAPIResonse))
            {
                ProgressService.Reset();
            }
        }
        #endregion
        #endregion

        #region Navigation Commands
        #region Library Item Clicked
        public RelayCommand<LibraryObject> LibraryItemClickedCommand
        {
            get
            {
                return new RelayCommand<LibraryObject>((libraryObject) =>
                {
                    LibraryItemClicked(libraryObject);
                });
            }
        }

        public void LibraryItemClicked(LibraryObject libraryObject)
        {
            Debug.WriteLine("Library Item Clicked");
            NavigateAnimeDetailsPage(libraryObject.Anime);
        }
        #endregion

        #region Recent Item Clicked
        public RelayCommand<AnimeObject> RecentItemClickedCommand
        {
            get
            {
                return new RelayCommand<AnimeObject>((animeObject) =>
                {
                    RecentItemClicked(animeObject);
                });
            }
        }
        public void RecentItemClicked(AnimeObject animeObject)
        {
            Debug.WriteLine("Recent Item Clicked");
            NavigateAnimeDetailsPage(animeObject);
        }
        #endregion

        #region Favourite Item Clicked
        public RelayCommand<AnimeObject> FavouriteItemClickedCommand
        {
            get
            {
                return new RelayCommand<AnimeObject>((animeObject) =>
                {
                    FavouriteItemClicked(animeObject);
                });
            }
        }
        public void FavouriteItemClicked(AnimeObject animeObject)
        {
            Debug.WriteLine("Favourite Item Clicked");
            NavigateAnimeDetailsPage(animeObject);
        }
        #endregion

        /// <summary>
        /// Gets the Library Object from the Currently Logged In User
        /// </summary>
        /// <param name="animeObject"></param>
        /// <returns></returns>
        public static LibraryObject GetLibraryObject(AnimeObject animeObject)
        {
            Debug.WriteLine("GetLibraryObject");
            HummingbirdUser loggedInUser = MainViewModel.Instance.HummingbirdUser_LoggedIn;
            if (loggedInUser == null)
                return new LibraryObject(animeObject);

            LibraryObject libraryObject = loggedInUser.AnimeLibrary.FindInLibrary(animeObject.ID);

            if (libraryObject == null)
                libraryObject = new LibraryObject(animeObject);
            return libraryObject;
        }

        public void NavigateAnimeDetailsPage(AnimeObject animeObject)
        {
            Debug.WriteLine("NavigateAnimeDetailsPage");
            LibraryObject libraryObject = GetLibraryObject(animeObject);

            HummingbirdAnimeDetailsParameter parameter = new HummingbirdAnimeDetailsParameter();
            parameter.LibraryObject = libraryObject;

            Debug.WriteLine("Navigating");
            NavigationService.Navigate(typeof(HummingbirdAnimeDetailsPage), parameter);

            // Add this Anime to the Recent
            User?.AnimeLibrary.AddToRecent(animeObject);
        }
        #endregion
    }
}