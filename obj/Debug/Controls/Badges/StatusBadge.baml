   M S B A M L   `   `   `  ????B  >QPacity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:QPacityQPacity  5          QPacity.StatusBadge   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:QPacity     5        ? 	Resources  5         SpinnerAnimation ?? 
TargetName ?? TargetProperty W? From	 W? To g? Duration g? RepeatBehavior  SignInAnimation g? AutoReverse  SignOutAnimation??%Q  &	        &	 ?     &	 ?    ?? 5   
   .??5      W? $ Spinner??5      $9 3(TextBlock.RenderTransform).(RotateTransform.Angle)?5      $ 0X?5      $	 360X?5      $ 0:0:2D?5      $ Forever??5      5      ?? 5   
   .??5      W? $ 
SignInIcon??5      $ Opacity?5      $ 0X?5      $ 1X?5      $ 0:0:0.4D?5       . $ Forever??5!      5"      ?? 5%   
   .??5&      W? $ SignOutIcon??5'      $ Opacity?5(      $ 0X?5)      $ 1X?5*      $ 0:0:0.4D?5+       . $ Forever??5-      5.      5/      .??51      ? $??5q?6   .+?54   
   ?   CoreBlueBrush#??[ 6   y?6;   ?? *ScannerStatus+ ?? 	Converter ;   5QPacity.ScannerStatusToStatusBadgeVisibilityConverter    IntZero	 ?? ConverterParameter#	 [ 
 ? ColumnDefinitions
 55      ?? 56      $??60 ?6#   ?? 57      $??* ?6#   .+?5;      ?? -   5        $??Spinner??5;   _   $??0??6     CoreIconLight#??[ 6(   $	????6O    J? RenderTransformOrigin$ 0.5, 0.5&?6n   z?5<      ?? 5=       ?? Angle$ 0Q?6&   5?      ?? 5B      -   5        $??ScanningText??5B   ?   $??1??6     CoreBody#??[ 6(     CoreLightBrush#??[ 6J   $
??Left=?6w   $#??Scanning for QPacity codes...??6?   5C      ? 5F   
     CoreGreenBrush#??[ 6   y?6<   ?? *ScannerStatus+    
	 IntOne#	 [	 
 5G      ?? 5H      $??60 ?6#   ?? 5I      $??* ?6#   .+?5M      ?? -   5        $??
SignInIcon??5M   _   $??0??6   #??[ 6(   $	????6O   6_   ?? 5P      $??1??6   #??[ 6(   #??[ 6J   $
??Left=?6w   ??6?   ?? *LatestGuest+ 1   +QPacity.LatestGuestToSignInMessageConverter  5Q      ? 5T   
    
 CoreRedBrush#??[
 6   y?6:   ?? *ScannerStatus+    
 IntTwo#	 [ 
 5U      ?? 5V      $??60 ?6#   ?? 5W      $??* ?6#   .+?5[      ?? -   5        $??SignOutIcon??5[   _   $??0??6   #??[ 6(   $	????6O   6_   ?? 5^      $??1??6   #??[ 6(   #??[ 6J   $
??Left=?6w   ??6?   ?? *LatestGuest+ 2   ,QPacity.LatestGuestToSignOutMessageConverter  5_      ? 5b   
     CoreDarkBrush#??[ 6   y?6;   ?? *ScannerStatus+     IntThree#	 [ 
 5c      ?? 5d      $??60 ?6#   ?? 5e      $??* ?6#   .+?5i      ?? $??0??6   #??[ 6(   $	????6O   ?? 5l      $??1??6   #??[ 6(   #??[ 6J   $
??Left=?6w   $,??&Sorry! This room has reached capacity.??6?   5m      ? 5p   
   #??[ 6   y?6;   ?? *ScannerStatus+     IntFour#	 [ 
 5q      ?? 5r      $??60 ?6#   ?? 5s      $??* ?6#   .+?5w      ?? $??0??6   #??[ 6(   $	????6O   ?? 5z      $??1??6   #??[ 6(   #??[ 6J   $
??Left=?6w   $5??/Sorry! This room has been closed to new guests.??6?   5{      5}      5~      