using Archery.API.Attributes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Archery.API.Types
{
    public class Member : AE.Member
    {
        [Ignore]
        public async Task<Player> Player([Service] IMapper mapper, AE.Context context)
        {
            return await mapper.ProjectTo<Player>(context.Players.Where(p => p.MemberID == ID)).SingleAsync();
        }
    }
}