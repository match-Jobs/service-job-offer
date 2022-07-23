using System;
using System.Collections.Generic;
using static System.String;

namespace job_offer.JobOffers.Common.DomainModel.ValueObjects
{
    public class Detail : ValueObject<Detail>
    {
        public virtual string Tittle { get; private set; }
        public virtual string Description { get; private set; }

        public virtual string FullDescription
        {
            get
            {
                return Format("{0} - {1}", Tittle, Description);
            }
        }

        public static void CheckValidity(string description)
        {
            if (IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description), "Value cannot be empty");
            }

            if (description.Length < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(description), "Value cannot be shorter than 10 characters");
            }

            if (description.Length > 200)
            {
                throw new ArgumentOutOfRangeException(nameof(description), "Value cannot be longer than 200 characters");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Tittle;
            yield return Description;
        }
    }
}