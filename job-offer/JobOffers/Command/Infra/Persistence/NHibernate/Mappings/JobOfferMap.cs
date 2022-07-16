using job_offer.JobOffers.Command.DomainModel.Entities;
using FluentNHibernate.Mapping;

namespace job_offer.JobOffers.Command.Infra.Persistence.NHibernate.Mappings
{
    public class JobOfferMap : ClassMap<JobOffer>
    {
        public JobOfferMap()
        {
            Table("offer_job");
            CompositeId(x => x.JobOfferId).KeyProperty(x => x.Id, y => y.ColumnName("offer_job_id"));
            Map(x => x.CodOffer).Column("cod_oferta");
            Component(x => x.OfferDetail, m =>
            {
                m.Map(x => x.Description, "description");
                m.Map(x => x.Tittle, "tittle");
            });
            Component(x => x.EmployerId, m =>
            {
                m.Map(x => x.Id, "employer_id");
            });
            Component(x => x.Vacancie, m =>
            {
                m.Map(x => x.NumVacancies, "num_vacancies");
                m.Map(x => x.TotalVacancies, "total_vacancies");
            });
            Component(x => x.Remuneration, m =>
            {
                m.Map(x => x.Amount, "mto_remuneration");
                m.Map(x => x.Currency, "currency_remuneration");
            });
            Map(x => x.JobOfferStateId).CustomType<int>().Column("offer_job_state_id");
            Map(x => x.Active).CustomType<bool>().Column("is_active");
            Map(x => x.CreatedAt).Column("created_at_utc");
            Map(x => x.UpdatedAt).Column("updated_at_utc");
        }
    }
}