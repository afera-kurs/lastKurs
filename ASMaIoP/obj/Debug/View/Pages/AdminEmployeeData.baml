   M S B A M L   `   `   `  ????B  >ASMaIoP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ASMaIoP.View.PagesASMaIoP.View.Pages  5      *    $ASMaIoP.View.Pages.AdminEmployeeData   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:ASMaIoP.View.Pages     5      -   5          N? Title$  AdminEmployeeData??5
      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??10* ?6   ?? 5      $
??2.3* ?6    ? RowDefinitions 5   
   ?? 5      $
??0.7* ?6   ?? 5      $	??10* ?6   ?? 5      $
??1.4* ?6   .+?5   
   ??  ?? Orientation$ 
Horizontal=?6   ?? 5   
   -   5         ??Name$ CreateNewUser??5         ButtonLeftPanel#???   6(   $??1??5      $??2??5      $??10q?5      $??45??5      ???#359EF4$'??!Новый пользватель??5      ?? 5   
   -   5        $ 
ActiveUser??5      #???   6%   $??1??5      $??Right=?5       $??0??5!      $??80??5"      $??5 5 20 5q?5#      
???????$??На смене??5&      &   System.Windows.Controls.DataGrid  5'   
   -   5          Name$ EmployeeDataGrid??5'      $??1??5(      $??2??5)         AutoGenerateColumns .     
IsReadOnly . $??20q?5.      ??5*      ?? *DataGridData+   Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Фамилия??6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   ??  ?? Path$ Surname?6W     52      $	 Имя??6%   $	
 100 6P    62   ?? $
 Name?6P     53      $	 Должность??6%   $	
 120 6[    68   ?? $ 	RoleTitle?6[     54      $	 Телефон??6%   $	
 150 6[    66   ?? $ PhoneNumber?6[     55      $	 Прописка??6%   $	
 150 6X    67   ?? $ Address?6X     56      $	 Статус??6%   $
 80 6U    65   ?? $ Status?6U   58      59      5:      