using Archery.API.Attributes;
using Archery.Data.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Archery.API.Types
{
    public class Player : AE.Player
    {
        #region Mutations

        [GraphQLIgnore]
        public async Task<Player> CreatePlayer([Service] IMapper mapper, Context context, int teamID, int memberID)
        {
            if (!context.Teams.Any(t => t.ID == teamID) || !context.Members.Any(p => p.ID == memberID))
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("Cannot create Player")
                    .Build());
            }

            var newPlayer = await context.Players.AddAsync(new Player {  TeamID = teamID, MemberID = memberID });

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("Cannot create Player")
                    .Build());
            }

            // navs
            

            return mapper.Map<Player>(newPlayer);
        }

        #endregion

        #region Queries

        [Ignore]
        public new Member Member([Service] IMapper mapper, AE.Context context)
        {
            return mapper.Map<AE.Member, Member>(base.Member);
        }

        [Ignore]
        public new async Task<Team> Team([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<Team>(context.Teams.Where(t => t.ID == TeamID)).SingleAsync();
        }

        public new async Task<List<Score>> Scores([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<Score>(context.Scores.Where(s => s.PlayerID == ID)).ToListAsync();
        }

        #endregion
    }
}