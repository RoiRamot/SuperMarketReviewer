   M S B A M L   `   `   `  ����U  QSupermarketReviewer.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SupermarketReviewer.ClientSupermarketReviewer.Client  5      S OSupermarketReviewer.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullwQclr-namespace:SupermarketReviewer.Core.Controls;assembly=SupermarketReviewer.Core!SupermarketReviewer.Core.Controls 5      S.clr-namespace:SupermarketReviewer.Client.Views SupermarketReviewer.Client.Views  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _Microsoft.Expression.Drawing, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35    BooleanToVisibilityConverter  WindowTemplateKey  �� 
TargetType ��Name �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName �� Property �� Value  BaseWindowStyle	 �� 
TargetType  BaseForegroundBrush  MainWindowBackgroundBrush
 �� Triggers P� Property P� Value  BusyAnimation g� RepeatBehavior �� 
TargetName �� TargetProperty9   3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value �� DataType=  7SupermarketReviewer.Client.StoreCompareResultsViewModel>   8SupermarketReviewer.Client.Views.StoreCompareResultsView5  /SupermarketReviewer.Core.Controls.BusyIndicator ��Name ��Name J� Opacity �� 	Converter%  Microsoft.Expression.Shapes.Arc  Name   ArcThickness �� Stretch N� EnterActions N� ExitActions�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      3client(clr-namespace:SupermarketReviewer.Client  5      ^controlsQclr-namespace:SupermarketReviewer.Core.Controls;assembly=SupermarketReviewer.Core  5      =ed4http://schemas.microsoft.com/expression/2010/drawing  5      8views.clr-namespace:SupermarketReviewer.Client.Views  5      %�  &	        &	      &	 #    7  7  7  &	 �    (�� �    5D      *, +)'      7   7   7  7  �� 5      6#   �� 5      "  �5	      .R�5
   
   �� #��z��6   #��z��5      #��z��5      ._�5      � .+�5      �� .u�5      �� 5      �� 5      $ WindowResizeGrip��6   $��Right=�5      $��Bottom=�5      ��.  $y�	Collapsed=�5      5      5       5   
   ]� 5       5      �� 5       �@�
ResizeMode$ CanResizeWithGrip=�66   �� 5       �@�WindowState$ Normal=�67   ./�5      �� $ WindowResizeGrip��6    � y�$ Visible=�6M   5      5!      �� 5"      "	 �5#      .�5$   
   ��  � ��8   6'   �� 5%   
    � ��8  6'   �� 5&   
    �@�WindowState$ 	Maximized=�6(   �� 5'   
    � �� 5(      �� 5)      "  �6"   .R�5*      �� #��z��6   #��z��5+      #��z��5,      ._�5-      �� .u�5.      �� 5/      50      51      53      
 54   
   P� 55       �@�
ResizeMode$ CanResizeWithGrip=�6,   . �56      ��  � ��8  6-   57      59      �� 5:      $ Forever��5;      .��5<   
   U� $ BusyIndicator��6(   $ (ed:Arc.EndAngle)�5=   (   .\�5>         $ 0��6#   $ 0Q�5?   #      5@      $ 0:0:1��6#   $	 359Q�5A   #   5B      5C      �� 5D      "  6   .R�5E   
     5F      �� 5G      "	  6   .�5H   
   ��  � ��
 ��d�6'   �� 5I   
    � �� 5J      �� 5K      "   6"   .R�5L      � .+�5M      �� $ ContentPresenter��6+   #��z��5N   +   #��z��5O   +   �� 5P      $ Screen��6!   
���   �$	 0.8Q�5R   !   y�5S   !   �� *IsBusy+8     5U      $ BusyIndicator��6!   $	��100��5V   !   $	��100��5W   !   $ 5Q�5X   !   #��z��5Y   !   $
 None=�5Z   !   y�5[   !   �� *IsBusy+8  5]       5^      P� 5_       �@ IsBusy .  5`      �� 5a   "   8�� 62    5c      �� 5d   "   8�� 62   5f      5h      5j      5k      5m      