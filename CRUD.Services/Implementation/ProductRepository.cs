using CRUD.Repository;
using CRUD.Repository.Models;
using CRUD.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            await _context.Register.AddAsync(model);
            await Save();
        }

        public bool UserValid(Login model)
        {
            var result = _context.Register.FirstOrDefault(value => value.Email == model.Username && value.Password == model.Password);
            if (result == null)
                return false;
            return true;
        }
    }
}
