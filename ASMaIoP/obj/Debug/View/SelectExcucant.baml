   M S B A M L   `   `   `  ????B  >ASMaIoP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ASMaIoP.ViewASMaIoP.View  5   	   !    ASMaIoP.View.SelectExcucant   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ASMaIoP.View     5   	     ? 
ResizeMode$  NoResize=?5   	    ? Title$ SelectExcucant??5	   	   $	??450??6    $	??355??6-   .??5
      ?  ? RowDefinitions 5   
   ?? 5      $	??10* ?6   ?? 5      $??1* ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ SelectedExcecut??5         CanUserResizeColumns .  $??0??5         AutoGenerateColumns .  $??5q?5      ??5      ??    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ ФИО??67   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 120 6b   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6D   ??  ?? Path$
 Name?6b     5       . $ Должность??67   $		 110 6m   
 6J   ?? $ 	RoleTitle?6m   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Добавить??6)   $	 90 6_   
 6;   ?? $ 
IsSelected?6_   5      ?? 5   
   -   5         ??Name$ Accept??5      $??25??5         ButtonLeftPanel#???   5       $??Подтвердить??5!      
???????$??1??5#      $??3q?5$      $??Right=?5%      $	??100??5&      5'      5(      