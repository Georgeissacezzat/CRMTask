using CRMTask.DataAccess.Configurations;
using CRMTask.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Mail> Mail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CampaignConfig());
            modelBuilder.ApplyConfiguration(new MailConfig());
            modelBuilder.ApplyConfiguration(new TemplateConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
