using Archery.API.Types;
using AutoMapper;

namespace Archery.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AE.Team, Team>();
            CreateMap<AE.League, League>();
            CreateMap<AE.Tournament, Tournament>();
            CreateMap<AE.Player, Player>();
            CreateMap<AE.Reservation, Reservation>();
            CreateMap<AE.Lane, Lane>();
            CreateMap<AE.Set, Set>();
            CreateMap<AE.Score, Score>();
            CreateMap<AE.Member, Member>();
        }
    }
}
