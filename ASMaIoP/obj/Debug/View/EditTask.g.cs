﻿#pragma checksum "..\..\..\View\EditTask.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "576A58B1EDC47052178BF5B656BE58A517031B09EF4B6322A45D98EDF4218A85"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ASMaIoP.View;
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


namespace ASMaIoP.View {
    
    
    /// <summary>
    /// EditTask
    /// </summary>
    public partial class EditTask : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TitleTask;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Quickly;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Description;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LastDayTK;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker LastDay;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeExecut;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListExecut;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock StateTK;
        
        #line default
        #line hidden
        
        
        #line 177 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox State;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\View\EditTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveTask;
        
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
            System.Uri resourceLocater = new System.Uri("/ASMaIoP;component/view/edittask.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\EditTask.xaml"
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
            this.TitleTask = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Quickly = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 3:
            this.Description = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.LastDayTK = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.LastDay = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.ChangeExecut = ((System.Windows.Controls.Button)(target));
            
            #line 132 "..\..\..\View\EditTask.xaml"
            this.ChangeExecut.Click += new System.Windows.RoutedEventHandler(this.ChangeExecut_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ListExecut = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.StateTK = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.State = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.SaveTask = ((System.Windows.Controls.Button)(target));
            
            #line 198 "..\..\..\View\EditTask.xaml"
            this.SaveTask.Click += new System.Windows.RoutedEventHandler(this.SaveTask_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

