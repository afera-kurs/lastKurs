#pragma checksum "..\..\..\..\View\Pages\AdminEmployeeData.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "719A2F49FB261DBF1CD4C57A36FE7AC0C6172249DF657177102F673476C03772"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ASMaIoP.View.Pages;
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


namespace ASMaIoP.View.Pages {
    
    
    /// <summary>
    /// AdminEmployeeData
    /// </summary>
    public partial class AdminEmployeeData : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\View\Pages\AdminEmployeeData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateNewUser;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\View\Pages\AdminEmployeeData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ActiveUser;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\View\Pages\AdminEmployeeData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid EmployeeDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/ASMaIoP;component/view/pages/adminemployeedata.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Pages\AdminEmployeeData.xaml"
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
            
            #line 8 "..\..\..\..\View\Pages\AdminEmployeeData.xaml"
            ((ASMaIoP.View.Pages.AdminEmployeeData)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CreateNewUser = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\View\Pages\AdminEmployeeData.xaml"
            this.CreateNewUser.Click += new System.Windows.RoutedEventHandler(this.CreateNewUser_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ActiveUser = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\View\Pages\AdminEmployeeData.xaml"
            this.ActiveUser.Click += new System.Windows.RoutedEventHandler(this.ActiveUser_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.EmployeeDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 43 "..\..\..\..\View\Pages\AdminEmployeeData.xaml"
            this.EmployeeDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.EmployeeDataGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

