   M S B A M L   `   `   `  ����G  CImageManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ImageManagerImageManager  5   	   =#clr-namespace:ImageManager.BehaviorImageManager.Behavior  5	   	   ;"clr-namespace:ImageManager.ControlImageManager.Control  5
   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 5   	   H DCommonToolKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:CommonToolKit;assembly=CommonToolKitCommonToolKit 5   	       ImageManager.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ImageManager     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cie:http://schemas.microsoft.com/expression/2010/interactivity  5   	   ,bh#clr-namespace:ImageManager.Behavior  5	   	   +ct"clr-namespace:ImageManager.Control  5
   	   Bscm8clr-namespace:System.ComponentModel;assembly=WindowsBase  5   	   ;ex2clr-namespace:CommonToolKit;assembly=CommonToolKit  5   	   b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  6G     � Title$  图管��5   	   $	��600��6   $	��800��6!     CommonToolKit.ExWindow   TitlebarModel .    TitlebarHeight$ 36Q�6B   
����������	#FF2D2D30$��15(�5   /    � DataContext 5   	   ��    Locator �� Source# [   �� Path$
 Main� � 	Resources 5        DateGroupedData �� Source �� GroupDescriptions2  ,System.Windows.Data.PropertyGroupDescription	   PropertyName  DateTimeToDateConverter
   	Converter �� SortDescriptions+  %System.ComponentModel.SortDescription   PropertyName   	Direction��%�   &	       7  �� 5   
    67   �� *	IamgeList+ 5        5      $
	 Date��6+   8
   6?    5        5      $
 Date��6&   $ 
Descending=�6:     5      $
 Name��6&   $ 
Descending=�6:   5      5         TitlebarLeftContent 5      �� 5   
   $��42��6     Button_CanHover#��[ 5       y�5!      �� # [  $ ImageView.Viewable���5"      �� # [  $* $ImageView.CloseImageViewPanelCommand�.��5$      ��   Icon_Awesome_Solid#��[ 6   $	����6D   
���  ��$��16(�6e   5%      .��5(      � .+�5)   
   ��   TitleTabPanelStyle#��[ 6     TitleTabItemStyle#��[ 5*      .@�5+      �� $��图像��6   .��5-      � .+�5.      ��  �� Orientation$ 
Horizontal=�6!   $��48��6;   $��Right=�5/   !   $	��Top=�6=   $��0,0,20,0q�50   !   .+�51      �� #��[ 6!   $��48��6J   $��500.0��6~   ��6U   �� *ViewItemHeightCommad+.��52      �� $	����6(     Icon_Awesome_Regular#��[ 68   ���	#FF464646$��12(�6}   53      �� 54      #��[ 6!   $��48��6J   $��300.0��6~   ��6U   �� *ViewItemHeightCommad+.��55      �� $	����6(   #��[ 68   ���	#FF464646$��12(�6}   56      �� 57      #��[ 6!   $��48��6J   $��100.0��6~   ��6U   �� *ViewItemHeightCommad+.��58      �� $	����6(   #��[ 68   ���	#FF464646$��12(�6}   59      5:      �� 5<       %� CacheLength@  :System.Windows.Controls.VirtualizationCacheLengthConverter$ 1 6   $��0,48,0,0q�6A    %� CacheLengthUnit$
 Item=�5=       %� VirtualizationMode$ Standard=�5>       %� IsVirtualizing . ! %� IsVirtualizingWhenGrouping .  %� 
ScrollUnit$ Pixel=�5A        ImageItemStyle#��[ 5B       	 ImageInfoStyle#��[	 5D      $��Disabled=�5E      ��. $��0q�5G      ��5C      �� # [ ��5H      �� 5I      .R�5J      )   #ImageManager.Control.ImageWrapPanel   K� IsItemsHost . $��Stretch=�5M   !   $ 1 5N   !   $
 Item=�5O   !   $ Standard=�5P   !    .  . $ Pixel=�5S   !   ��.    
LineHeight 5K   !   ��  �� RelativeSource ��  �� AncestorType$ WindowL�$ DataContext.ItemHeight�.  (System.Windows.Interactivity.Interaction   	Behaviors 5V   "   9  3ImageManager.Behavior.WrapPanelItemAutoSizeBehavior  5W   &   5Y      5[       �� 
GroupStyle 5j      (  "System.Windows.Controls.GroupStyle  5k         Panel 5m      �� 5n   "   .R�5o   &   $�  .  $� Orientation$ Vertical=�6P   ��.  . $ Pixel=�5r   1   $ 1 5s   1   $
 Item=�5t   1   $ Standard=�5u   1   $��Stretch=�5v   1   5y   '   5z   #      ContainerStyle 5}      �� 5~   "    �� 
TargetType" ��6(   .�5   &   ��   �� Property  � ��! �� Value! 5�   *   �� 5�   .   " �� 
TargetType"" ��6>   .R�5�   2   �� $ Vertical=�6=   $��10q�6T   .+�5�   6   �� # �� TextAlignment$
# Left=�5�   @   $��16(�6U   $��Black"�6c   
�������$��10q�6�   ��5�   @   �� $
 Name�$ �� StringFormat$$ {0:yyyy年MM月dd日}��5�   �   �� 5�   6   $��	10,0,0,10q�6E   5�   3   5�   /   5�   '   5�   #   5�      5�      5�      5�      �� 5�      $��库��6   .��5�      �� $��0q�6    
 LibItemStyle#��[
 5�        LibStySelector#��[ 5�      $��Disabled=�5�      ��5�      �� *	LibList+��5�      �� 5�      .R�5�      �  . 6(   5�      5�      5�      �� 5�      $��查找��6   .��5�      �  6   �� # [  $ 
SearchView�% � RowDefinitions% 5�      �� 5�      $
��Auto �6(   �� 5�      .+�5�      �� $��5q�6   
�������$��Center=�6;   $	��240��6X   $��1q�6d   ���	#FF007ACC& �� CornerRadius$& 5��6�   ._�5�      � ' � ColumnDefinitions' 5�      �� 5�   "   �� 5�   "   $
��Auto �63   .+�5�      �� -   5        ( ��Name$( PART_SearchText��5�   &   
������ $��0q�6X   $��Center=�6l   $	��5,0q�6�   ) J� 	AllowDrop) . .��6�   * J� InputBindings* 5�   "   �� 5�   &   
+ �� Key$+ Return��61   , �� Command, 6>   �� *SearchCommand+- �� CommandParameter- 6`   �� . �� ElementName$. PART_SearchText��$
 Text�.��5�   9   /   Triggers/ 5�   "   /	  )System.Windows.Interactivity.EventTrigger	  5�   &   0 	  	EventName$0 PreviewDragOver��66   .
  (System.Windows.Interactivity.TriggerBase1 
  Actions.1 5�   *   /  )GalaSoft.MvvmLight.Command.EventToCommand  2   PassEventArgsToCommand2 . 3   Command3 6B   �� *DragOverCommand+6f   5�   '   	  5�   &   $0 PreviewDrop��66   .1 5�   *     2 . 3 6B   �� *DropCommand+6b   5�   '   .��5�   ;   5�      �� 5�      #��[ 6%   $��32��6N   $��32��6Y   $��1��6e   $��0q�6u   ��5�   %   �� *SearchCommand+��6G   �� $. PART_SearchText��$
 Text�.��5�   "   �� $	����6,   $��14(�6<   #��[ 6J   
