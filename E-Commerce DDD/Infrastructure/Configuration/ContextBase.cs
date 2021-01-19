using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CompraUsuario> CompraUsuarios { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        private string GetStringConectionConfig()
        {
            /*string strCon = "Data Source=DESKTOP-V0RI5SU;Initial Catalog=DDD_ECOMMERCE;Integrated Security=False;Integrated Security=true;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True";*/
            string strCon = "Server=tcp:servergigio.database.windows.net,1433;Initial Catalog=Desenvolvimento;Persist Security Info=False;User ID=gelvane.silva;Password=Acesso73*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return strCon;
        }
    }
}
