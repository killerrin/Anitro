﻿

#pragma checksum "C:\Users\Andrew\Documents\GitHub\Anitro\Anitro 8.1\Anitro\Anitro.Windows\OptionsSettingsPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "76262C567EDF1CF001AA8E7557B83E47"
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
    partial class OptionsSettingsPane : global::Windows.UI.Xaml.Controls.SettingsFlyout, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 25 "..\..\OptionsSettingsPane.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.aboutButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 21 "..\..\OptionsSettingsPane.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.logoutButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


