using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DailyNotes.Model
{
    /// <summary>
    /// Cette classe permet d'effectuer des appels à l'API Deezer et de récupérer les résulats.
    /// Si vous voulez explorer cette API, rendez-vous sur http://developers.deezer.com.
    /// Si vous voulez l'utiliser depuis un projet .net, rendez-vous sur https://github.com/cmaneu/deezer-net-api
    /// </summary>
    class DeezerProvider
    {
        private const string API_ENDPOINT = "http://api.deezer.com/";
        private HttpClient _httpClient;

        public DeezerProvider()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Album>> GetTopAlbums(int categoryId = 0)
        {
            string responseContent = await this.ExecuteHttpGet(string.Format("/editorial/{0}/charts", categoryId));

            JObject response = JsonConvert.DeserializeObject(responseContent) as JObject;

            int position = 1;
            Random r = new Random();
            IEnumerable<Album> albums = from album in response["albums"]["data"]
                            select new Album()
                            {
                                Id            = (int) album["id"],
                                Titre         = album["title"].ToString(),
                                AlbumImageUri = new Uri(album["cover"].ToString()+"&size=500x500"),
                                Position      = position++,
                                Variation     = r.Next(-1,1),
                                Artiste = new Artiste()
                                {
                                    Id = (int)album["artist"]["id"],
                                    Nom = album["artist"]["name"].ToString(),
                                    ArtisteImageUri = new Uri(album["artist"]["picture"].ToString())
                                }
                            };

            return albums;
        }

        public async Task<Album> GetAlbum(int albumId)
        {
            string responseContent = await this.ExecuteHttpGet(string.Format("/album/{0}", albumId));

            JObject response = JsonConvert.DeserializeObject(responseContent) as JObject;
            
            Album album = new Album();
            album.Id = (int)response["id"] ;
            album.Titre = response["title"].ToString();
            album.AlbumImageUri = new Uri(response["cover"] + "&size=800x800");
            album.Artiste = new Artiste();
            album.Artiste.Id = (int) response["artist"]["id"];
            album.Artiste.Nom = response["artist"]["name"].ToString();
            album.Artiste.ArtisteImageUri = new Uri(response["artist"]["picture"].ToString());
            album.Artiste.ArtisteImageFondUri = GetAlternateArtistImageUri(album.Artiste.Id);

            int trackNumber = 1;
            album.Pistes = from track in response["tracks"]["data"]
                select new Track()
                {
                    Number = trackNumber++,
                    Title = track["title"].ToString(),
                    Duration = TimeSpan.FromSeconds((int)track["duration"]),
                    PreviewUri = new Uri(track["preview"].ToString())
                };

            return album;
        }
        
        public Uri GetAlternateArtistImageUri(int artistId)
        {
            switch (artistId)
            {
                case 144227:    // Katy Perry
                    return new Uri("http://d1qhhammy2egfp.cloudfront.net/wp-content/uploads/2013/10/katyperry_prism_butterflies_01-1062x1280.jpg");
                case 1816:      // Girls in Hawaii
                    return new Uri("http://cdn-images.deezer.com/images/cover/8ca5a4fcce557319b177a0b5a11406e3/1000x1000-000000-80-0-0.jpg");
                case 311820:    // Ellie goulding
                    return new Uri("http://static.rukkus.com/blog/wp-content/uploads/2013/11/Ellie-Goulding.jpg");
                case 678:       // M83
                    return new Uri("http://funkyoudear.com/wp-content/uploads/2012/06/m83.jpg");
                case 4771546:   // London Grammar
                    return new Uri("http://www.crumbmagazine.com/wp/wp-content/uploads/2013/10/london-grammar2.jpg");
                case 416239:    // Imagine Dragons
                    return new Uri("http://vegasseven.com/files/2013/12/imagine_dragons_by_anthony_mair_WEB.jpg");
                case 310260:    // Stromae
                    return new Uri("http://media.zenithlimoges.com/medias/Image/stromae_2013.jpg");
                case 4429712:   // Maître Gimms
                    return new Uri("http://rap-game.org/wp-content/uploads/2013/05/maitre-gims.jpg");
                case 56895:     // FAUVE
                    return new Uri("http://bewaremag.com/wp-content/uploads/2013/04/Fauve-Blizzard-21.jpg");
                case 27:        // Daft Punk
                    return new Uri("http://www.mtv.com/shared/promoimages/bands/d/daft_punk/a_z/2007/cr_Daft_Life_Ltd/robot_photo.jpg");
                case 293585:    // Avicii
                    return new Uri("http://userserve-ak.last.fm/serve/500/91216321/Avicii.png");
            }
            return null;
        }

        internal async Task<string> ExecuteHttpGet(string method)
        {
            HttpResponseMessage httpResponse = await this._httpClient.GetAsync(API_ENDPOINT + method);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }

            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
