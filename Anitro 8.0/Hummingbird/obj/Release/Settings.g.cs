﻿#pragma checksum "C:\Users\killer rin\Programming\Windows Phone\Anitro\Hummingbird\Settings.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8877631E037F3B95A976C026EB2E0DB3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Hummingbird {
    
    
    public partial class Settings : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.ProgressBar ApplicationProgressBar;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.Pivot mainPivot;
        
        internal Microsoft.Phone.Controls.PivotItem generalPivot;
        
        internal System.Windows.Controls.TextBlock logoutText;
        
        internal System.Windows.Controls.TextBlock logoutUsernameText;
        
        internal System.Windows.Controls.Button logoutButton;
        
        internal System.Windows.Controls.TextBlock scheduledTaskRunner;
        
        internal Microsoft.Phone.Controls.PivotItem lockscreenPivot;
        
        internal Microsoft.Phone.Controls.ToggleSwitch lockscreenSwitch;
        
        internal System.Windows.Controls.Button lockscreenUpdate;
        
        internal System.Windows.Controls.Button lockscreenSettingsButton;
        
        internal Microsoft.Phone.Controls.ToggleSwitch randomize_Favourites;
        
        internal Microsoft.Phone.Controls.ToggleSwitch randomize_CurrentlyWatching;
        
        internal Microsoft.Phone.Controls.ToggleSwitch randomize_PlanToWatch;
        
        internal Microsoft.Phone.Controls.ToggleSwitch randomize_Completed;
        
        internal Microsoft.Phone.Controls.ToggleSwitch randomize_OnHold;
        
        internal Microsoft.Phone.Controls.ToggleSwitch randomize_Dropped;
        
        internal Microsoft.Phone.Shell.ApplicationBar mainAppBar;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Hummingbird;component/Settings.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationProgressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("ApplicationProgressBar")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.mainPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("mainPivot")));
            this.generalPivot = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("generalPivot")));
            this.logoutText = ((System.Windows.Controls.TextBlock)(this.FindName("logoutText")));
            this.logoutUsernameText = ((System.Windows.Controls.TextBlock)(this.FindName("logoutUsernameText")));
            this.logoutButton = ((System.Windows.Controls.Button)(this.FindName("logoutButton")));
            this.scheduledTaskRunner = ((System.Windows.Controls.TextBlock)(this.FindName("scheduledTaskRunner")));
            this.lockscreenPivot = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("lockscreenPivot")));
            this.lockscreenSwitch = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("lockscreenSwitch")));
            this.lockscreenUpdate = ((System.Windows.Controls.Button)(this.FindName("lockscreenUpdate")));
            this.lockscreenSettingsButton = ((System.Windows.Controls.Button)(this.FindName("lockscreenSettingsButton")));
            this.randomize_Favourites = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("randomize_Favourites")));
            this.randomize_CurrentlyWatching = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("randomize_CurrentlyWatching")));
            this.randomize_PlanToWatch = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("randomize_PlanToWatch")));
            this.randomize_Completed = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("randomize_Completed")));
            this.randomize_OnHold = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("randomize_OnHold")));
            this.randomize_Dropped = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("randomize_Dropped")));
            this.mainAppBar = ((Microsoft.Phone.Shell.ApplicationBar)(this.FindName("mainAppBar")));
        }
    }
}

