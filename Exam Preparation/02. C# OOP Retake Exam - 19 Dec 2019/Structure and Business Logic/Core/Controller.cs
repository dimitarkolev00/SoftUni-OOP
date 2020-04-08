using System;
using System.Linq;
using System.Text;

using SantaWorkshop.Core.Contracts;
using SantaWorkshop.Models.Dwarfs;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops;
using SantaWorkshop.Repositories;
using SantaWorkshop.Utilities.Messages;

namespace SantaWorkshop.Core
{
    public class Controller : IController
    {
        private DwarfRepository dwarfRepository;
        private PresentRepository presentRepository;
        public Controller()
        {
            this.dwarfRepository = new DwarfRepository();
            this.presentRepository = new PresentRepository();
        }

        public string AddDwarf(string dwarfType, string dwarfName)
        {
            IDwarf dwarf = null;

            if (dwarfType == "HappyDwarf")
            {
                dwarf = new HappyDwarf(dwarfName);

            }
            else if (dwarfType == "SleepyDwarf")
            {
                dwarf = new SleepyDwarf(dwarfName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDwarfType);
            }

            this.dwarfRepository.Add(dwarf);
            return String.Format(OutputMessages.DwarfAdded, dwarfType, dwarfName);
        }

        public string AddInstrumentToDwarf(string dwarfName, int power)
        {
            Instrument currentInstrument = new Instrument(power);
            var currentDwarf = this.dwarfRepository.FindByName(dwarfName);

            if (currentDwarf == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentDwarf);
            }

            currentDwarf.AddInstrument(currentInstrument);

            return String.Format(OutputMessages.InstrumentAdded, power, dwarfName);
        }

        public string AddPresent(string presentName, int energyRequired)
        {
            Present currentPresent = new Present(presentName, energyRequired);
            this.presentRepository.Add(currentPresent);

            return String.Format(OutputMessages.PresentAdded, presentName);
        }

        public string CraftPresent(string presentName)
        {
            Workshop workshop = new Workshop();

            var presentToCraft = this.presentRepository.FindByName(presentName);

            bool dwarfsAreCapable = this.dwarfRepository.Models.Any(d => d.Energy >= 50);

            if (!dwarfsAreCapable)
            {
                throw new InvalidOperationException(ExceptionMessages.DwarfsNotReady);
            }

            var capableDwarfs = this.dwarfRepository.Models
                .Where(d => d.Energy >= 50).OrderByDescending(e => e.Energy).ToList();

            IDwarf currentDwarf;

            while (capableDwarfs.Any() && !presentToCraft.IsDone())
            {
                currentDwarf = capableDwarfs.First();

                workshop.Craft(presentToCraft, currentDwarf);

                if (currentDwarf.Energy == 0)
                {
                    this.dwarfRepository.Remove(currentDwarf);
                    capableDwarfs.Remove(currentDwarf);
                }
                else
                {
                    if (!currentDwarf.Instruments.Any(i => i.IsBroken() == false))
                    {
                        capableDwarfs.Remove(currentDwarf);
                    }
                }
            }

            if (presentToCraft.IsDone())
            {
                return string.Format(OutputMessages.PresentIsDone, presentName);
            }
            
            return string.Format(OutputMessages.PresentIsNotDone, presentName);
        }

        public string Report()
        {
            var presentsDone = presentRepository.Models
                .Where(p => p.IsDone()==true).Count();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{presentsDone} presents are done!")
                .AppendLine("Dwarfs info:");

            foreach (var dwarf in this.dwarfRepository.Models)
            {
                sb.AppendLine(dwarf.ToString());
            }

            return sb.ToString().TrimEnd();
        }
      
    }
}

