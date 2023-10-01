using AccessManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Configuration;

public class PermissionGroupConfiguration : IEntityTypeConfiguration<GroupPermissionEntity>
{
    public void Configure(EntityTypeBuilder<GroupPermissionEntity> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

    }
}
