﻿using AnimeTrackingServiceWrapper;
using AnimeTrackingServiceWrapper.Implementation.HummingbirdV1.Models;
using AnimeTrackingServiceWrapper.UniversalServiceModels;
using AnimeTrackingServiceWrapper.UniversalServiceModels.ActivityFeed;
using Anitro.Models;
using Anitro.Models.Enums;
using Anitro.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Killerrin_Studios_Toolkit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Media.SpeechRecognition;

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
    public class HummingbirdSearchViewModel : AnitroViewModelBase
    {
        #region Search Results
        private ObservableCollection<AnimeObject> m_searchBoxAutoSuggestions = new ObservableCollection<AnimeObject>();
        public ObservableCollection<AnimeObject> SearchBoxAutoSuggestions
        {
            get { return m_searchBoxAutoSuggestions; }
            set
            {
                if (m_searchBoxAutoSuggestions == value) return;
                m_searchBoxAutoSuggestions = value;
                RaisePropertyChanged(nameof(SearchBoxAutoSuggestions));
            }
        }

        private ObservableCollection<AnimeObject> m_onlineSearchResults = new ObservableCollection<AnimeObject>();
        public ObservableCollection<AnimeObject> OnlineSearchResults
        {
            get { return m_onlineSearchResults; }
            set
            {
                if (m_onlineSearchResults == value) return;
                m_onlineSearchResults = value;
                RaisePropertyChanged(nameof(OnlineSearchResults));
            }
        }
        #endregion

        #region Visibility
        public bool AnimeLibraryVisible
        {
            get
            {
                if (!APIServiceCollections.HummingbirdV1API.AnimeAPI.Supported) return false;
                if (HummingbirdUser_LoggedIn == null) return false;
                return HummingbirdUser_LoggedIn.LoginInfo.HasUsername;
            }
        }
        public bool MangaLibraryVisible
        {
            get
            {
                if (!APIServiceCollections.HummingbirdV1API.MangaAPI.Supported) return false;
                if (HummingbirdUser_LoggedIn == null) return false;
                return HummingbirdUser_LoggedIn.LoginInfo.HasUsername;
            }
        }
        #endregion

        private string m_searchTerms = "";
        public string SearchTerms
        {
            get { return m_searchTerms; }
            set
            {
                if (m_searchTerms == value) return;

                m_searchTerms = value;
                RaisePropertyChanged(nameof(SearchTerms));

                if (HummingbirdUser_LoggedIn == null) return;
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.SearchFilter.SearchTerm = value;
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.SearchFilter.SearchTerm = value;
            }
        }

        private SearchFilter m_filter = SearchFilter.Everything;
        public SearchFilter Filter
        {
            get { return m_filter; }
            set
            {
                if (m_filter == value) return;
                m_filter = value;
                RaisePropertyChanged(nameof(Filter));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public HummingbirdSearchViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                HummingbirdUser_LoggedIn = new HummingbirdUser();
                HummingbirdUser_LoggedIn.UserInfo.Username = "Design Time";
                HummingbirdUser_LoggedIn.UserInfo.AvatarUrl = new System.Uri("https://static.hummingbird.me/users/avatars/000/007/415/thumb/TyrilCropped1.png?1401236074", System.UriKind.Absolute);
                HummingbirdUser_LoggedIn.HummingbirdUserInfo.cover_image = "https://static.hummingbird.me/users/cover_images/000/007/415/thumb/Zamma_resiz.jpg?1401237213";

                HummingbirdUser_LoggedIn.LoginInfo.Username = "Design Time";
                HummingbirdUser_LoggedIn.LoginInfo.AuthToken = "AuthToken";

                // Search
                SearchTerms = "Gate";
                Filter = SearchFilter.Anime;

                // Results
                AnimeObject anime = new AnimeObject();
                anime.RomanjiTitle = "Gate: Jieitai Kanochi nite, Kaku Tatakaeri";
                anime.EnglishTitle = "GATE";
                anime.CoverImageUrl = new Uri("https://static.hummingbird.me/anime/poster_images/000/010/085/large/85a5d8cc2972ae422158be7069076be41435868848_full.jpg?1435924413", UriKind.Absolute);
                anime.EpisodeCount = 24;

                SearchBoxAutoSuggestions.Add(anime);

                // Online Search Results
                OnlineSearchResults.Add(anime);
                OnlineSearchResults.Add(anime);
                OnlineSearchResults.Add(anime);
                OnlineSearchResults.Add(anime);
                OnlineSearchResults.Add(anime);
                OnlineSearchResults.Add(anime);

                // Library
                LibraryObject completedLibraryObject = new LibraryObject();
                completedLibraryObject.Anime = anime;
                completedLibraryObject.Rating = 3.5;
                completedLibraryObject.EpisodesWatched = 6;
                completedLibraryObject.Section = LibrarySection.Completed;

                // Anime Library
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);

                // Manga Library
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection.Add(completedLibraryObject);
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
            MainViewModel.Instance.CurrentNavigationLocation = NavigationLocation.Search;

            if (HummingbirdUser_LoggedIn != null)
            {
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.LibrarySelectionFilter.LibrarySelection = LibrarySection.All;
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.LibrarySelectionFilter.LibrarySelection = LibrarySection.All;
            }

            if (!string.IsNullOrWhiteSpace(SearchTerms))
            {
                SearchAnime();
            }
        }

        public override void OnNavigatedFrom()
        {
            SearchTerms = "";
        }

        public override void ResetViewModel()
        {

        }

        #region Navigate Anime Details Page
        public RelayCommand<AnimeObject> NavigateAnimeDetailsCommand
        {
            get
            {
                return new RelayCommand<AnimeObject>((anime) =>
                {
                    NavigateAnimeDetails(anime);
                });
            }
        }

        public void NavigateAnimeDetails(AnimeObject anime)
        {
            Debug.WriteLine("Navigating Anime Details - Search View Model");
            if (anime == null) return;

            ViewModelLocator.Instance.vm_HummingbirdAnimeLibraryViewModel.NavigateAnimeDetailsPage(anime);
        }
        #endregion

        #region Get Anime
        public RelayCommand<ServiceID> GetAnimeCommand
        {
            get
            {
                return new RelayCommand<ServiceID>((serviceID) =>
                {
                    GetAnime(serviceID);
                });
            }
        }

        public void GetAnime(ServiceID serviceID)
        {
            Debug.WriteLine("Getting Anime");
            if (string.IsNullOrWhiteSpace(serviceID.ID))
                return;

            Progress<APIProgressReport> m_getAnimeDetailsProgress = new Progress<APIProgressReport>();
            m_getAnimeDetailsProgress.ProgressChanged += M_getAnimeDetailsProgress_ProgressChanged;
            APIServiceCollection.Instance.HummingbirdV1API.AnimeAPI.GetAnime(serviceID.ID, m_getAnimeDetailsProgress);
        }

        private void M_getAnimeDetailsProgress_ProgressChanged(object sender, APIProgressReport e)
        {
            ProgressService.SetIndicatorAndShow(true, e.Percentage, e.StatusMessage);
            if (e.CurrentAPIResonse == APIResponse.Successful)
            {
                ProgressService.Reset();
            }
            else if (APIResponseHelpers.IsAPIResponseFailed(e.CurrentAPIResonse))
            {
                ProgressService.Reset();
            }
        }
        #endregion

        #region Search Anime
        public RelayCommand SearchAnimeCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SearchAnime();
                });
            }
        }

        public void SearchAnime()
        {
            Debug.WriteLine("Searching Anime");
            if (string.IsNullOrWhiteSpace(SearchTerms))
                return;

            // Search Local
            if (HummingbirdUser_LoggedIn != null)
            {
                HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.ApplyFilters();
                HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.ApplyFilters();
            }

            // Search Online
            Progress<APIProgressReport> m_searchAnimeDetailsProgress = new Progress<APIProgressReport>();
            m_searchAnimeDetailsProgress.ProgressChanged += M_searchAnimeDetailsProgress_ProgressChanged; ;
            APIServiceCollection.Instance.HummingbirdV1API.AnimeAPI.SearchAnime(SearchTerms, m_searchAnimeDetailsProgress);
        }

        private void M_searchAnimeDetailsProgress_ProgressChanged(object sender, APIProgressReport e)
        {
            ProgressService.SetIndicatorAndShow(true, e.Percentage, e.StatusMessage);
            if (e.CurrentAPIResonse == APIResponse.Successful)
            {
                ProgressService.Reset();
                OnlineSearchResults = new ObservableCollection<AnimeObject>((List<AnimeObject>)e.Parameter.Converted);
            }
            else if (APIResponseHelpers.IsAPIResponseFailed(e.CurrentAPIResonse))
            {
                ProgressService.Reset();
            }
        }
        #endregion

        #region SearchBox Microphone
        public RelayCommand SearchBoxMicrophoneCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SearchBoxMicrophone();
                });
            }
        }

        public void SearchBoxMicrophone()
        {
            Progress<APIProgressReport> cortanaProgress = new Progress<APIProgressReport>();
            cortanaProgress.ProgressChanged += CortanaProgress_ProgressChanged;

            string exampleText = "";
            exampleText = HummingbirdUser_LoggedIn?.AnimeLibrary.SelectRandomTitle()?.Anime.RomanjiTitle ?? "Steins;Gate";
            CortanaTools.Instance.StartListening(cortanaProgress, exampleText);
        }

        private void CortanaProgress_ProgressChanged(object sender, APIProgressReport e)
        {
            if (e.CurrentAPIResonse == APIResponse.Successful)
            {
                Debug.WriteLine("Cortana Succeeded");
                SpeechRecognitionResult result = e.Parameter.Converted as SpeechRecognitionResult;
                if (string.IsNullOrWhiteSpace(result.Text)) return;

                if (HasMediaService)
                    CortanaTools.SynthesizeVoice("Searching " + result.Text, MediaService.MediaPlayer);

                SearchTerms = result.Text;
                SearchAnime();
            }
            else if (e.CurrentAPIResonse == APIResponse.ContinuingExecution)
            {
                Debug.WriteLine("Cortana Hypothesis Suggested");
                SpeechRecognitionHypothesis hypothosis = e.Parameter.Converted as SpeechRecognitionHypothesis;
                if (string.IsNullOrWhiteSpace(hypothosis.Text)) return;

                SearchTerms = hypothosis.Text;
                SearchAnime();
            }
            else
            {
                Debug.WriteLine("Cortana Failed");
                SearchTerms = "";
            }
        }
        #endregion

        public void UpdateAutoSuggestions()
        {
            if (HummingbirdUser_LoggedIn == null) return;

            ObservableCollection<AnimeObject> autoSuggestions = new ObservableCollection<AnimeObject>();

            foreach (var item in HummingbirdUser_LoggedIn.AnimeLibrary.LibraryCollection.UnfilteredCollection)
            {
                if (item.Anime.DoesNameFitSearch(SearchTerms))
                    autoSuggestions.Add(item.Anime);
            }

            foreach (var item in HummingbirdUser_LoggedIn.MangaLibrary.LibraryCollection.UnfilteredCollection)
            {
                if (item.Anime.DoesNameFitSearch(SearchTerms))
                    autoSuggestions.Add(item.Anime);
            }

            SearchBoxAutoSuggestions = autoSuggestions;
        }
    }
}