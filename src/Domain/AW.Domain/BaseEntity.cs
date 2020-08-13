namespace AW.Domain
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; protected set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}