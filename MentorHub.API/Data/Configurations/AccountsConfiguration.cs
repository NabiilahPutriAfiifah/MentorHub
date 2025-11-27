using MentorHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorHub.API.Data.Configurations;

public class AccountsConfiguration : IEntityTypeConfiguration<Accounts>
{
    public void Configure(EntityTypeBuilder<Accounts> builder)
    {
        builder.ToTable("tbl_accounts");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Username).HasColumnName("username").HasColumnType("nvarchar(150)").IsRequired();
            builder.Property(p => p.Password).HasColumnName("password").HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(p => p.Otp).HasColumnName("otp");
            builder.Property(p => p.Expired).HasColumnName("eppired");
            builder.Property(p => p.IsUsed).HasColumnName("is_used");
    }

}
