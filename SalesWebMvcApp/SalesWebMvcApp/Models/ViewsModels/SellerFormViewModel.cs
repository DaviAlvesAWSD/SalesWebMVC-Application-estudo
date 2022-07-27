using System.Collections.Generic;

namespace SalesWebMvcApp.Models.ViewsModels
{
    public class SellerFormViewModel
    {
        public Saller Seller { get; set; }
        public ICollection<Departments> Departments { get; set; }

    }
}
