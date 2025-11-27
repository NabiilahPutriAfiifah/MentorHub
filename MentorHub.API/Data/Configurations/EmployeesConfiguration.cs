using MentorHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Data.Configurations;

public class EmployeesConfiguration : IEntityTypeConfiguration<Employees>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employees> builder)
    {
         builder.ToTable("tbl_employees");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.FirstName).HasColumnName("first_name").HasMaxLength(100).IsRequired();
            builder.Property(p => p.LastName).HasColumnName("last_name").HasMaxLength(100).IsRequired();
            builder.Property(p => p.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            builder.Property(p => p.Bio).HasColumnName("bio").HasMaxLength(1000);
            builder.Property(p => p.Experience).HasColumnName("experience").HasMaxLength(255);
            builder.Property(p => p.Position).HasColumnName("position").HasMaxLength(100).IsRequired();
            builder.Property(p => p.MentorId).HasColumnName("mentor_id");

   
            // builder.HasOne(p => p.Mentor).WithMany(m => m.Employee).HasForeignKey(p => p.MentorId).OnDelete(DeleteBehavior.Restrict);

            // builder.HasOne(p => p.Account).WithOne(a => a.Employee).HasForeignKey<Accounts>(a => a.Id);
    }

}
