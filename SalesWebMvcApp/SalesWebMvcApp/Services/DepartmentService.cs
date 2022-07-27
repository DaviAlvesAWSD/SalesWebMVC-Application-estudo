using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvcApp.Models;

namespace SalesWebMvcApp.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcAppContext _context;

        public DepartmentService(SalesWebMvcAppContext context)
        {
            _context = context;
        }

        public List<Departments> FindAll()
        {
            return _context.Departments.OrderBy(x => x.Name).ToList();
        }
    }
}