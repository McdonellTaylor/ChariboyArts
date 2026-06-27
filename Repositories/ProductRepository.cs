using CHARIBOY_ARTS.Data;
using CHARIBOY_ARTS.Models;
using CHARIBOY_ARTS.Repositories.IRepository;

namespace CHARIBOY_ARTS.Repositories
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
