using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvcApp.Models
{
    public class Departments
    {



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name  { get; set; }

        public ICollection<Saller> Sellers { get; set; } = new List<Saller>();

        public Departments()
        {

        }

        public Departments(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Saller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSalles(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }   
}
