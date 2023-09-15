using Archery.API.Attributes;
using Archery.Data.Entity;
using AutoMapper;
using GreenDonut;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;

namespace Archery.API.Types
{
    /// <summary>
    /// // A unique [Parent] type is needed when there are more than one Tournaments signatures (such as on Team and Lane) so GraphQL can route to the right one. The ID will be set to the same value as league.ID
    /// </summary>
    public class League : AE.League
    {
        #region Mutations

        [GraphQLIgnore]
        public async Task<League?> UpdateLeague([Service] IMapper mapper, Context context, int id, string name)
        {
            throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("League does not exist")
                    .SetCode("League does not exist")
                    .Build());

            try
            {
                var existingLeague = await context.Leagues.SingleAsync(l => l.ID == id);

                existingLeague.Name = name;

                context.SaveChanges();

                return mapper.Map<AE.League, League>(existingLeague);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("League does not exist")
                    .Build());
            }
        }

        [GraphQLIgnore]
        public async Task<League> CreateLeague([Service] IMapper mapper, Context context, string name)
        {
            var result = await context.Leagues.AddAsync(new AE.League() { Name = name });

            try
            {
                await context.SaveChangesAsync();

                return mapper.Map<AE.League, League>(result.Entity);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("League name already exists")
                    .Build());

            }
        }

        #endregion

        #region Queries

        [Ignore]
        public async Task<List<Lane>> Lanes([Service] IMapper mapper, Context context, [Parent] League parent)
        {
            return await mapper.ProjectTo<Lane>(context.Tournaments.Where(tr => tr.LeagueID == ID).Select(t => t.Lane).Distinct()).ToListAsync();
        }

        [Ignore]
        public async Task<List<Player>> Players([Service] IMapper mapper, Context context)
        {
            var players = context.Tournaments
                .Where(tr => tr.LeagueID == ID)
                .Join(context.Reservations,
                    tr => tr.ID,
                    r => r.TournamentID,
                    (tr, r) => new { tr, r })
                .Join(context.Teams,
                    g => g.r.TeamID,
                    tm => tm.ID,
                    (g, tm) => new { g, tm })
                .SelectMany(x => x.tm.Players).Distinct();

            return await mapper.ProjectTo<Player>(players).ToListAsync();
        }

        [Ignore]
        public new async Task<List<Tournament>> Tournaments([Service] IMapper mapper, Context context, [Parent] League parent)
        {
            return await mapper.ProjectTo<Tournament>(context.Tournaments.Where(t => t.LeagueID == ID)).ToListAsync();
        }

        public async Task<List<Team>> Teams([Service] IMapper mapper, Context context, [Parent] League parent)
        {
            var teams = context.Tournaments
                .Where(tr => tr.LeagueID == ID)
                .Join(context.Reservations,
                    tr => tr.ID,
                    r => r.TournamentID,
                    (tr, r) => new { tr, r })
                .Select(x => x.r.Team).Distinct();

            return await mapper.ProjectTo<Team>(teams).ToListAsync();
        }

        #endregion
    }
}