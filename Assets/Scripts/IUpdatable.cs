namespace Assets.Scripts
{
    public interface IUpdatable<T>
    {
        void UpdateFrom(T source);
    }
}
