using Newtonsoft.Json;

namespace Guideviewer
{
    public class Quest
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("difficulty")]
        public long Difficulty { get; set; }

        [JsonProperty("members")]
        public bool Members { get; set; }

        [JsonProperty("questPoints")]
        public long QuestPoints { get; set; }

        [JsonProperty("userEligible")]
        public bool UserEligible { get; set; }
    }
}
