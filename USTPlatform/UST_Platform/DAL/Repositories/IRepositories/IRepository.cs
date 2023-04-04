namespace DAL.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        T GetById(int id);

        IQueryable<T> FindAll();

        void Create(T entity);

        void Delete(int id);

    }
}
