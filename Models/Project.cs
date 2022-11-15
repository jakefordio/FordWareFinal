using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FordWare.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [ValidateNever]
        [DisplayName("Image")]
        public string ImageURL { get; set; }
        public string LinkToProject { get; set; }
        [Required]
        public double OriginalPrice { get; set; }
        public double? AmountPaid { get; set; }
        public double? RemainingBalance { get; set; }
        [DisplayName("Category")]
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [DisplayName("Company")]
        [Required]
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
    }
}
