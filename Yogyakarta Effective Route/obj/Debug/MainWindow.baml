   M S B A M L   `   `   `  ����d  `Microsoft.Maps.MapControl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPFMicrosoft.Maps.MapControl.WPF  5   	    Yogyakarta Effective Route+   %Yogyakarta_Effective_Route.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   `mRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF     5   	     � Title$   Yogyakarta Effective Route��5   	   $	��768��6,   $
��1024��69    � WindowState$ 	Maximized=�6F    � WindowStartupLocation$ CenterScreen=�6^   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5   
   �� $
��Left=�6   .+�5      �� $%��Choose places you want to visit��6   6A   �� 5	      -   5         ��Name$ lvTouristObjects��5	   2   $
��Left=�6   $	��Top=�6L   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6|   ��6d   ��  �� View 5
      �� 5      P�5      �� 5       �� CellTemplate 5   "   �� 5   &   .R�5   *   �� 
 � Tag 63   �� *ID+	 d� 	IsChecked	 6F   �� 
 �� RelativeSource
 ��  �� AncestorType" �� �� Path$ 
IsSelected�5   '   5      �� 5      $
��Name��6S    �� DisplayMemberBinding 6-   �� *Name+6S   �� 5      $��Description��6Z    6-   �� *Description+6Z   5      5      �� 5      -   5         ��Name$ 
bCalculate��5      $��Calculate Best Route��6)   6c   �� 5      -   5         ��Name$ ResultPanel��5      5      5      '   !Microsoft.Maps.MapControl.WPF.Map  5   
   -   5          Name$ mRoute��5      $	��850��6    +   %Microsoft.Maps.MapControl.WPF.MapCore   CredentialsProviderT   NMicrosoft.Maps.MapControl.WPF.Design.ApplicationIdCredentialsProviderConverter$F @AgchoLh-mhsNBNEjRvUTN0kKhoNOsc_nnezXlasG6FJArss7xgt38h5cNp99N814 6,      Mode;   5Microsoft.Maps.MapControl.WPF.Design.MapModeConverter$
 Road 6�      	ZoomLevel$ 12Q�6�      Center<   6Microsoft.Maps.MapControl.WPF.Design.LocationConverter$ -7.7987857,110.3671027 6�   6�   5      5       