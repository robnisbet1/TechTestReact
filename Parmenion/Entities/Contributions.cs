namespace Parmenion.Entities
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Contributions
    {
        public string EmailAddress { get; set; }

        public decimal InitialContribution { get; set; }

        public decimal MonthlyContribution { get; set; }

        public class ContributionsMap : IEntityTypeConfiguration<Contributions>
        {
            public void Configure(EntityTypeBuilder<Contributions> builder)
            {
                builder.HasKey(x => x.EmailAddress);
            }
        }
    }
}
