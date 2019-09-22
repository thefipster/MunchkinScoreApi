using System.Collections.Generic;

namespace TheFipster.Munchkin.GamePersistance
{
    public interface ICardStore
    {
        List<string> GetDungeons();
        void SyncDungeons(List<string> dungeons);

    }
}
