using Archery.API.Attributes;
using Archery.Data.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Archery.API.Types
{
    /// <summary>
    /// The new keyword is used to override navigation properties in the Entity Model.
    /// </summary>
    public class Tournament : AE.Tournament
    {
        #region Mutations

        // Return a full entity from the context otherwise navigation properties will be null in the return type.
        [GraphQLIgnore]
        public async Task<Tournament> CreateTournament([Service] IMapper mapper, Context context,
                                                            int leagueID,
                                                            int laneID,
                                                            string name,
                                                            //CreateConvention a request filter that performs validation, requested Start Time has to be greater than the current time
                                                            string startTime, string endTime)
        {
            var existingTournament = await context.Tournaments.Include("League").Where(t => t.LeagueID == leagueID && t.Name == name).SingleOrDefaultAsync();

            if (existingTournament != null)
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage($"A Tournament named { existingTournament.Name } for League { existingTournament.League.Name } already exists.")
                    .Build());
            }

            // Build a Utility to perform this calculation

            // Get all existing Lane timeslots scheduled for Tournaments of the Lane


            var requestedTimeSlot = new { StartTime = DateTime.Parse(startTime), EndTime = DateTime.Parse(endTime) };

            // Forget about any previous time slots that start after the requested End Time or end before the requested Start Time
            // Order by existing Start Times

            var conflicts = context.Tournaments.Where(t =>
                                            (requestedTimeSlot.StartTime > t.StartTime && requestedTimeSlot.StartTime < t.EndTime)
                                            ||
                                            (requestedTimeSlot.EndTime < t.EndTime && requestedTimeSlot.EndTime > t.StartTime)).ToList();

            if (conflicts.Any())
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage($"Tournament { name }'s schedule conflicts with existing appointments.")
                    .Build());
            }

            var result = await context.Tournaments.AddAsync(new AE.Tournament()
            {
                LeagueID = leagueID,
                Name = name,
                LaneID = laneID,
                StartTime = DateTime.Parse(startTime),
                EndTime = DateTime.Parse(endTime)
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
                    .SetMessage("Tournament cannot be created.")
                    .Build());

            }
            
            // Return with Navigation properties hydrated
            var hydratedEntity = await context.Tournaments.Include("League").Include("Lane").SingleAsync(c => c.ID == result.Entity.ID);

            return mapper.Map<AE.Tournament, Tournament>(hydratedEntity);
        }

        [GraphQLIgnore]
        public async Task<Tournament> UpdateTournament([Service] IMapper mapper, Context context, int tournamentID, string name, string startTime, string endTime)
        {
            try
            {
                var existingTournament = await context.Tournaments.SingleAsync(t => t.ID == tournamentID);

                var requestedTimeSlot = new { StartTime = DateTime.Parse(startTime), EndTime = DateTime.Parse(endTime) };

                if (existingTournament.StartTime != existingTournament.StartTime || existingTournament.EndTime != EndTime)
                {
                    var conflicts = context.Tournaments.Where(t => t.ID != tournamentID &&
                                                    (requestedTimeSlot.StartTime > t.StartTime && requestedTimeSlot.StartTime < t.EndTime)
                                                    ||
                                                    (requestedTimeSlot.EndTime < t.EndTime && requestedTimeSlot.EndTime > t.StartTime)).ToList();

                    if (conflicts.Any())
                    {
                        throw new GraphQLException(
                            ErrorBuilder
                            .New()
                            .SetMessage($"Tournament {name}'s schedule conflicts with existing appointments.")
                            .Build());
                    }

                    existingTournament.StartTime = requestedTimeSlot.StartTime;
                    existingTournament.EndTime = requestedTimeSlot.EndTime;
                }

                existingTournament.Name = name;

                await context.SaveChangesAsync();

                return mapper.Map<AE.Tournament, Tournament>(existingTournament);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage("Tournament cannot be updated.")
                    .Build());
            }
        }

        [GraphQLIgnore]
        public async Task<bool> DeleteTournament([Service] IMapper mapper, Context context, int tournamentID)
        {
            try
            {
                var tournament = await context.Tournaments.SingleAsync(t => t.ID == tournamentID);

                context.Tournaments.Remove(tournament);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(
                    ErrorBuilder
                    .New()
                    .SetMessage($"Tournament deletion failed.")
                    .Build());
            }
        }

        #endregion

        #region Queries

        [Ignore]
        public new League League([Service] IMapper mapper, AE.Context context)
        {
            return mapper.Map<AE.League, League>(base.League);
        }

        [Ignore]
        public new Lane Lane([Service] IMapper mapper, AE.Context context)
        {
            return mapper.Map<AE.Lane, Lane>(base.Lane);
        }

        public async Task<List<Player>> Players([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<AT.Player>(context.Reservations.Where(r => r.TournamentID == ID).Select(r => r.Team).SelectMany(t => t.Players)).ToListAsync();
        }

        public async Task<List<Team>> Teams([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<AT.Team>(context.Reservations.Where(r => r.TournamentID == ID).Select(r => r.Team)).ToListAsync();
        }

        public new async Task<List<Set>> Sets([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<Set>(context.Sets.Where(t => t.Tournament.ID == ID)).ToListAsync();
        }

        [Ignore]
        public new async Task<List<Reservation>> Reservations([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<Reservation>(context.Reservations.Where(t => t.TournamentID == ID)).ToListAsync();
        }

        #endregion
    }
}