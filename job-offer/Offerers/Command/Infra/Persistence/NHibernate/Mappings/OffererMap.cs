using job_offer.Offerers.Command.DomainModel.Entities;
using FluentNHibernate.Mapping;

namespace job_offer.Offerers.Command.Infra.Persistence.NHibernate.Mapping
{
    public class OffererMap : ClassMap<Offerer>
    {
        public OffererMap()
        {
            CompositeId(x => x.OffererId).KeyProperty(x => x.Id, y => y.ColumnName("offerer_id"));
            Map(x => x.CodOfferer).Column("cod_offerer");
            Map(x => x.Active).CustomType<bool>().Column("is_active");
            Map(x => x.CreatedAt).Column("created_at_utc");
            Map(x => x.UpdatedAt).Column("updated_at_utc");
        }
    }
}