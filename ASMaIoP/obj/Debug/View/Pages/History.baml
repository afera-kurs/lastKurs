   M S B A M L   `   `   `  ????B  >ASMaIoP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ASMaIoP.View.PagesASMaIoP.View.Pages  5           ASMaIoP.View.Pages.History   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:ASMaIoP.View.Pages     5      -   5          N? Title$  History??5
      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??10* ?6    ? RowDefinitions 5   
   ?? 5      $	??10* ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ HistoryDataGrid??5         CanUserResizeColumns .  $??0??5         AutoGenerateColumns .     
IsReadOnly . $??20q?5      ??5      ??    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ФИО??6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 6P   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 62   ??  ?? Path$
 Name?6P     5      $ Дата??6%   $		 100 6Q   
 63   ?? $
 Date?6Q     5      $ Описание??6%   $		 430 6\   
 67   ?? $ Description?6\     5      $ Создадетель??6%   $		 150 6]   
 6:   ?? $ 	OwnerName?6]   5       5!      5"      