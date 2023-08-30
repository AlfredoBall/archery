using Archery.API.Attributes;
using Archery.Data.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Archery.API.Types
{
    public class Reservation : AE.Reservation
    {
        #region Mutations

        [GraphQLIgnore]
        public async Task<Reservation> CreateReservation([Service] IMapper mapper, Context context, int tournamentID, int teamID)
        {
            var existingReservation = await context.Reservations.Include("Tournament").Include("Team").Where(r => r.TournamentID == tournamentID && r.TeamID == teamID).SingleOrDefaultAsync();

            if (existingReservation != null)
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage($"A Reservation for Tournament { existingReservation.Tournament.Name } and Team { existingReservation.Team.Name } already exists.")
                    .Build());
            }

            var result = await context.Reservations.AddAsync(new AE.Reservation()
            { 
                TournamentID = tournamentID,
                TeamID = teamID
            });

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("Reservation cannot be created.")
                    .Build());

            }

            return mapper.Map<AE.Reservation, Reservation>(result.Entity);
        }

        #endregion

        #region Queries

        [Ignore]
        public new Team Team([Service] IMapper mapper, AE.Context context)
        {
            return mapper.Map<AE.Team, Team>(base.Team);
        }

        [Ignore]
        public new Tournament Tournament([Service] IMapper mapper, AE.Context context)
        {
            return mapper.Map<AE.Tournament, Tournament>(base.Tournament);
        }

        #endregion
    }
}