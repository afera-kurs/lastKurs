   M S B A M L   `   `   `  ????B  >ASMaIoP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ASMaIoP.View.PagesASMaIoP.View.Pages  5      !    ASMaIoP.View.Pages.TaskView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:ASMaIoP.View.Pages     5      -   5          N? Title$  TaskView??5
      .??5      ?  ? RowDefinitions 5   
   ?? 5      $??1* ?6   ?? 5      $	??10* ?6   .+?5   
   ?? -   5         ??Name$ CreateTaskButton??5         ButtonLeftPanel#???   5      $!??Создать задачу??5      
???????$??30??5      $??5 5 15 5q?5      $??Right=?5      $	??100??5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ TaskDataGrid??5      $??1??5         
IsReadOnly . $??15q?5         AutoGenerateColumns .  ??5      ??    Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 90 6%      CanUserResize .  	   Header$	 Срочность??6k   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 60   ??  ?? Path$ TasksStatus?6k     5"      $	 130 6%    .  $	 Название??6j   
 61   ?? $ 	TaskTitle?6j     5#      $ 70 6%    .  $	 Статус??6j   
 60   ?? $ 
TasksState?6j     5$       .  $	 Крайний срок??6\   
 6;   ?? $ LastDay?6\     5%      $	 150 6%    .  $	 Создатель??6n   
 6G   ?? $ OwnerFullName?6n   5'      5(      5)      