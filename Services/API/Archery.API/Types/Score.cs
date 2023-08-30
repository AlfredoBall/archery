using Archery.API.Attributes;
using AutoMapper;

namespace Archery.API.Types
{
    public class Score : AE.Score
    {
        [Ignore]
        public new Player Player([Service] IMapper mapper, AE.Context context)
        {
            return mapper.Map<AE.Player, Player>(base.Player);
        }

        [Ignore]
        public new Set Set([Service] IMapper mapper, AE.Context context)
        {
            return mapper.Map<AE.Set, Set>(base.Set);
        }
    }
}