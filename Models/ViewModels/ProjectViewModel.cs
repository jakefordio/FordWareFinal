using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FordWare.Models.ViewModels
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
