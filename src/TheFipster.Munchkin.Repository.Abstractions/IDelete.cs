namespace TheFipster.Munchkin.CardStash.Repository.Abstractions
{
    public interface IDelete<TEntity>
    {
        void Delete(TEntity entity);
    }
}
