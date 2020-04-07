﻿using System.Collections.Generic;

namespace P07.MilitaryElit.Contracts
{
    public interface ILieutenantGeneral : IPrivate
    {
        IReadOnlyCollection<ISoldier> Privates { get; }
        void AddPrivate(ISoldier @private);
        
    }
}
