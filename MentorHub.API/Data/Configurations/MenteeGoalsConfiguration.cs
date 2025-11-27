using MentorHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorHub.API.Data.Configurations;

public class MenteeGoalsConfiguration : IEntityTypeConfiguration<MenteeGoals>
{
    public void Configure(EntityTypeBuilder<MenteeGoals> builder)
    {
        builder.ToTable("tbl_mentee_goals");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.MenteeId).HasColumnName("mentee_id").IsRequired();
        builder.Property(p => p.LearningId).HasColumnName("learning_id").IsRequired();
    }

}
