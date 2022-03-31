namespace Week1.ModelingTask.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetByID(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
