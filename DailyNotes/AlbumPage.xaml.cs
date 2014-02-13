using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using DailyNotes.Model;
using DailyNotes.ViewModel;

namespace DailyNotes
{

    public sealed partial class AlbumPage : Page
    {
        private AlbumViewModel _dataContext;

        public AlbumPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter == null)
            {
                MessageDialog dialog = new MessageDialog("Impossible de charger cet album.");
                await dialog.ShowAsync();
                Frame.GoBack();
            }

            // Chargement des données
            _dataContext = new AlbumViewModel();
            await _dataContext.LoadAlbum((int) e.Parameter);
            DataContext = _dataContext;
        }

        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void OnListItemTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement listElement = sender as FrameworkElement;
            if(listElement == null)
                return;

            Track tappedTrack = listElement.DataContext as Track;
            if(tappedTrack == null)
                return;

             App.Instance.AppRootVisual.PlayTrack(tappedTrack, _dataContext.Album);
        }
    }
}
