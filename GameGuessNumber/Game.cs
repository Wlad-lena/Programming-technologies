using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGuessNumberLib {
    internal class Game {
        private int guessNumb {  get; set; }
        private int cntAttempts { get; set; } 
        private IGameSettings _GameSettings;
        private IPrinter printer;
        // конструктор игры определяющий ее настройки и способ ввода/вывода информации
        public Game(IGameSettings gameSettings, IPrinter _printer) {
            _GameSettings = gameSettings;
            printer = _printer;
            _GameSettings.set();
            Console.WriteLine("Игра Угадай число");
        }
        // метод создания рандомного числа
        private void RandomNumb(int min, int max) {
            Random random = new Random();
            guessNumb = random.Next(min, max+1);
        }
        // инициализация настроек игры
        private void Settings() { 
            RandomNumb(_GameSettings.MinBorder, _GameSettings.MaxBorder);
        }
        public void ChangeBorder() {
            int min = printer.Read<int>("Введите новое минимальное значение: ");
            int max = printer.Read<int>("Введите новое максимальное значение: ");
            _GameSettings.MinBorder = min;
            _GameSettings.MaxBorder = max;
        }
        // метод самой игры с угадыванием числа
        public void ProcessGame() {
            Settings();
            cntAttempts = 0;
            while (true) {
                cntAttempts++;
                int userNumb = printer.Read<int>("Введите число: ");
                if (userNumb == guessNumb) {
                    printer.Print("Вы выиграли АВТОМОБИЛЬ");
                    break;
                }
                else if (userNumb > guessNumb) printer.Print("Слишком много");
                else printer.Print("Слишком мало");
            }
            printer.Print("Вы угадали число " + guessNumb + ". Количество попыток: " + cntAttempts);
        }
    }
}
