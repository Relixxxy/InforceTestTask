using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InforceTestTask.Data.Contexts;

public class AuthDbContext : IdentityDbContext
{
	public AuthDbContext(DbContextOptions<AuthDbContext> options)
		: base(options)
	{
	}
}
