using job_offer.Postulations.Command.DomainModel.Entities;
using FluentNHibernate.Mapping;

namespace job_offer.Postulations.Command.Infra.Persistence.NHibernate.Mappings
{
    public class PostulationMap : ClassMap<Postulation>
    {
        public PostulationMap()
        {
            CompositeId(x => x.PostulationId).KeyProperty(x => x.Id, y => y.ColumnName("postulation_id"));
            Component(x => x.OffererId, m =>
            {
                m.Map(x => x.Id, "offerer_id");
            });
            Component(x => x.JobOfferId, m =>
            {
                m.Map(x => x.Id, "offer_job_id");
            });
            Map(x => x.PostulationStateId).CustomType<int>().Column("postulation_state_id");
            Map(x => x.CreatedAt).Column("created_at_utc");
            Map(x => x.UpdatedAt).Column("updated_at_utc");
        }
    }
}