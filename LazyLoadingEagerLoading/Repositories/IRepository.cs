namespace LazyLoadingEagerLoading.Repositories
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetAll();
    }
}
