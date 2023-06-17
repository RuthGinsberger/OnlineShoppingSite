namespace DalApi;
public interface ICrud<T>
{
    public T Get(Func<T, bool> func);
    public int Add(T item);
    public void Delete(int id);
    public void Update(T item);
    public IEnumerable<T> GetAll(Func<T, bool>? func=null);
}

