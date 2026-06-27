namespace CHARIBOY_ARTS.Repositories.IRepository
{
    public interface IUnityOfWork
    {
        IProductRepository product { get; }

        void Save();
    }
}
