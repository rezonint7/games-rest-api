namespace WEB_API.Models {
    public class SystemRequirements {
        public string GameId { get; set; }
        public Game Game { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string HDD { get; set; }
        public string OS { get; set; }
        public string RAM { get; set; }
    }
}
