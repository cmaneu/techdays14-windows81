using System;

namespace DailyNotes.Model
{
    public class Track
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public Uri PreviewUri { get; set; }
    }
}
