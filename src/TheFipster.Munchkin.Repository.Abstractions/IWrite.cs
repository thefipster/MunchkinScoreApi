namespace TheFipster.Munchkin.StashRepository.Abstractions
{
    public interface IWrite<TEntity>
    {
        void Write(TEntity entity);
    }
}
