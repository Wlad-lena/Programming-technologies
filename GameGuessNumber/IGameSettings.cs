using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGuessNumberLib {
    internal interface IGameSettings {
        public int MinBorder { get; set; }
        public int MaxBorder { get; set; }
        public void set() { }
    }
}
