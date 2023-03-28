namespace DAL.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        T? GetById(long id);

        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
