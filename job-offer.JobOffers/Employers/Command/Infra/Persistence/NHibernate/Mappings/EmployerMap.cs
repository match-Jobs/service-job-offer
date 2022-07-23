using job_offer.JobOffers.Employers.Command.DomainModel.Entities;
using FluentNHibernate.Mapping;

namespace job_offer.JobOffers.Employers.Command.Infra.Persistence.NHibernate.Mapping
{
    public class EmployerMap : ClassMap<Employer>
    {
        public EmployerMap()
        {
            CompositeId(x => x.EmployerId).KeyProperty(x => x.Id, y => y.ColumnName("employer_id"));
            Map(x => x.CodEmployer).Column("cod_employer");
            Map(x => x.Active).CustomType<bool>().Column("is_active");
            Map(x => x.CreatedAt).Column("created_at_utc");
            Map(x => x.UpdatedAt).Column("updated_at_utc");
        }
    }
}