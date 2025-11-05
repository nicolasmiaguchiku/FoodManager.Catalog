using FastMenu.Domain.Entities;
using FastMenu.Domain.Interfaces;
using MongoDB.Driver;

namespace FastMenu.Infrastructure.Persistence.Repositories
{
    public class FoodRepository(IMongoDatabase mongoDb) : BaseRepository<FoodEntity>(mongoDb, "Foods"), IFoodRepository;
}
