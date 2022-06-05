using System;
using ApiCrudCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrudCore.Data
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

		public DbSet<Todo> todos => Set<Todo>();
	}
}

