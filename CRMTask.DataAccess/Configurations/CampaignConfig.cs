using CRMTask.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Configurations
{
    public class CampaignConfig : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Subject).HasMaxLength(500);
            builder.Property(e => e.MailchimpCampaignId).HasMaxLength(100);
            builder.Property(e => e.Status).HasConversion<int>();

            builder.HasOne(e => e.Template)
                  .WithMany(t => t.Campaigns)
                  .HasForeignKey(e => e.TemplateId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
