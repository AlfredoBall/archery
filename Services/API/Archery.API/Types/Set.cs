using Archery.API.Attributes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Archery.API.Types
{
    public class Set : AE.Set
    {
        [Ignore]
        public new Tournament Tournament([Service] IMapper mapper, AE.Context context)
        {
            return mapper.Map<AE.Tournament, Tournament>(base.Tournament);
        }

        public new async Task<List<Score>> Scores([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<Score>(context.Scores.Where(s => s.Set.ID == ID)).ToListAsync();
        }
    }
}