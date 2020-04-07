
using System.Collections.Generic;

namespace P07.MilitaryElit.Contracts
{
    public interface IEngineer : ISpecialisedSoldier
    {
        IReadOnlyCollection<IRepair> Reapairs { get; }
        void AddRepair(IRepair repair);
    }
}
