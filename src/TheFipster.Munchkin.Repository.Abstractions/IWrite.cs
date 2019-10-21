namespace TheFipster.Munchkin.CardStash.Repository.Abstractions
{
    public interface IWrite<TEntity>
    {
        void Write(TEntity entity);
    }
}
