﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class RaceMotorcycle : Motorcycle
    {

        public override double FuelConsumption { get; set; }

        public RaceMotorcycle(int horsePower, double fuel) 
               : base(horsePower, fuel)
        {
            this.FuelConsumption = 8;
        }

        

    }
}