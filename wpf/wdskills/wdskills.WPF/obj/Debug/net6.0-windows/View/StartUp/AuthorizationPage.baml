   M S B A M L   `   `   `  ????G  Cwdskills.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:wdskills.WPF.View.StartUpwdskills.WPF.View.StartUp  5      1    +wdskills.WPF.View.StartUp.AuthorizationPage   5      Z VDevExpress.Mvvm.UI, Version=22.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:wdskills.WPF.View.StartUp     5        N? Title$  Авторизация??5
       ? DataContext 5      ?? *AuthViewModel+   ViewModelLocator ?? Source# [  .??5      ?  ? RowDefinitions 5   
   ?? 5      $??80 ?6   ?? 5      $??* ?6   ?? 5      $??80 ?6   .+?5   
   ? $??0??6   .+?5      ?? $??0??6   $??Авторизация??6%   $??24(?68     TextBlockTittle#??[ 6F   5      ? 5   
   $??1??6    5      ?? 5      $??50 ?6    6.   ?? 5      6"   ?? 5      6"   ?? 5      $
??auto ?6    60   ?? 5      $
??auto ?6    60   ?? 5      $
??20px ?6    60   .+?5      ?? $??0??6   $??Center=?6%   ??6B   ?? *ErrorMessage+?? 5      $??1??6   $??
Логин??6%   $??50 0 0 0q?62   ?? 5      $??1??6   $??Center=?6#   $??0 0 0 5q?6>   ??6O   ?? *Login+?? 5       $??2??6   $??Пароль??6%   $??50 0 0 0q?63   ?? 5!      $??2??6   $??Center=?6#   $??0 5 0 0q?6>   ??6O   ?? *
Password+?? 5"      $??3??6   $??300px??6!   $??100px??6/   ??6A   ?? *ImageCaptcha+  imageSourceConverter ?? 	Converter# [ y?6?   ?? *SendCaptcha+  boolToVisibilityConverter# [ ?? 5#      $??4??6   $??Center=?6#   $??
0 20px 0 5q?6>   $
??auto??6?   ??6R   ?? *InputCaptcha+y?6p   ?? *SendCaptcha+# [ 6?   ?? 5$      $??5??6   $??Bottom=?6"   $(??"У вас нет аккаунта???6d     
ButtonLink#??[ 6?   ??6=   ?? *RedirectToRegister+6?   5%      ?? 5&   
   $??2??6   $??
Войти??6>   ??6   ?? *JoinAccount+6>   5'      5(      