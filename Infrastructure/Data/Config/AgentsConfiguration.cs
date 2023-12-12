using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class AgentsConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);

            builder.Property(p => p.LicenseAttachment).IsRequired();
            builder.Property(p => p.LicenseAttachmentPath).IsRequired();
            builder.Property(p => p.AgentPhoto).IsRequired();
            builder.Property(p => p.AgentPhotoPath).IsRequired();

            builder.Property(p => p.Commission).IsRequired();
            builder.Property(p => p.DateOfBirth).IsRequired();

            builder.HasOne(p => p.Category).WithMany()
                               .HasForeignKey(p => p.CategoryId);
        }
    }
}