���   �6v   5�      5�      5�      �� 5�      $ 
Horizontal=�6!   $��48��6;   $��Right=�5�   !   $	��Top=�6=   $��0,0,20,0q�5�   !   .+�5�      �� #��[ 6!   $��48��6J   $��500.0��6~   ��6U   �� *ViewItemHeightCommad+.��5�      �� $	����6(   #��[ 68   ���	#FF464646$��12(�6}   5�      �� 5�      #��[ 6!   $��48��6J   $��300.0��6~   ��6U   �� *ViewItemHeightCommad+.��5�      �� $	����6(   #��[ 68   ���	#FF464646$��12(�6}   5�      �� 5�      #��[ 6!   $��48��6J   $��100.0��6~   ��6U   �� *ViewItemHeightCommad+.��5�      �� $	����6(   #��[ 68   ���	#FF464646$��12(�6}   5�      5�      �� 5�      $��1��5�      $ 1 5�      $
 Item=�5�      $ Standard=�5�       .  . $ Pixel=�5�      #��[ 5�      #��[	 5�      $��Disabled=�5�      ��. $��0q�5�      ��5�      �� *SearchResult+��5�      �� 5�      .R�5�   "      . $��Stretch=�5�   !   $ 1 5�   !   $
 Item=�5�   !   $ Standard=�5�   !    .  . $ Pixel=�5�   !   ��.  5�   5   ��  �� $ ListViewL�$ DataContext.ItemHeight� 5�   &     5�   *   5�   #   5�      5�      5�      5�      5�      �� 5   
    6   �� # [  $ 	ImageView�.u�5     � 
