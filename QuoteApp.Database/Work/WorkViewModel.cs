namespace QuoteApp.Database.Work
{
    public class WorkViewModel
    {
        [Required]
        public int WorkId { get; set; }

        [Required]
        public string WorkName { get; set; }

        [Required]
        public string WorkDescription { get; set; }

        [Required]
        public int WorkPrice { get; set; }

        [Required]
        public int WorkAreaId { get; set; }

        public string WorkAreaName { get; set; }
    }
}