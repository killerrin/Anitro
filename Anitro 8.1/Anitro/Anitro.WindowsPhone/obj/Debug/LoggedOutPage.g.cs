﻿

#pragma checksum "C:\Users\killer rin\Documents\GitHub\Anitro\Anitro 8.1\Anitro\Anitro.WindowsPhone\LoggedOutPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "64E7C108E94C64F446B5E110E341F66D"
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
    partial class LoggedOutPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 11 "..\..\LoggedOutPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.LoggedOutPage_Loaded;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 16 "..\..\LoggedOutPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Search_Clicked;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 19 "..\..\LoggedOutPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.About_Clicked;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 20 "..\..\LoggedOutPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Settings_Clicked;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 21 "..\..\LoggedOutPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Review_Clicked;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 49 "..\..\LoggedOutPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.AdControl_Loaded;
                 #line default
                 #line hidden
                #line 50 "..\..\LoggedOutPage.xaml"
                ((global::Microsoft.Advertising.Mobile.UI.AdControl)(target)).ErrorOccurred += this.AdControl_ErrorOccured;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 42 "..\..\LoggedOutPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.LoginButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


