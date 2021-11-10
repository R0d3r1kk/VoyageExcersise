using System;
using System.Collections.Generic;

namespace VoyageExcercise.Models
{
    public class MediatorDescriptionOptions
    {
        public Type StartupClassType { get; set; }
        public IEnumerable<string> Assembly { get; set; }
    }
}
