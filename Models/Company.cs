using System.ComponentModel.DataAnnotations;

namespace FordWare.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
