   M S B A M L   `   `   `  ����U  QSupermarketReviewer.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SupermarketReviewer.ClientSupermarketReviewer.Client  5      >    8SupermarketReviewer.Client.Views.StoreCompareResultsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;client(clr-namespace:SupermarketReviewer.Client     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5         MainWindowBackgroundBrush#��[  5      .��5
      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $��0��5      $��Center=�5      $��Center=�5      $��25(�5      $��DemiBold"�5      $��Supermarket Compare List��5        BaseTextblockStyle#��[ 64   �� 5   
   $��1��6   $��0��5      $��2��5      $	��0 5q�5      #��[  5      �� 5   
     BaseBorderBrush#��[ 6   $��2��6@   $��1q�6M   ._�5   
   ]� .+�5      �� $��Right=�6     BaseElementBackgroundBrush#��[ 5      .��5       �� $��10q�6   ��5!      �� *ShopingItems+��5"      �� 5#      .R�5$      � $��10q�6#    � ColumnDefinitions 5%   "   �� 5&   &   $	��200 �67   �� 5'   &   $��10 �67   �� 5(   &   .+�5*   "   �� $
��Left=�6,   #��[ 5+   J   ��6,   �� *Product.Name+6J   �� 5,   "   $��2��6,   $
��Left=�5-   ,   #��[ 5.   E   ��6,   �� *	Quntity+6E   5/      50      52      53      �� 54      #��[ 6   .��55      �� $��10q�6   ��56      �� *ShopingBaskts+��57      �� 58      .R�59      9� $
��0 10q�6N   ��6'   �� ��6:   �� ��5:   "   �� 5;   &   .R�5<   *   ]� .+�5=   .   �� $
��10 0q�68   #��[ 6F   �� 5>   2   $�Brand:��66    �� 5?   2   �66   �� *
Brand.Name+5@   /   �� 5A   .   $
��10 0q�68   #��[ 6F   �� 5B   2   $�store:��66    �� 5C   2   �66   �� *
Store.Name+5D   /   �� 5E   .   $
��10 0q�68   #��[ 6F   �� 5F   2   $�Adress:��66    �� 5G   2   �66   �� *Store.Adress+5H   /   �� 5I   .   $
��10 0q�68   #��[ 6F   �� 5J   2   $�Total Price:��66    �� 5K   2   �66   �� *
TotalPrice+5L   /   5M   +   5N   '   ��5P   "   �� 5Q   &   .R�5R   *   �� ��67   �� *ShoppingList+��5S   .   �� 5T   2   .R�5U   6   �  5V   :   �� 5W   >   $
��Auto �6O    z� SharedSizeGroup$ a��6\   �� 5X   >   $
��Auto �6O   $ b��6\   �� 5Y   >   $��* �6O   .+�5[   :   �� $
��10 0q�6D   $��0��6R   #��[ 6b   �� 5\   >   $�Product name: ��6B    �� 5]   >   �6B   �� *Product.Name+5^   ;   �� 5_   :   $
��10 0q�6D   $��1��6R   #��[ 6b   �� 5`   >   $�
Quantity: ��6B    �� 5a   >   �6B   �� *	Quntity+5b   ;   �� 5c   :   $
��10 0q�6D   $��2��6R   #��[ 6b   �� 5d   >   $�Price per unit: ��6B    �� 5e   >   �6B   �� *Product.Price+5f   ;   5g   7   5h   3   5j   +   5k   '   5m      5n      5p      5q      5r      5s      5t      5u      