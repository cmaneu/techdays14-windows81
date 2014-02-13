using System;

namespace DailyNotes.Model
{
    public class Artiste : BaseModele
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Uri ArtisteImageUri { get; set; }
        public Uri ArtisteImageFondUri { get; set; }
        public int NombreFans { get; set; }
    }
}
