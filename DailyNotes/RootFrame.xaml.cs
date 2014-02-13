using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DailyNotes.Model;

namespace DailyNotes
{
    public sealed partial class RootFrame : Page
    {
        public RootFrame()
        {
            this.InitializeComponent();
        }

        private void OnRootFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RootFrame - Navigation failed to " + e.SourcePageType);
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RootFrame - Navigated to " + e.SourcePageType);
        }

        private void OnRootFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RootFrame - Navigating to " + e.SourcePageType);
        }

        public async void PlayTrack(Track track, Album album)
        {
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => PlayerElement.Source = track.PreviewUri);

            ToastHelper.ShowToast("A l'écoute", string.Format("{0} de {1}", track.Title, album.Artiste.Nom), album.AlbumImageUri.AbsoluteUri);
        }

        private void Element_OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media failed: " + e.ErrorMessage);
        }

        private void Element_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media opened.");
        }

        private void Element_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media state changed: " + PlayerElement.CurrentState);
        }
        
        private void PlayerElement_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media Ended");
        }
    }
}
