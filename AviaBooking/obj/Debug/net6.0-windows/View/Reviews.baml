   M S B A M L   `   `   `  ����F  BAviaBooking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AviaBookingAviaBooking  5      ?$clr-namespace:AviaBooking.ViewModelsAviaBooking.ViewModels  6/       AviaBooking.Reviews   5      Z VDevExpress.Mvvm.UI, Version=22.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:AviaBooking     5      ;
viewmodels$clr-namespace:AviaBooking.ViewModels     6/     N� Title$  Reviews��5	       � DataContext 5
      -   'AviaBooking.ViewModels.ReviewsViewModel  5   
   .��5      � 
������� � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $	��100 �6   .+�5   
   �� $��0��6   $��Center=�6"   $��	0,35,0,35q�6?   $
��Auto��6R   .+�5      �� $��Отзывы��6   $��Segoe UI)�6%   $��30(�5      $
��Bold"�6"   $��Center=�5      �� 5      $��25��68    �� DisplayMemberPath$
 Name��5      $��Center=�5      $	��150��64   ��5      �� *
Airlines+ �� SelectedItem 5   0   �� *SelectedAirline+5   4   �� 5      $#��Показать отзывы��6   $	��150��6/   ��6;   �� *ShowReviews+�� 5      $��0q�5      $��0,25,0,0q�6+   ��5      �� *ReviewModels+��5      �� 5       .R�5!      ��  �� CornerRadius$ 10��6!    �� Padding$ 15,20q�63   $��15q�6C   $��1q�6O   
�������._�5"      � $	��500��6#    5#   "   �� 5$   &   $
��Auto �64   �� 5%   &   .+�5'   "   � $��0��6'   $��0,0,0,15q�64   .+�5(   &   ��  �� Orientation$ 
Horizontal=�61   .+�5)   *   �� $��0,0,5,0q�6P   ��64   �� *
ClientName+6P   �� 5*   *   ��64   �� *ClientSurname+5+   '   �� 5-   &   $ 
Horizontal=�61   $��Right=�6J   .+�5.   *   �� $��Оценка:��64   $��0,0,5,0q�6C   �� 5/   *   ��64   �� *Score+50   '   51   #   �� 52   "   $��1��6,   ��69   �� *
ReviewText+54      55      56      58      5;      � 5<   
   $��0��6   $��Right=�6   $	��Top=�68   $��	0,75,75,0q�5=      .+�5>      �� $ Vertical=�6   .+�5?      �� $!��Добавить отзыв��6   $��25��62   ��5@      �� *ShowReviewMenu+�� 5A      $��0,15,0,0q�6   y�6/   �� *ReviewMenuVisibility+.+�5B      �� $!��Оценка от 1 до 5:��6    �� 5C      ��6   �� *review.Score+�� 5D      $��Отзыв:��6    �� 5E      ��6   �� *review.Text+�� 5G      $#��Отправить отзыв��6   ��67   �� *
SendReview+5H      5J      5K         AviaBooking.Footer  5N   
   $��1��6   5P      5Q      