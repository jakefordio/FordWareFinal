using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FordWare.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [ValidateNever]
        public string ImageURL { get; set; }
        public string LinkToProject { get; set; }
        [Required]
        public double OriginalPrice { get; set; }
        public double? AmountPaid { get; set; }
        public double? RemainingBalance { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
    }
}
