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
    public class TemplateConfig : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Subject).HasMaxLength(500);
            builder.Property(e => e.MailchimpTemplateId).HasMaxLength(100);
            builder.Property(e => e.HtmlContent).HasColumnType("ntext");
            builder.Property(e => e.TextContent).HasColumnType("ntext");

        }
    }
}
