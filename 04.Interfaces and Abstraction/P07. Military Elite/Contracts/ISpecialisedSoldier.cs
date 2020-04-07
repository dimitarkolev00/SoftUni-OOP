using P07.MilitaryElit.Enumerations;

namespace P07.MilitaryElit.Contracts
{
    public interface ISpecialisedSoldier:IPrivate
    {
        Corps Corps { get; }
    }
}
