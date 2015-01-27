using System.ComponentModel.DataAnnotations;

namespace QuoteApp.Database.Invoice
{
    public class InvoiceDetail
    {
        [Key]
        [Required]
        public int DetailId { get; set; }

        [Required]
        public string InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

    }
}