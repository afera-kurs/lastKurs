   M S B A M L   `   `   `  ????B  >ASMaIoP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ASMaIoP.ViewASMaIoP.View  5   	       ASMaIoP.View.AddWeakend   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ASMaIoP.View     5   	     ? 
ResizeMode$  NoResize=?5   	    ? Title$ 
AddWeakend??5	   	   $	??450??6   $	??800??6)   .??5
      ?  ? ColumnDefinitions 5   
   ?? 5      $	??10* ?6    ? RowDefinitions 5   
   ?? 5      $	??10* ?6   ?? 5      $??1* ?6   .+?5   
   ? $??0??6   $??1??5      .+?5      ?? $??1??6   $??2??5      
??????? ?? CornerRadius$ 15??5      $??10q?5      ._?5      ?? .+?5      ?? 
???????$??7q?5      $ 10??5      ._?5      ?  5      ?? 5    "   ?? 5!   "    5#      ?? 5$   "   $??30 ?60   ?? 5%   "   $	??10* ?60   .+?5'      ??  ?? Orientation$ 
Horizontal=?6)   .+?5(   "   ?? $??5q?6,   $??Причина :??5)   $   $
??Left=?5*   $   $??Center=?5+   $   ?? 5,   "   $??5q?6,   $
??Left=?5.   $   $??Center=?5/   $   ??5-   $   ??  ?? Path$ OldTaskTitle?5/   $   50      ?? 51      -   5         ??Name$ Description??51   &   $??3q?53   *   $??0??54   *   $??2??55   *   $??1??56   *   $	??100??57   *      TextBoxRound#???   58   "   ??52   &   ?? $ TaskDesc?.??59   #    ? 	Resources 5:   "   	 ?? 
TargetType
 ?? Property ?? Value??%^   '??       ?? 5;   &   "	 ??6,   .?5<   *   ?? 
 ?@??CornerRadius$ 7??6I   5=   '   5>   #   .??65   5?      5@      5A      ?? 5B      
???????$??7q?5C      $ 10??5D      ._?5E      ?  5F      ?? 5G   "   ?? 5H   "   .+?5J      ?? $ 
Horizontal=?6)   .+?5K   "   ?? $??15 5 5 5q?6,   $??Начало :??5L   $   $
??Left=?5M   $   $??Center=?5N   $   5O      (  "System.Windows.Controls.DatePicker  5P      -   5          Name$ FirstDay??5P   )   $??1??5Q   -   $??3 3 7 3q?5R   -   5S      5T      ?? 5U      
???????$??7q?5V      $ 10??5W      ._?5X      ?  5Y      ?? 5Z   "   ?? 5[   "   .+?5]      ?? $ 
Horizontal=?6)   .+?5^   "   ?? $??15 5 5 5q?6,   $??Крайний срок :??5_   $   $
??Left=?5`   $   $??Center=?5a   $   ?? 5b   "   -   5         ??Name$ 	LastDayTK??5b   ,   $??Center=?5c   ,   ??5d   ,   ?? $ OldLastDate?5e        5f      -   5        $ LastDay??5f   )   $??1??5g   -   $??3 3 7 3q?5h   -   5i      5j      ?? 5k      
???????$??7q?5l      $ 10??5m      ._?5n      ?  5o      ?? 5p   "   ?? 5q   "   .+?5s      ?? $ 
Horizontal=?6)   .+?5t   "   ?? $??15 5 5 5q?6,   $??Тип :??5u   $   $
??Left=?5v   $   $??Center=?5w   $   5x      ?? 5y      -   5         ??Name$
 Type??5y   '   $??1??5z   '   $??Right=?5{   '   $??25??5|   '   $	??120??5}   '   $??	10 5 10 5q?5~   '     Combobox_Base#???  5   '   .@?5?   "   ?? $??Больничный??6,   ?? 5?   "   $%??Отпуск ежегодный??6,   ?? 5?   "   $)??#Отпуск за свой счёт??6,   5?      5?      5?      5?      5?      5?      ?? 5?   
   $??3??6   $ 
Horizontal=?5?      $??Right=?5?      .+?5?      ?? -   5         ??Name$ SaveBtn??5?      ???#359EF4  ButtonLeftPanel#???  5?      $??Сохранить??5?      $??Right=?5?      $??25??5?      $??5q?5?      $	??100??5?      5?      5?      5?      