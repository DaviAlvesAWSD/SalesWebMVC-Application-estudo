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

        public async Task<List<Saller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Saller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Saller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Seller.Find(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException err)
            {

                throw new IntegrityException("Cant't delete seller because he/she has sales");
            }
        }

       public async Task UpdateAsync(Saller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) throw new NotFoundException("Id not found");

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();

            }
            catch(DbUpdateConcurrencyException err)
            {
                throw new DbUpdateConcurrencyException(err.Message);
            }
        }
    }
}
