﻿using AnimeTrackingServiceWrapper;
using Anitro.Models;
using Anitro.Models.Enums;
using Anitro.Pages;
using Anitro.Pages.Hummingbird;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;
using System;
using Anitro.Models.Page_Parameters;
using AnimeTrackingServiceWrapper.UniversalServiceModels;
using Windows.UI.Xaml;
using Anitro.Helpers;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Killerrin_Studios_Toolkit;
using Windows.ApplicationModel.Email;
using Windows.UI.Popups;

namespace Anitro.ViewModels
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
    public class SettingsViewModel : AnitroViewModelBase
    {
        public static SettingsViewModel Instance { get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); } }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public SettingsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }

            ResetViewModel();
        }

        public override void Loaded()
        {

        }

        public override void OnNavigatedTo()
        {
        }

        public override void OnNavigatedFrom()
        {

        }

        public override void ResetViewModel()
        {

        }

        #region Unlock Anitro
        public RelayCommand UnlockAnitroCommand { get { return new RelayCommand(() => { UnlockAnitro(); }); } }
        public async void UnlockAnitro()
        {
            if (AnitroLicense.AnitroUnlocked) return;
            Debug.WriteLine("Unlock Anitro");

            InAppPurchaseHelper.PurchaseAnitroUnlock();
        }
        #endregion
    }
}