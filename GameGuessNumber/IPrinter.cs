using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGuessNumberLib {
    public interface IPrinter {
        public void Print(string str) { }
        public T Read<T>() { return default(T); }
        public T Read<T>(string str) { return default(T); }
    }
}
