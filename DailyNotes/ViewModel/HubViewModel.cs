using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using DailyNotes.Model;

namespace DailyNotes.ViewModel
{
    class HubViewModel : BaseViewModel
    {
        private Album _albumMisEnAvant;
        private static ObservableCollection<Album> _decouvertesRecentes;
        private ObservableCollection<Album> _recommandesPourVousList;
        private ObservableCollection<Album> _topEcoutes;
        private static HubViewModel _current;

        public static HubViewModel Current
        {
            get
            {
                if(_current == null)
                    _current = new HubViewModel(true);
                return _current;
            }
        }

        public Album AlbumMisEnAvant
        {
            get { return _albumMisEnAvant; }
            set { SetProperty(ref _albumMisEnAvant, value);  }
        }

        public ObservableCollection<Album> DecouvertesRecentes
        {
            get { return _decouvertesRecentes; }
            set { SetProperty(ref _decouvertesRecentes, value); }
        }

        public ObservableCollection<Album> RecommandesPourVousList
        {
            get { return _recommandesPourVousList; }
            set { SetProperty(ref _recommandesPourVousList, value); }
        }

        public bool EstInitialise { get; set; }

        public ObservableCollection<Album> TopEcoutes
        {
            get { return _topEcoutes; }
            set { SetProperty(ref _topEcoutes, value); }

        }

        // On est obligés de le conserver pour le support design time
        [Obsolete("Uniquement pour le designer", true)]
        public HubViewModel()
        {
            EstInitialise = false;

            if (DesignMode.DesignModeEnabled)
            {
                InitialiseDonneesDesignTime();
            }

            DecouvertesRecentes = new ObservableCollection<Album>();
        }

        protected HubViewModel(bool estUnAppelInterne)
        {
            EstInitialise = false;

            if (DesignMode.DesignModeEnabled)
            {
                InitialiseDonneesDesignTime();
            }

            DecouvertesRecentes = new ObservableCollection<Album>();
        }

        private void InitialiseDonneesDesignTime()
        {
            AlbumMisEnAvant = new Album()
            {
                AlbumImageUri =
                    new Uri(
                        "http://cdn-images.deezer.com/images/cover/fe781ecd9879a82beed80f6d3e80745b/500x500-000000-80-0-0.jpg"),
                Titre = "PRISM",
                Artiste = new Artiste()
                {
                    Nom = "Katy Perry",
                    NombreFans = 189282,
                    ArtisteImageUri =
                        new Uri(
                            "http://d1qhhammy2egfp.cloudfront.net/wp-content/uploads/2013/10/katyperry_prism_butterflies_01-1062x1280.jpg")
                }
            };

            TopEcoutes = new ObservableCollection<Album>();

            TopEcoutes.Add(new Album()
            {
                Titre = "racine carrée",
                Artiste = new Artiste() { Nom = "Stromae", ArtisteImageUri = new Uri("http://cdn-images.deezer.com/images/artist/657264c62071008afa7535e7f2390ae2/120x120-000000-80-0-0.jpg") },
                AlbumImageUri = new Uri("http://cdn-images.deezer.com/images/cover/914db9146f330d0a2969d157872da5eb/500x500-000000-80-0-0.jpg"),
                Position = 1,
                Variation = 0
            });

            TopEcoutes.Add(new Album()
            {
                Titre = "Subliminal La face cachée du monde",
                Artiste = new Artiste() { Nom = "Maître Gims" },
                AlbumImageUri = new Uri("http://cdn-images.deezer.com/images/cover/a48a99f7fd9c42d9de770de4543b4b86/500x500-000000-80-0-0.jpg"),
                Position = 2,
                Variation = -1
            });

            TopEcoutes.Add(new Album()
            {
                Titre = "Vieux Frères",
                Artiste = new Artiste() { Nom = "FAUVE" },
                AlbumImageUri = new Uri("http://cdn-images.deezer.com/images/cover/86eb856359d225884203a4aa6be3b1ab/500x500-000000-80-0-0.jpg"),
                Position = 3,
                Variation = 1
            });


            RecommandesPourVousList = new ObservableCollection<Album>()
            {
                new Album()
                {
                    Titre = "Delta Machine",
                    Artiste = new Artiste()
                    {
                        Nom = "Depeche Mode"
                    },
                    AlbumImageUri = new Uri("http://cdn-images.deezer.com/images/cover/063d0b1eccd4d08ed2ebad075b754a05/500x500-000000-80-0-0.jpg")
                },
                new Album()
                {
                    Titre = "Little Red",
                    Artiste = new Artiste()
                    {
                        Nom = "Katy B"
                    },
                    AlbumImageUri = new Uri("http://cdn-images.deezer.com/images/cover/5d15c32ac58649685741bcb9c0bd4c79/500x500-000000-80-0-0.jpg")
                },
                new Album()
                {
                    Titre = "The Golden Age",
                    Artiste = new Artiste()
                    {
                        Nom = "Woodkid"
                    },
                    AlbumImageUri = new Uri("http://cdn-images.deezer.com/images/cover/0efc3bf3edc856f298cb259124e1474f/500x500-000000-80-0-0.jpg")
                }
            };    
        }

