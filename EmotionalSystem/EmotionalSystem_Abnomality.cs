using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmotionalSystem.B_LucyEmotionalLevel;

namespace EmotionalSystem
{
    public class Abnormality
    {
        public string Name { get; set; }
        public AbnoType Type { get; set; }
        public int Level { get; set; }
        public Abnormality(string name, AbnoType type, int level)
        {
            Name = name;
            Type = type;
            Level = level;
        }
    }

    public enum AbnoType
    {
        Pos = 0,
        Neg = 1,
        EGO = 2
    }
}
