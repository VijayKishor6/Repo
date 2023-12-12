using CRUD.Domain.Models;

namespace CRUD.Services.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product model);
        Task Update(Product model); 
        Task Delete(int id);
        Task RegisterAdd(Register model);
        bool UserValid( Login model);
        bool EmailValid(string email);
        void UpdatePassword(string email, string password);
    }
}