        public async Task Load()
        {
            if (EstInitialise)
                return;


            DeezerProvider deezer = new DeezerProvider();
            var topEcoutes = await deezer.GetTopAlbums();
            TopEcoutes = new ObservableCollection<Album>(topEcoutes.Take(5));

            // TODO: Loader l'album depuis le réseau
            AlbumMisEnAvant = new Album()
            {
                AlbumImageUri =
                    new Uri(
                        "http://cdn-images.deezer.com/images/cover/fe781ecd9879a82beed80f6d3e80745b/500x500-000000-80-0-0.jpg"),
                Titre = "PRISM",
                Artiste = new Artiste()
                {
                    Nom = "Katy Perry",
                    NombreFans = 189282,
                    ArtisteImageUri =
                        new Uri(
                            "http://d1qhhammy2egfp.cloudfront.net/wp-content/uploads/2013/10/katyperry_prism_butterflies_01-1062x1280.jpg")
                }
            };

            // SACHA: Remplace par tes albums
            int[] albumsRecosIds = new[] { 7361221, 707980, 1262368, 6886576, 6240279};
            RecommandesPourVousList = new ObservableCollection<Album>();

            foreach (int albumsRecosId in albumsRecosIds)
            {
                RecommandesPourVousList.Add(await deezer.GetAlbum(albumsRecosId));
            }

            UpdateTile();

            EstInitialise = true;
        }

        // Cette méthode permet de mettre à jour la tuile principale de l'application, 
        // une fois qu'elle est mise en format rectangulaire.
        private void UpdateTile()
        {
            try
            {
                // La classe TileUpdate nous permet d'envoyer plusieurs tuiles qui vont s'enchaîner naturellement.
                TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.EnableNotificationQueue(true);
                updater.Clear();

                // Nous allons simplement ajouter 5 tuiles, mais la liste peut être plus grande.
                int itemCount = 0;

                // Création d'une tuile par album dans la recommendation.
                foreach (var item in RecommandesPourVousList)
                {
                    // On récupère le template XML qui décrit une tuile large, avec une image, un titre et un sous-titre.
                    XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150SmallImageAndText02);

                    // On remplit ce XML avec les informations de l'album en cours.
                    string titleText = "Par " + item.Artiste.Nom;
                    tileXml.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = item.AlbumImageUri.AbsoluteUri;
                    tileXml.GetElementsByTagName("text")[0].InnerText = item.Titre;
                    tileXml.GetElementsByTagName("text")[1].InnerText = titleText;

                    // On ajoute la tuile en cours dans la liste des tuiles à donner au système.
                    updater.Update(new TileNotification(tileXml));

                    // Si nous avons nos 5 éléments, on s'arrête là.
                    if (itemCount++ > 5) break;
                }
            }
            catch (Exception)
            {
               // On fait rien, c'est de la démo :)
            }
           
        }
    }
}
