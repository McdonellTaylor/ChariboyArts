using CHARIBOY_ARTS.Models;

namespace CHARIBOY_ARTS.Repositories.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        void Update(Product product);
    }
}
