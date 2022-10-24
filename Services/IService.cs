namespace Services;

public interface IService<T>
{
    List<T> GetAll();
    T GetById(int id);
    bool Add(ref T t);
}