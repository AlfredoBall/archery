using Archery.Data.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Archery.API.Types
{
    public class BaseLists
    {
        public async Task<List<League>> Leagues([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<League>(context.Leagues).ToListAsync();
        }

        public async Task<List<Tournament>> Tournaments([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<Tournament>(context.Tournaments).ToListAsync();
        }

        public async Task<List<Team>> Teams([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<Team>(context.Teams).ToListAsync();
        }

        public async Task<List<Player>> Players([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<Player>(context.Players).ToListAsync();
        }

        public async Task<List<Member>> Members([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<Member>(context.Members).ToListAsync();
        }

        public async Task<List<Reservation>> Reservations([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<Reservation>(context.Reservations).ToListAsync();
        }

        public async Task<List<Set>> Sets([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<Set>(context.Sets).ToListAsync();
        }

        public async Task<List<Score>> Scores([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<Score>(context.Scores).ToListAsync();
        }

        public async Task<List<Lane>> Lanes([Service] IMapper mapper, [Service] Context context)
        {
            return await mapper.ProjectTo<Lane>(context.Lanes).ToListAsync();
        }
    }
}
