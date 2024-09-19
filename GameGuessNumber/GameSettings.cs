using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGuessNumberLib {
    internal class GameSettings : IGameSettings {
        public int MinBorder { get; set; }
        public int MaxBorder { get; set; }
        private IPrinter printer;
        public GameSettings(IPrinter _printer) {
            printer = _printer;
        }
        public void set() {
            int min = printer.Read<int>("Введите минимальное значение: ");
            int max = printer.Read<int>("Введите максимальное значение: ");
            MinBorder = min; MaxBorder = max;
        }
    }
}
