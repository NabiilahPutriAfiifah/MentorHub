using MentorHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorHub.API.Data.Configurations;

public class SkillsConfiguration : IEntityTypeConfiguration<Skills>
{
    public void Configure(EntityTypeBuilder<Skills> builder)
    {
        builder.ToTable("tbl_skills");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasMaxLength(500);
    }

}
