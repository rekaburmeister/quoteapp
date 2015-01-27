namespace QuoteApp.Database.Work
{
    public class WorkItem
    {
        [Required]
        public string WorkArea { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string WorkTitle { get; set; }

        [Required]
        public int Price { get; set; }

        
    }
}