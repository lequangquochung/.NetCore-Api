using System.Collections.Generic;

namespace CoreCodeCamp.Model
{
    public class TalksModel
    {
        public int TalkId { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Level { get; set; }

        public SpeakersModel Speaker { get; set; }
    }
}