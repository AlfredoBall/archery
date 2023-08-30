using Archery.API.Attributes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Archery.API.Types
{
    public class Lane : AE.Lane
    {
        [Ignore]
        public new async Task<List<Tournament>> Tournaments([Service] IMapper mapper, AE.Context context, [Parent] Lane parent)
        {
            return await mapper.ProjectTo<Tournament>(context.Tournaments.Where(t => t.LaneID == ID)).ToListAsync();
        }
    }
}