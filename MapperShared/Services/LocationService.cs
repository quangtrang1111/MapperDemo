using MapperShared.Models;
using System.Threading.Tasks;

namespace MapperShared.Services
{
    public class LocationService : ILocationService
    {
        public Task<LocationModel> GetByIdAsync(int id)
        {
            var model = new LocationModel { Id = id, Name = "LocationModel" };

            return Task.FromResult(model);
        }
    }
}
