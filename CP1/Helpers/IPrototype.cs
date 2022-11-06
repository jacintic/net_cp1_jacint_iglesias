namespace CP1.Helpers;

public interface IPrototype<T>
{
    T CreateDeepCopy(List<T> list);
}
