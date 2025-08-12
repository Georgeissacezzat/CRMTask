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
    public class MailConfig: IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.MailchimpEmailId).HasMaxLength(100);
            builder.Property(e => e.Status).HasConversion<int>();

            builder.HasOne(e => e.User)
                  .WithMany(u => u.Mails)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Campaign)
                  .WithMany(c => c.Mails)
                  .HasForeignKey(e => e.CampaignId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Template)
                  .WithMany(t => t.Mails)
                  .HasForeignKey(e => e.TemplateId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
