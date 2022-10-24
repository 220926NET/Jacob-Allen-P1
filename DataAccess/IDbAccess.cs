namespace DataAccess;

public interface IDbAccess<T>
{
    List<T> GetAll();
    T GetById(int id);
    bool Add(ref T t);
}