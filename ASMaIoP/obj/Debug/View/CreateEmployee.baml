   M S B A M L   `   `   `  ????B  >ASMaIoP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ASMaIoP.ViewASMaIoP.View  5   	   !    ASMaIoP.View.CreateEmployee   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ASMaIoP.View     5   	     ? Title$  CreateEmployee??5   	   $	??480??6    $	??430??6-    ? 
ResizeMode$ NoResize=?5	   	   .??5
      ? .+?5   
   ? -   5         ?Name$ Physical??5      $y?	Collapsed=?5       ? ColumnDefinitions 5      ?? 5      $	??10* ?6#    ? RowDefinitions 5      ?? 5      $	??10* ?6    ?? 5      $	??10* ?6    ?? 5      $	??10* ?6    ?? 5      $??5* ?6    .+?5      ?? $%??Создание профиля??6   $??22(?5      $
??Left=?5      $??Bottom=?5      $??20 20 20 40q?5      ?? 5      $5??/Данные о физическом лице. ??6   $??14(?5      $
??Left=?5      $??Bottom=?5      $??2??5      $??20 20 20 20q?5       ? 5!      $??1??6   $??2??5"      $y?Visible=?6    .+?5$      ?? $??1??6   $??2??5%      
??????? ?? CornerRadius$ 15??5'      $??10q?5(      ._?5)      ?? .+?5*      ?? 
???????$??7q?5+      $ 10??5,      ._?5-      ?  5.   "   ?? 5/   &   ?? 50   &   .+?52   "   ?? $??5q?6,   $??Имя :??53   $   $
??Left=?54   $   $??Center=?55   $   ?? 56   "   $??3q?6*   $??1??57   "   $??Right=?58   "      TextBoxRound#???   59   "   $	??100??5;   "   ??5:   "   ??  ?? Path$
 Name?.??5;   .    ? 	Resources 5<   &    ?? 
TargetType	 ?? Property
 ?? Value??%^   '??       ?? 5=   *   " ??60   .?5>   .   ?? 	 ?@??CornerRadius$
 7??6M   5?   +   5@   '   .??69   5A   #   5B      5C      ?? 5D      
???????$??7q?5E      $ 10??5F      ._?5G      ?  5H   "   ?? 5I   &   ?? 5J   &   .+?5L   "   ?? $??5q?6,   $??Фамилия :??5M   $   $
??Left=?5N   $   $??Center=?5O   $   ?? 5P   "   $??3q?6*   $??1??5Q   "   $??Right=?5R   "   #???   5S   "   $	??100??5U   "   ??5T   "   ?? $ Surname?.??5U   .    5V   &   ??%^   '??       ?? 5W   *   " ??60   .?5X   .   ?? 	 ?@??CornerRadius$
 7??6M   5Y   +   5Z   '   .??69   5[   #   5\      5]      ?? 5^      
???????$??7q?5_      $ 10??5`      ._?5a      ?  5b   "   ?? 5c   &   ?? 5d   &   .+?5f   "   ?? $??5q?6,   $??Отчество :??5g   $   $
??Left=?5h   $   $??Center=?5i   $   ?? 5j   "   $??3q?6*   $??1??5k   "   $??Right=?5l   "   #???   5m   "   $	??100??5o   "   ??5n   "   ?? $ PatName?.??5o   .    5p   &   ??%^   '??       ?? 5q   *   " ??60   .?5r   .   ?? 	 ?@??CornerRadius$
 7??6M   5s   +   5t   '   .??69   5u   #   5v      5w      ?? 5x      
???????$??7q?5y      $ 10??5z      ._?5{      ?  5|   "   ?? 5}   &   ?? 5~   &   .+?5?   "   ?? $??5q?6,   $#??Номер телефона :??5?   $   $
??Left=?5?   $   $??Center=?5?   $   ?? 5?   "   $??3q?6*   $??1??5?   "   $??Right=?5?   "   #???   5?   "   $	??100??5?   "   ??5?   "   ?? $ PhoneNumber?.??5?   .    5?   &   ??%^   '??       ?? 5?   *   " ??60   .?5?   .   ?? 	 ?@??CornerRadius$
 7??6M   5?   +   5?   '   .??69   5?   #   5?      5?      ?? 5?      
???????$??7q?5?      $ 10??5?      ._?5?      ?  5?   "   ?? 5?   &   ?? 5?   &   .+?5?   "   ?? $??5q?6,   $)??#Адрес регистрации :??5?   $   $
??Left=?5?   $   $??Center=?5?   $   ?? 5?   "   $??3q?6*   $??1??5?   "   $??Right=?5?   "   #???   5?   "   $	??100??5?   "   ??5?   "   ?? $ Address?.??5?   .    5?   &   ??%^   '??       ?? 5?   *   " ??60   .?5?   .   ?? 	 ?@??CornerRadius$
 7??6M   5?   +   5?   '   .??69   5?   #   5?      5?      ?? 5?      
