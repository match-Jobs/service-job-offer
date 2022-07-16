using FluentMigrator;

namespace job_offer.API.Infra.Persistence.Migrations.MySQL
{
    [Migration(5)]
    public class InsertOffersJob : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("InsertOffersJob.sql");
        }

        public override void Down()
        {
        }
    }
}
