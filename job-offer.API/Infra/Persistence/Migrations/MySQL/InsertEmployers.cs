using FluentMigrator;

namespace job_offer.API.Infra.Persistence.Migrations.MySQL
{
    [Migration(3)]
    public class InsertEmployers : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("InsertEmployers.sql");
        }

        public override void Down()
        {
        }
    }
}