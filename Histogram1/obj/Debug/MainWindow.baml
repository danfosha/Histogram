   M S B A M L   `   `   `  ����E  AHistogram1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Histogram1
Histogram1  5   	       Histogram1.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Histogram1     5   	   -   5          � Title$  
Histogram1��5   	   $	��700��6   $	��900��6)    � WindowStartupLocation$ CenterScreen=�65   ���#F0F0F0 � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $2 ,ResourceDictionaries\HistoBoxDictionary.xaml?�6%   %    5      .��5      �  � UseLayoutRounding .  � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6   .+�5   
   � $��0��6   $��20 20 10 20q�6    5      �� 5!      $
��Auto �6#   �� 5#      $��* �6#   �� 5%      $
��Auto �6#    � RowDefinitions 5(      �� 5*      $��* �6    �� 5,      $
��Auto �6    .+�51      �� $��0��6   $��3��6$   $��0��68   $��0,0,0,20q�6g   $��Right=�6y   $	��565��6�   $	��536��6�   $��Bottom=�6�   ��6E   �� *LocalImagePath+6�   �� 52      $��0��6   $��1��6$   .��61   	File Name6<   �� 53      $��1��6   $��1��6&   $��2q�6M   ��63   �� *
ImageURL+6M   �� 54      -   5        $��2��54      $��1��6%   $��40��6H   $��2q�6S   .��6^   Go!6c   56      � 59   
   $��1��6   $��4q�6    5:      �� 5;      �� 5<      �� 5=      �� 5>      .+�5@      �� $��0��6   $��0��6'   $��
Luminosity��64      histoGroupBox#��[  6H   .��5A      ��   histoBoxBorder#��[ 6   ._�5B      �   histoPolygon#��[ 6J   
���   � � Points 6   �� *LuminanceHistogramPoints+6q   5C      5D      �� 5F      $��0��6   $��1��6'   $	��Red��64   #��[  6A   .��5G      �� #��[ 6   ._�5H      � #��[ 6I   
���  �� 6   �� *RedColorHistogramPoints+6p   5I      5J      �� 5L      $��0��6   $��2��6'   $��Green��64   #��[  6C   .��5M      �� #��[ 6   ._�5N      � #��[ 6K   
��� � � 6   �� *GreenColorHistogramPoints+6r   5O      5P      �� 5R      $��0��6   $��3��6'   $
��Blue��64   #��[  6C   .��5S      �� #��[ 6   ._�5T      � #��[ 6J   
����  � 6   �� *BlueColorHistogramPoints+6p   5U      5V      5W      �� 5Z   
   $��12(�6   $��20��6"   $	��Top=�6.   $��20 10q�6F   �� 5[   	   -   5        
�������$7��1https://www.linkedin.com/in/daniel-fosha-02a5028/?�5[   >   .�6~   Built by Daniel Fosha6�   5\      5]      5^      