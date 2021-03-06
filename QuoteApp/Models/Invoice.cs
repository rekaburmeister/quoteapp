﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class Invoice
    {
        [Key]
        [Required]
        public string InvoiceId { get; set; }

        [Required]
        public int WorkLocationId { get; set; }
        public virtual WorkLocation WorkLocation { get; set; }

        [Required]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        public DateTime? PaidDate { get; set; }

        [Required]
        public int Price { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        public string Reference { get; set; }

        // I was thinking of approving the quoted works individually with the invoice but now it seems daft. Still, leaving it here to think about it later
        //public virtual ICollection<InvoicedWork> InvoicedWorks { get; set; }

        public static List<Invoice> GetInvoicesForPeriod(DateTime from, DateTime to)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Invoices.Where(i => i.InvoiceDate >= from && i.InvoiceDate <= to).ToList();
            }
        }

        public static List<Invoice> GetAllInvoices()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Invoices.ToList();
            }
        }

        public static List<Invoice> GetUnpaidInvoices()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Invoices.Where(i => i.PaidDate == null).ToList();
            }
        }

        public static string GetNextInvoiceId()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (context.Invoices.Any())
                {
                    var id = context.Invoices.ToList().Last();
                    string idString = id.InvoiceId;
                    string idPart = idString.Split('-').Last();
                    idPart = idPart.Remove(0, 1);
                    return "I" + (Convert.ToInt16(idPart) + 1);
                }
                return "I1658";
            }
        }

        public static void MarkAsPaid(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Invoice invoice = context.Invoices.Find(invoiceId);
                invoice.PaidDate = DateTime.UtcNow;
                context.Entry(invoice).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }
        }

        public void Add()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Invoices.Add(this);
                context.SaveChanges();
            }
        }
    }
}