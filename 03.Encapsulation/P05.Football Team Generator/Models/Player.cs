using System;

using P05.FootballTeamGenerator.Common;

namespace P05.FootballTeamGenerator.Models
{
    public class Player
    {
        private string name;

        public Player(string name , Stats stats)
        {
            this.Name = name;
            this.Stats = stats;
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(GlobalConstants.EmptyNameExceptionMessage);
                }
                this.name = value;
            }
        }
        public double OverallSkill => this.Stats.AverageStats;
        public Stats Stats { get; }
    }
}
