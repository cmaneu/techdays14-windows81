using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238
using DailyNotes.Model;
using DailyNotes.ViewModel;

namespace DailyNotes
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HubViewModel _viewModel;

        public MainPage()
        {
            this.InitializeComponent();
            _viewModel = HubViewModel.Current;
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (_viewModel.EstInitialise == false)
            {
                Task chargementViewModelTask = _viewModel.Load();
            }
        }

        private void OnAlbumMisEnAvantTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AlbumPage), 7040437);
        }

        private void OnTopEcoutesAlbumTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if(element == null)
                return;

            Album album = element.DataContext as Album;
            if(album == null)
                return;

            // Cette ligne permet de naviguer vers la page album
            Frame.Navigate(typeof (AlbumPage), album.Id);
        }
    }
}
