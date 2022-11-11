﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FordWare.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display order must be between 1 and 100.")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
