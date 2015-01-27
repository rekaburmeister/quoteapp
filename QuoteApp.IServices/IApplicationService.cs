using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuoteApp.Services
{
    public interface IApplicationService
    {
        IdentityDbContext<TUser> GetDbContext();
    }
}