???????$??7q?5?      $ 10??5?      ._?5?      ?  5?   "   ?? 5?   &   ?? 5?   &   .+?5?   "   ?? $??5q?6,   $,??&Первый рабочий день :??5?   $   $
??Left=?5?   $   $??Center=?5?   $   (  "System.Windows.Controls.DatePicker  5?   "   -   5          Name$ FirstWorkDay??5?   -   $	??100??5?   -   $??1??5?   -   $??3q?5?   -   $??Right=?5?   -      SelectedDate 5?   -   ?? $ FirstWorkDay?5?   -   5?      5?      5?      5?      5?      ?? 5?      -   5         ??Name$ 
CreateUser??5?      ???#359EF4  ButtonLeftPanel#???  5?      $??Создать??5?      $??Right=?5?      $??10q?5?      $	??100??5?      $??3??5?      ?? 5?      -   5        $
 Prev??5?      $??0??6#   $??3??5?      
???????$	??100??5?      #???  5?      $??
Назад??5?      $??10,10,-60,10q?5?      5?      ? 5?   
   -   5        $ Employee??5?      $y?Visible=?5?       5?      ?? 5?      $	??10* ?6#    5?      ?? 5?      $	??10* ?6    ?? 5?      $	??10* ?6    ?? 5?      $	??10* ?6    ?? 5?      $??5* ?6    .+?5?      ?? $%??Создание профиля??6   $??22(?5?      $
??Left=?5?      $??Bottom=?5?      $??20 20 20 40q?5?      ?? 5?      $*??$Данные о работнике. ??6   $??14(?5?      $
??Left=?5?      $??Bottom=?5?      $??2??5?      $??20 20 20 20q?5?      ? 5?      $??1??6   $??2??5?      .+?5?      ?? $??1??6   $??2??5?      
???????$ 15??5?      $??10q?5?      ._?5?      ?? .+?5?      ?? 
???????$??7q?5?      $ 10??5?      ._?5?      ?  5?   "   ?? 5?   &   ?? 5?   &   .+?5?   "   ?? $??5q?6,   $??Логин :??5?   $   $
??Left=?5?   $   $??Center=?5?   $   ?? 5   "   $??3q?6*   $??25??5  *   $??1??5  "   $??Right=?5  "   #???   5  "   $	??100??5  "   ??5  "   ?? *Login+.??5  .    5  &   ??%^   '??       ?? 5  *   " ??60   .?5	  .   ?? 	 ?@??CornerRadius$
 7??6M   5
  +   5  '   .??69   5  #   5     5     ?? 5     
???????$??7q?5     $ 10??5     ._?5     ?  5  "   ?? 5  &   ?? 5  &   .+?5  "   ?? $??5q?6,   $??Пароль :??5  $   $
??Left=?5  $   $??Center=?5  $   ?? 5  "   $??5q?6*   $??25??5  *   $??1??5  "   $??Right=?5  "   #???   5  "   $	??100??5!  "   ??5   "   ?? $ Password?.??5!  .    5"  &   ??%^   '??       ?? 5#  *   " ??60   .?5$  .   ?? 	 ?@??CornerRadius$
 7??6M   5%  +   5&  '   .??69   5'  #   5(     5)     ?? 5*     
???????$??7q?5+     $ 10??5,     ._?5-     ?  5.  "   ?? 5/  &   ?? 50  &   .+?52  "   ?? $??5q?6,   $??Должность :??53  $   $
??Left=?54  $   $??Center=?55  $   ?? 56  "   -   5         ??Name$ roleComboBox??56  +   $??5q?6A   $??1??57  +     Combobox_Base#???  58  +   $??Right=?59  +   $??25??5;  +   $	??100??5<  +   ??5:  +   ?? 5=  #   5>     5?     ?? 5@     
???????$??7q?5A     $ 10??5B     ._?5C     ?  5D  "   ?? 5E  &   ?? 5F  &   .+?5H  "   ??  ?? Orientation$ 
Horizontal=?6-   .+?5I  %   ?? $??5q?6/   $??Карта :??5J  $   $
??Left=?5K  $   $??Center=?5L  $   ?? 5M  %   -   5         ??Name$ 
CardIDtext??5N  $   $??5q?5M  /   $
??Left=?5P  $   $??Center=?5Q  $   ??5O  $   ?? $ CardId?5Q  $   5R  #   ?? 5S  "   -   5        $ CardRead??5S  )   $??25??5T  )   #???  5U  )   $??Считать карту??5V  )   
???????$??1??5X  )   $??3q?5Y  )   $??Right=?5[  )   $	??100??5\  )   5]     5^     5_     5`     5a     ?? 5b     -	   5        $
 Next??5b     $??1??6#   ???#359EF4#???  5d     $??
Далее??5e     $	??100??5f     $??10q?5g     $??Right=?5h     $??3??5j     ?? 5k     -
   5        $ cancel??5k     $??0??6%   $??3??5l     
???????$	??100??5n     #???  5o     $??Отмена??5p     $??10,10,-60,10q?5r     5s     5t     5u     