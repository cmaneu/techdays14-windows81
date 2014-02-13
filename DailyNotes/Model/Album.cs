using System;
using System.Collections.Generic;

namespace DailyNotes.Model
{
    public class Album : BaseModele
    {
        public int Id { get; set; }
        public Uri AlbumImageUri { get; set; }
        public string Titre { get; set; }
        public Artiste Artiste { get; set; }
        public int Position { get; set; }
        public int Variation { get; set; }
        public IEnumerable<Track> Pistes { get; set; }
    }
}