�������y�6   �� *
Viewable+% 5     �� 5     $
��Auto �6$   �� 5     �� 5     $
��Auto �6$   ' 5     �� 5	     $
��Auto �6'   �� 5
     .+�5     �� $��1��6   $��1��6&   
�������* 5     d� 5     4 d� MouseAction$4 LeftDoubleClicke�6'   , 6E   �� *ViewResetCommand+- 5  '   �� $. PART_DetailCanvas��._�5     �� -   5        5 ��Name$5 PART_DetailCanvas��5     $
��Auto=�5     $
��Auto=�5     +�5     �� 5     6 �� Left$6 0��6N   
7 �� Top$7 0��6^   8 J� RenderTransformOrigin$8 0.5,0.5&�6m   ��6$   �� *ImageTarget.SourcePath+z�5  "   V� 5  &   .~�5  *   �� 9 �� Angle9 6:   �� *Angle+�� 5  *   : �� ScaleX: 69   �� *Scale+; �� ScaleY; 6R   �� *Scale+5  '    5  "   2   ,ImageManager.Behavior.CanvasDragMoveBehavior  5  &   7  1ImageManager.Behavior.AwaysAtCanvasCenterBehavoir  5   &   5"     5$     5%     �� 5'     
�������$��1��6,   $��2��6<   ._�5(     �� $ 
Horizontal=�6!   $��Center=�6;   $��46��6X   .+�5*     d� -   5        < d�Name$< PART_InfoVisible��5*  (   = d� 	IsChecked$= FalseY�6B     Toggle_Frame#��[ 6T   > � Cursor$
> Hand��6z   
������ $��0q�5+  A   $��48��6U   .��5,     �� $	����6(   
���   �#��[ 5-  (   5.     �� 50     $��48��6"   �� 52     $��48��6"   ? � ToolTip$? 删除��6-   #��[ 53  "   ��54  !   �� *DeleteImageViewPanelCommand+.��55     �� #��[ 6(   $	����56  (   
���   �68   57     �� 58     $��48��6"   $? 	左旋转��6-   #��[ 59  "   ��5:  "   �� *!LeftRotateImageViewPanelCommand+.��5;     �� #��[ 6(   $	����5<  (   
���   �68   5=     �� 5?     $��48��6"   $? 	右旋转��6-   #��[ 5@  "   ��5A  "   �� *" RightRotateImageViewPanelCommand+.��5B     �� #��[ 6(   $	����5C  (   
���   �69   5D     �� 5E     $��48��6"   $? 放大��6-   #��[ 5F  "   ��5G  "   �� *ZoominImageViewPanelCommand+.��5H     �� #��[ 6(   $	����5I  (   
���   �69   5J     �� 5K     $��48��6"   $? 缩小��6-   #��[ 5L  "   ��5M  "   �� *ZoomoutImageViewPanelCommand+.��5N     �� #��[ 6(   $	����5O  (   
���   �68   5P     5R     5V     �� 5Y     $��2��6   $	��360��6*   
�������
���   �$
��Auto=�5[  K   y�5Z     �� $. PART_InfoVisible��$ 	IsChecked�  BoolToVisibilityConverter@ �� 	Converter#@ [ ._�5\     � $��15q�6   % 5]     �� 5^     $��64 �6,   �� 5_     $
��Auto �6,   �� 5`     $
��Auto �6,   �� 5a     $
��Auto �6,   �� 5b     $
��Auto �6,   �� 5c     $
��Auto �6,   �� 5d     $
��Auto �6,   �� 5e     $
��Auto �6,   �� 5f     $
��Auto �6,   �� 5g     $
��Auto �6,   �� 5h     $
��Auto �6,   �� 5i     $
��Auto �6,   �� 5j     $
��Auto �6,   �� 5k     $
��Auto �6,   �� 5l     $
��Auto �6,   �� 5m     $
��Auto �6,   �� 5n     $
��Auto �6,   �� 5o     $
��Auto �6,   �� 5p     $
��Auto �6,   �� 5q     $
��Auto �6,   �� 5r     $��20��6,   .+�5u     �� $��文件信息��6$   $��Center=�60   $��18(�6K   
���iii�6Y   d� 5w     #��[ 6i   
������ $��0q�5x  6   $��32��6J   $��32��6U   $
> Hand��6a   $��Right=�6o   = 5w  '   �� $. PART_InfoVisible��$ 	IsChecked�.��5y     �� $	����6(   $��18(�68   #��[ 6F   
���iii�6t   5z     �� 5|     $��1��6$   $��拍摄时间：��61   
���iii�6?   �� 5}     $��2��6$   $��32��61   ��6=   �� *ImageTarget.Date+$$ {0:yyyy/MM/dd HH:mm:ss}���� 5     $��3��6$   $��分辨率：��61   
���iii�6>   �� 5�     $��4��6%   $ 
Horizontal=�62   .+�5�     �� $��32��6)   ��65   �� *ImageTarget.Width+�� 5�     $��x��6)   �� 5�     $��32��6)   ��65   �� *ImageTarget.Height+5�     �� 5�     $��5��6$   $��修改时间：��61   
���iii�6?   �� 5�     $��6��6$   $��32��61   ��6=   �� *ImageTarget.ModifiedDate+$$ {0:yyyy/MM/dd HH:mm:ss}���� 5�     $��7��6$   $��ISO：��61   
���iii�6>   �� 5�     $��8��6$   $��32��61   $
��Wrap=�6^   ��6=   �� *ImageTarget.ISO+6^   �� 5�     $��9��6$   $��	设备：��61   
���iii�6=   �� 5�     $��10��6$   $��32��62   $
��Wrap=�6g   ��6>   �� *ImageTarget.CameraModel+6g   �� 5�     $��11��6$   $��	镜头：��62   
���iii�6>   �� 5�     $��12��6$   $��32��62   $
��Wrap=�6e   ��6>   �� *ImageTarget.LensModel+6e   �� 5�     $��13��6$   $��闪光灯：��62   
���iii�6?   �� 5�     $��14��6$   $��32��62   $
��Wrap=�6c   ��6>   �� *ImageTarget.FlashON+6c   �� 5�     $��15��6$   $��存储路径：��62   
���iii�6@   �� 5�     $��16��6$   $
��Auto��62   $
��Wrap=�6h   ��6@   �� *ImageTarget.SourcePath+6h   �� 5�     $��17��6!   $
��Auto��6/   $��32��6<     Button_Frame#��[ 6H   .��5�     �� $��打开路径��6(   $
> Hand��64   
��� E��6B   5�     � 5�     $��18��6   $	��0,6q�6-   .+�5�     �� $��标签��6(   
���iii�62   d� 5�     -   5        $< PART_AddTogg��5�  +   $��Right=�6B   $��Center=�6^   $��0,0,7,0q�6y     TagToggleStyle#��[ 6�   .��5�  "   �� $	����6-   $��18(�6=   #��[ 5�  -   ��5�  -   ��  �� $ ToggleButtonL�$ 
Foreground�5�     5�     �� 5�     $ 
Horizontal=�6%   $��19��6>   $��32��6L   y�6X   �� $. PART_AddTogg��$ 	IsChecked�#@ [ .+�5�     �� -   5        $( PART_AddTag��5�  >   $	��280��6&   $��26��62   $��Center=�6S   �� 5�     #��[ 6%   $
��10,0q�6K   $? 添加��6Y   ��5�  %   �� *AddTagCommand+��6G   �� $. PART_AddTag��$
 Text�.��5�  "   �� $	����6,   $��18(�6<   #��[ 6J   
������6x   5�     5�     �� 5�     $��20��6#     ImageTagItemStyle#��[ 5�  #     ImageTagsItemDataStyle#��[ 5�  #   ��5�  #   �� *	ImageTags+��5�     �� 5�  "   .R�5�  &   � 5�  #   5�     5�     A J� EffectA 5�     3  -System.Windows.Media.Effects.DropShadowEffect  5�     B   
BlurRadius$B 5Q�6+   C   ShadowDepth$C 0Q�6:   5�     5�     5�     5�     5�     