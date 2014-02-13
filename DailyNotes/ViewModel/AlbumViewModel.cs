using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using DailyNotes.Model;

namespace DailyNotes.ViewModel
{
    // Cette classe permet de préparer l'ensemble des éléments nécessaires à l'affichage de la page album.
    class AlbumViewModel : BaseViewModel
    {
        private string _titreAlbum;
        private string _nomArtiste;
        private Uri _albumCoverUri;
        private Uri _artisteImageUri;
        private IEnumerable<Track> _titres;
        private Artiste _artiste;
        private Album _album;

        public Artiste Artiste
        {
            get { return _artiste; }
            set { SetProperty(ref _artiste, value); }
        }

        public Album Album
        {
            get { return _album; }
            set { SetProperty(ref _album, value); }
        }

        public Uri AlbumCoverUri
        {
            get { return _albumCoverUri; }
            set { SetProperty(ref _albumCoverUri, value); }
        }

        public Uri ArtisteImageUri
        {
            get { return _artisteImageUri; }
            set { SetProperty(ref _artisteImageUri, value); }
        }

        public IEnumerable<Track> Titres
        {
            get { return _titres; }
            set { SetProperty(ref _titres, value); }
        }

        public AlbumViewModel()
        {
            // Si notre code est exécuté par Visual Studio ou Blend, on charge des données de démo, 
            // afin d'avoir un rendu réaliste dans les designers
            if (DesignMode.DesignModeEnabled)
            {
                LoadWithDemoData();
            }
        }

        public async Task LoadAlbum(int albumId)
        {
            DeezerProvider provider = new DeezerProvider();
            Album album = await provider.GetAlbum(albumId);
            Album = album;
            AlbumCoverUri = album.AlbumImageUri;
            Artiste = album.Artiste;
            
            Titres = album.Pistes;

            if (!HubViewModel.Current.DecouvertesRecentes.Contains(album))
            {
                HubViewModel.Current.DecouvertesRecentes.Insert(0, album);
            }
        }



        private void LoadWithDemoData()
        {
            Album = new Album()
            {
                Titre =  "If You Wait", 
                Artiste = new Artiste()
                {
                    Nom = "London Grammar",
                    ArtisteImageUri = new Uri("http://cdn-images.deezer.com/images/artist/6bd8526d5ae7fe503a78348e1b57fadc/120x120-000000-80-0-0.jpg")
                }
            };

            ArtisteImageUri = new Uri("http://distilleryimage10.ak.instagram.com/60d963f84f0811e38c5d12d02d33708a_8.jpg");
            AlbumCoverUri = new Uri("http://cdn-images.deezer.com/images/cover/615db2012ca3877aee3da15e5e7f7017/500x500-000000-80-0-0.jpg");
            List<Track> chansons = new List<Track>();
            chansons.Add(new Track(){Number = 1, Title = "If you Wait", Duration = TimeSpan.FromSeconds(243)});
            chansons.Add(new Track(){Number = 2, Title = "NightCall", Duration = TimeSpan.FromSeconds(232)});
            chansons.Add(new Track(){Number = 3, Title = "One Day", Duration = TimeSpan.FromSeconds(253)});
            Titres = chansons;

            Artiste = Album.Artiste;
        }
    }
}
