using MentorHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorHub.API.Data.Configurations;

public class LearningGoalsConfiguration : IEntityTypeConfiguration<LearningGoals>
{
    public void Configure(EntityTypeBuilder<LearningGoals> builder)
    {
        builder.ToTable("tbl_learning_goals");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Title).HasColumnName("title").HasColumnType("nvarchar(200)").IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasColumnType("nvarchar(500)");
        builder.Property(p => p.Status).HasColumnName("status").HasConversion<string>().IsRequired();
        builder.Property(p => p.TargetDate).HasColumnName("target_date").HasColumnType("datetime").IsRequired();
    }

}
