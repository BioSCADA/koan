   M S B A M L   `   `   `  ����  Koan    BioSCADA.UserLogin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $��Roboto)�5      .��5      � $��0q�6   $	��181��6   $	��132��6"     � 	Resources  5	   
    �� DataType �� XPath��%�  (��       5      *Person+)�� 6   $ Person��6   .R�5      �� $��Stretch=�6   .+�5      �� $��Stretch=�6    $
��Bold"�6>   Nome: �� 6W   ��6a   �� $ @Name��6�   �� 5      $��Stretch=�6    $
��Bold"�6>   Id: �� 6U   ��6_   �� $	 @Id��6   �� 5      $��Stretch=�6    $
��Bold"�6>   Grupo:6X   �� 5      $��Stretch=�6   ��6<   �� $ ./Group��5      5      5       � ColumnDefinitions 5   
   �� 5      $	��80* �6   �� 5      $
��101* �6    � RowDefinitions 5   
   �� 5      $��21 �6   �� 5      $��19 �6   �� 5       $��40 �6   �� 5!      $��0* �6   �� 5"      $
��Auto �6   $	��124��6*   .+�5$   
   �� $��17,6,9,4q�6      
Foreground#��[  6&   $��Roboto)�6O   $��Center=�6c   $��30��6~   $��2��6�   �� 6�   $�Usuário��6�   6�   �� 5%   
   $��16(�6   $��48,10,47,10q�6    $��1��65   $��2��6E   �� 5&   
   $��2��6   $��	21,5,13,5q�6!   #��[  64   $��Roboto)�6]   $��Center=�6q   $��30��6�   �� 6�   $�Senha��6�   6�   �� 5'   
   -   5         ��Name$ 	txt_login��5'      $��0,5,9,5q�6%   $��Roboto)�66   $	��106��6J   $��30��6V   $��Right=�6b   $��Center=�6~   $��	convidado��6�   $��2��6�   $��2��6�   G� 5(   
   -   5         G�Name$ txt_pass��5(      $��2��6(   $��0,5,9,5q�65    G� PasswordChar$ *��6F   $��Roboto)�6W   $	��106��6k   $��30��6w   $��Right=�6�   $��Center=�6�    G� Password$ 	convidado��6�   $��2��6�   �� 5)   
   -   5        	 ��Name$	 bt_login��5)      $��18,10,10,84q�6#   $��Login��68   $��4��6_     BackgroundNormal#���  6l   $��1��6�   �� 5*   
   -   5        
 ��Name$
 lb_joomla_user��5*      $��4��6*   $ �� IsSynchronizedWithCurrentItem$
 TrueY�67     PeopleProvider � DataContext# �  6\   $��2��6�   $��0q�6�   $��Roboto)�6�   $��10(�6�   $y�	Collapsed=�6�   ��6�   �� 6�   5,      5-      