using System.Collections.Generic;
using System.Linq;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Repositories.Contracts;

namespace SantaWorkshop.Repositories
{
    class DwarfRepository : IRepository<IDwarf>
    {
        private readonly List<IDwarf> dwarfs;

        public DwarfRepository()
        {
            this.dwarfs = new List<IDwarf>();
        }

        public IReadOnlyCollection<IDwarf> Models => this.dwarfs.AsReadOnly();
        public void Add(IDwarf model)
        { 
            this.dwarfs.Add(model);
        }

        public bool Remove(IDwarf model)
        {
            return this.dwarfs.Remove(model);
        }

        public IDwarf FindByName(string name)
        {
            return this.dwarfs.FirstOrDefault(n => n.Name == name);
        }

    }
}
