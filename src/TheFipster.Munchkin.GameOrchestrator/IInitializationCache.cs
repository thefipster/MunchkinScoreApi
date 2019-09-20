using System;
using System.Collections.Generic;
using System.Text;

namespace TheFipster.Munchkin.GameOrchestrator
{
    public interface IInitializationCache
    {
        bool CheckInitCode(string gameInitId);
        string GenerateInitCode();
    }
}
