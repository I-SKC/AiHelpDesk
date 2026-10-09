using AIHelpDesk.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AIHelpDesk.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}