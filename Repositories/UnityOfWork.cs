using CHARIBOY_ARTS.Data;
using CHARIBOY_ARTS.Repositories.IRepository;

namespace CHARIBOY_ARTS.Repositories
{
    public class UnityOfWork: IUnityOfWork
    {
        private ApplicationDbContext _db;
        public IProductRepository product { get; private set; }
        public UnityOfWork(ApplicationDbContext db)
        {
            _db = db;
            product = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
