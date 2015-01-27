using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace QuoteApp.Database.Pdf
{
    public class GeneratedPdf
    {
        [Key]
        [Required]
        public string Reference { get; set; }
        public byte[] Content { get; set; }

        public GeneratedPdf Find(string id)
        {
            using (IApplicationService context = new DatabaseService())
            {
                return context.GeneratedPdfs.Find(id);
            }
        }

        public void SaveChanges()
        {
            using (IApplicationService context = new DatabaseService())
            {
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Add()
        {
            using (IApplicationService context = new DatabaseService())
            {
                context.GeneratedPdfs.Add(this);
                context.SaveChanges();
            }
        }
    }
}