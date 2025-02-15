using BankUI.Models;

namespace BankUI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllProducts();
        Task<UserDto> GetProductById(int id);
        Task AddProduct(UserDto product, MultipartFormDataContent content);
        Task UpdateProduct(UserDto product, MultipartFormDataContent content);
        Task DeleteProduct(int id);
    }
}
