using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Archery.API.Attributes;

namespace Archery.API.Types
{
    public class Team : AE.Team
    {
        [Ignore]
        public async Task<List<Tournament>> Tournaments([Service] IMapper mapper, AE.Context context, [Parent] Team parent)
        {
            return await mapper.ProjectTo<Tournament>(context.Reservations.Where(r => r.TeamID == ID).Select(r => r.Tournament)).ToListAsync();
        }

        public new async Task<List<Player>> Players([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<Player>(context.Players.Where(p => p.TeamID == ID)).ToListAsync();
        }

        [Ignore]
        public new async Task<List<Reservation>> Reservations([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<Reservation>(context.Reservations.Where(p => p.TeamID == ID)).ToListAsync();
        }
    }
}