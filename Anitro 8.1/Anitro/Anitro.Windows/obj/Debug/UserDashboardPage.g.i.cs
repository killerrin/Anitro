﻿

#pragma checksum "C:\Users\killer rin\Documents\GitHub\Anitro\Anitro 8.1\Anitro\Anitro.Windows\UserDashboardPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CF99F078A011F85772FD5E7D71585CCB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Anitro
{
    partial class UserDashboardPage : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton refreshUserInfo; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Hub DashboardHub; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.HubSection LoggedInHub_Social; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.HubSection LoggedInHub_General; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.HubSection Stats; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///UserDashboardPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            refreshUserInfo = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("refreshUserInfo");
            DashboardHub = (global::Windows.UI.Xaml.Controls.Hub)this.FindName("DashboardHub");
            LoggedInHub_Social = (global::Windows.UI.Xaml.Controls.HubSection)this.FindName("LoggedInHub_Social");
            LoggedInHub_General = (global::Windows.UI.Xaml.Controls.HubSection)this.FindName("LoggedInHub_General");
            Stats = (global::Windows.UI.Xaml.Controls.HubSection)this.FindName("Stats");
        }
    }
}



