using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGuessNumberLib {
    internal class ConsolePrinter : IPrinter {
        public void Print(string str) {
            Console.WriteLine(str);
        }
        // считывание информации с консоли
        public T Read<T>()  {
            T value;
            string? str = null;
            while (str == null) {
                str = Console.ReadLine();
            }
            while (true) {
                try {
                    value = (T)Convert.ChangeType(str, typeof(T));
                    break;
                }
                catch (Exception) {
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    str = Console.ReadLine();
                }
            }
            return value;
        }
        // считывание информации с консоли и вывод сообщения
         public T Read<T>(string message)  {
            Console.WriteLine(message);
            T value;
            string? str = null;
            while (str == null) {
                str = Console.ReadLine();
            }
            while (true) {
                try {
                    value = (T)Convert.ChangeType(str, typeof(T));
                    break;
                }
                catch (Exception) {
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    str = Console.ReadLine();
                }
            }
            return value;
        }
        // очистка экрана
        public void Clear() {
            Console.Clear();
        }
        public void Wait() {
            // Ожидание нажатия клавиши
            Print("Нажмите любую клавишу для продолжения");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
        }
    }
}
