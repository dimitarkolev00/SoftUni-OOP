using P03.Telephony.Contracts;
using P03.Telephony.Exceptions;
using System.Linq;

namespace P03.Telephony.Models
{
    public class StationaryPhone : ICallable
    {
        public StationaryPhone()
        {

        }
        public string Call(string number)
        {
            if (!number.All(ch=>char.IsDigit(ch)))
            {
                throw new InvalidNumberException();
            }
            return $"Dialing... {number}";
        }
    }
}
