   M S B A M L   `   `   `  ????B  >ASMaIoP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ASMaIoP.View.PagesASMaIoP.View.Pages  5      !    ASMaIoP.View.Pages.JobsList   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:ASMaIoP.View.Pages     5      -   5          N? Title$  JobsList??5
      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??10* ?6   ?? 5      $
??2.3* ?6    ? RowDefinitions 5   
   ?? 5      $	??10* ?6   ?? 5      $
??1.4* ?6   .+?5   
   ?? -   5         ??Name$ 	CreateJob??5         ButtonLeftPanel#???   5      $??1??5      $??2??5      $??10q?5      ???#359EF4$#??Новая должность??5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ JobsDataGrid??5      $??0??5      $??2??5          AutoGenerateColumns .     
IsReadOnly . $??20q?5$      ??5!      ??    Columns 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Header$ Название??6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 6Z   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 67   ??  ?? Path$ 	RoleTitle?6Z     5'      $% Уровень допступа??6%   $		 150 6^   
 6?   ?? $ Level?6^     5(      $- 'Занимающих должность??6%   $		 200 6j   
 6C   ?? $ EmployeeCount?6j   5*      5+      5,      