   M S B A M L   `   `   `  ����U  QSatisfactoryItemCalculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SatisfactoryItemCalculatorSatisfactoryItemCalculator  5   	   +    %SatisfactoryItemCalculator.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:SatisfactoryItemCalculator     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��50 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��5      $��0��5      $��52(�5      $��2��5      .��6"    Satisfactory Raw Material Tool5      �� 5    
   $��0��5!      $��1��5"      $��15(�5#      $��Center=�5$      $��0,0,0,0q�5%      .��5&      Item Per Min. Empty = Default5(      �� 5)   
   $��1��5*      $��1��5+      $��15(�5,      $��Center=�5-      $��0,0,0,0q�5.      .��5/      List Of Items51      �� 52   
   -   5         ��Name$ ItemsPerMin��55      $��0��53      $��1��54      $��
10,30,10,0q�56      $��22��6!   $��2��57      $	��Top=�58      .��5;   
   05=      �� 5?   
   -   5         ��Name$ ItemSelected��5B      $��1��5@      $��2��5A      $
��10,0q�5C      6   �� 5E   
   -   5         ��Name$ ResetButton��5J      $��0��5F      $��3��5G      $��10,10,10,10q�5H      $��10q�5I      .��5K   '   Reset5M      �� 5N   
   -   5        $ 	AddButton��5S      $��1��5O      $��3��5P      $��10,10,10,10q�5Q      $��10q�5R      .��5U      Add6   �� 5V   
   -   5         ��Name$ ItemList��5[      $��0��5W      $��2��5X      $��2��5Y      $��4��5Z      $��0,0,0,0q�5\      6   5_      5`      