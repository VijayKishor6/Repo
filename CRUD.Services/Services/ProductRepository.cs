using CRUD.Data.MySQL.Data;
using CRUD.Domain.Models;
using CRUD.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Services.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
           var Products = await _context.Products.ToListAsync();
            return Products;
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Add(Product model)
        {
            await _context.Products.AddAsync(model);
            await Save();
        }
        public async Task Update(Product model)
        {
            var product = await _context.Products.FindAsync(model.ID);
            if(product != null)
            {
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _context.Update(product);
                await Save();
            }
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null )
            {
                _context.Products.Remove(product);
                await Save();
            }
        }

        public async Task RegisterAdd(Register model)
        {

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _context.Register.AddAsync(model);
            await Save();
        }
        public bool UserValid(Login model)
        {
            var user = _context.Register.FirstOrDefault(value => value.Email == model.Username);

            if (user != null)
            {
              
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

                return isPasswordValid;
            }

            return false;
        }

        public bool EmailValid(string email)
        {
            var value = _context.Register.FirstOrDefault(x => x.Email == email);
            if (value == null)
                return false;
            return true;
        }

        public void UpdatePassword(string email, string newPassword)
        {
            var user = _context.Register.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                user.Password = newPassword;

                
                _context.SaveChanges();
            }
           
        }

    }
}
