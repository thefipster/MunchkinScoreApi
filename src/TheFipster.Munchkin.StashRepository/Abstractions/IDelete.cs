namespace TheFipster.Munchkin.StashRepository.Abstractions
{
    public interface IDelete<TEntity>
    {
        void Delete(TEntity entity);
    }
}
