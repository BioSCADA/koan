   M S B A M L   `   `   `  ���� ?  ;Koan, Version=2.0.0.1, Culture=neutral, PublicKeyToken=null#clr-namespace:BioSCADABioSCADA      BioSCADA.ReportPanel   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     (localclr-namespace:BioSCADA     .���   � 	Resources   �� DataType �� XPath��%  (��       *Acquisition+)�� $ Acquisition��.R��� $��Stretch=�.+��� $
��Bold"�Id: �� $��Normal"����� $
 ./Id���� $
��Bold"�Timestamp: �� $��Normal"����� $ ./Timestamp���� $
��Bold"�	Arquivo: �� $��Normal"����� $ ./File�� � RowDefinitions �� �� $��0* �.+�   BioSCADA.SessionPanel  -     Name$ session_panel��$��10,150,10,10q�$
��Auto��$	��150��$��Bottom=��� -    ��Name$ lb_aquisitions��$ �� IsSynchronizedWithCurrentItem$
 TrueY�   ReportProvider � DataContext# �   $��10,10,0,150q�$	��300��$
��Left=�$��Roboto)�$	��130��$	��Top=����� �� -    ��Name$ lb_filename��# �   $
��Bold"�	 �� TextAlignment$	 Center=�$��	0,10,10,0q�$��Roboto)�$��14(�$	��Top=�$��Right=�$	��240��  
Foreground#���  $y�Hidden=�$��17������ $ ./File��� $
��Left=�$��
319,10,0,0q�$	��333��$	��106��$	��Top=�.+��� -   
 ��Name$
 	lbl_rmssd��$��RMSSD��$��
0,10,75,70q�$	��120��$��Right=��� -   $
 lbl_lf��$��LF��$
��Left=�$��
10,10,0,70q�$	��120���� -   $
 lbl_hf��$��HF��$
��Left=�$��	10,80,0,0q�$	��120���� -   $
 lbl_sd1��$	��SD1��$
��Left=�$��
10,45,0,35q�$	��120���� -   $
 lbl_sd2��$	��SD2��$��
0,45,75,35q�$��Right=�$	��120���� -	   $
 lbl_d2��$��D2��$��	0,80,75,0q�$��Right=�$	��120��