namespace TheFipster.Munchkin.StashRepository.Abstractions
{
    public interface ISave<TEntity>
    {
        void Save(TEntity entity);
    }
}
