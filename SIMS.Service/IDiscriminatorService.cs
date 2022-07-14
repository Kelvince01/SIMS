using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IDiscriminatorService
    {
        IEnumerable<Discriminator> Gets(string name = null);

        Discriminator Get(Decimal id);

        Discriminator Get(string name);

        void CreateUpdate(Discriminator model);

        void Update(Discriminator model);

        void Remove(Discriminator model);

        void Save();

        bool IsDuplicate(Discriminator model);

        List<Discriminator> GetServerDiscriminator();
    }
}
