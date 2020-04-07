using System.Collections.Generic;

namespace P07.MilitaryElit.Contracts
{
    public interface ICommando:ISpecialisedSoldier
    {
        IReadOnlyCollection<IMission> Missions { get; }
        void AddMission(IMission mission);
       
    }
}
