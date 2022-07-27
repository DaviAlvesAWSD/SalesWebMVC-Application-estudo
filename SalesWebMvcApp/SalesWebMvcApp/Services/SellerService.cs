using Microsoft.EntityFrameworkCore;
using SalesWebMvcApp.Models;
using SalesWebMvcApp.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvcApp.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcAppContext _context;

        public SellerService(SalesWebMvcAppContext context)
        {
            _context = context;
        }

        public List<Saller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Saller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Saller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

       public void Update(Saller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) throw new NotFoundException("Id not found");

            try
            {
                _context.Update(obj);
                _context.SaveChanges();

            }
            catch(DbUpdateConcurrencyException err)
            {
                throw new DbUpdateConcurrencyException(err.Message);
            }
        }
    }
}
