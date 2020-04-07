using P07.MilitaryElit.Enumerations;

namespace P07.MilitaryElit.Contracts
{
    public interface IMission
    {
        string CodeName { get; }
        State State { get; }
        void CompleteMission();
    }
}
