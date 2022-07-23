using FluentMigrator;

namespace job_offer.API.Infra.Persistence.Migrations.MySQL
{
    [Migration(4)]
    public class InsertCustomers : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("InsertOfferers.sql");
        }

        public override void Down()
        {
        }
    }
}