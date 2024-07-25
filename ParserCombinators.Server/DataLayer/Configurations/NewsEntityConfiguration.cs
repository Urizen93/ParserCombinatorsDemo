using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParserCombinators.Server.DataLayer.Configurations;

public sealed class NewsEntityConfiguration : IEntityTypeConfiguration<NewsEntity>
{
    public void Configure(EntityTypeBuilder<NewsEntity> builder)
    {
        builder.HasKey(x => x.ID);

        builder.Property(x => x.Text)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Filter)
            .HasMaxLength(500);

        builder.HasData(
            new NewsEntity { ID = 1, Text = "This is shown to everybody!" },
            new NewsEntity { ID = 2, Text = "Some update for Admins", Filter = "Role = 'Admin'" },
            new NewsEntity { ID = 3, Text = "Managers cannot see it", Filter = "Role != 'Manager'" },
            new NewsEntity { ID = 4, Text = "Admins and managers only", Filter = "Role IN ('Admin','Manager')" },
            new NewsEntity { ID = 5, Text = "For premium customers or admins", Filter = "Role = 'Admin' OR Premium IS true" },
            new NewsEntity { ID = 6, Text = "This one is for adults!", Filter = "Age >= 18" },
            new NewsEntity { ID = 7, Text = "News for people younger than 65", Filter = "Age < 65" }
        );
    }
}