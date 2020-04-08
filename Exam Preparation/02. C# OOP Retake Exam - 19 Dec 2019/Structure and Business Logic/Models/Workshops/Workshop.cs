using System.Collections.Generic;
using System.Linq;

using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments.Contracts;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops.Contracts;

namespace SantaWorkshop.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {
        }
        public void Craft(IPresent present, IDwarf dwarf)
        {
            bool hasEnergy = dwarf.Energy > 0;
            bool hasInstrument = dwarf.Instruments.Any(i => i.IsBroken() == false);

            if (hasEnergy && hasInstrument)
            {

                while (!present.IsDone() && hasEnergy && hasInstrument)
                {
                    IInstrument currentInstrument = dwarf.Instruments
                        .FirstOrDefault(i => i.IsBroken() == false);

                    if (currentInstrument == null)
                    {
                        break;
                    }

                    present.GetCrafted();
                    dwarf.Work();
                    currentInstrument.Use();
                }
            }

        }
    }
}
