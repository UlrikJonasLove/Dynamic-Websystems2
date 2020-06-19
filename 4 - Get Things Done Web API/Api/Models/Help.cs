using System.Linq;

namespace Api.Models
{
    public class Help : IHelp
    {
            private readonly ApiDbContext _context;

            public Help(ApiDbContext context)
            {
                _context = context;
            }

            public User GetUser(string id)
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                return user;
            }
        
    }
}
