namespace DataAccess;

public interface IDbAccess<T>
{
    List<T> GetAll();
    T GetById(int id);
    bool Add(T t);
}