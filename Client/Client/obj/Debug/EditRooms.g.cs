﻿#pragma checksum "..\..\EditRooms.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F4AE60FA45B493F601885B1E0C2314A4D69A2BF2A68DC93110FC4447157E4977"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using Client;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Client {
    
    
    /// <summary>
    /// EditRooms
    /// </summary>
    public partial class EditRooms : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\EditRooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock napis;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\EditRooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Insert;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\EditRooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Update;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\EditRooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Delete;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\EditRooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Dane;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\EditRooms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Client;component/editrooms.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EditRooms.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.napis = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Insert = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\EditRooms.xaml"
            this.Insert.Click += new System.Windows.RoutedEventHandler(this.Create);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Update = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\EditRooms.xaml"
            this.Update.Click += new System.Windows.RoutedEventHandler(this.Modify);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Delete = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\EditRooms.xaml"
            this.Delete.Click += new System.Windows.RoutedEventHandler(this.Delet);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 36 "..\..\EditRooms.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Clear);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Dane = ((System.Windows.Controls.DataGrid)(target));
            
            #line 38 "..\..\EditRooms.xaml"
            this.Dane.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.Hide);
            
            #line default
            #line hidden
            
            #line 38 "..\..\EditRooms.xaml"
            this.Dane.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Selected);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
