using Newtonsoft.Json;

namespace Library
{
    //Reference to decide if a quest has been completed or not
    public enum Status { Completed, NotStarted, Started };
	public enum CheckBoxType { SelectAll, Mqc, Comp, Trim };

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
