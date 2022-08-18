using MapperShared.Models;
using System.Threading.Tasks;

namespace MapperShared.Services
{
    public interface ILocationService
    {
        Task<LocationModel> GetByIdAsync(int id);
    }
}
