using MentorHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorHub.API.Data.Configurations;

public class MentorSkillsConfiguration : IEntityTypeConfiguration<MentorSkills>
{
    public void Configure(EntityTypeBuilder<MentorSkills> builder)
    {
        builder.ToTable("tbl_mentor_skills");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.MentorId).HasColumnName("mentor_id").IsRequired();
        builder.Property(p => p.SkillId).HasColumnName("skill_id").IsRequired();
        builder.Property(p => p.Level).HasColumnName("level").IsRequired();
    }

}
