﻿#pragma checksum "..\..\..\..\..\Windows\Stores\StoreCreationWindow - Copy.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FC151D27F26F5BCC0281275AD976DAE57CC87DC3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using StoreManagementSystem.View.Windows.Stores;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace StoreManagementSystem.View.Windows.Stores {
    
    
    /// <summary>
    /// StoreCreationWindow
    /// </summary>
    public partial class StoreCreationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\..\Windows\Stores\StoreCreationWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbName;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\..\Windows\Stores\StoreCreationWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox rtnDescription;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\..\Windows\Stores\StoreCreationWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StoreManagementSystem.View;V1.0.0.0;component/windows/stores/storecreationwindow" +
                    "%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Windows\Stores\StoreCreationWindow - Copy.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tbName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.rtnDescription = ((System.Windows.Controls.RichTextBox)(target));
            return;
            case 3:
            this.btnCreate = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\..\..\Windows\Stores\StoreCreationWindow - Copy.xaml"
            this.btnCreate.Click += new System.Windows.RoutedEventHandler(this.Create_Button);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

