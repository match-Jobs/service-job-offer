using System;
using System.Collections.Generic;
using static System.String;
using job_offer.JobOffers.Common.DomainModel.ValueObjects;

namespace job_offer.JobOffers.DomainModel.ValueObjects
{
    public class Vacancie : ValueObject<Vacancie>
    {
        public virtual int TotalVacancies { get; private set; }
        public virtual int NumVacancies { get; private set; }

        public virtual string DetailVacancies
        {
            get
            {
                return Format("{0} completes of {1}", NumVacancies, TotalVacancies);
            }
        }

        public virtual bool CompleteVacancies()
        {
            return TotalVacancies == NumVacancies;
        }

        public virtual void AddVacancie()
        {
            NumVacancies ++;
        }

        public bool ExistsVacancies(int vacancie)
        {
            return TotalVacancies >= vacancie;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TotalVacancies;
            yield return NumVacancies;
        }
    }
}