using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace GameGuessNumberLib {
    public class GuessGame {
        public void Start() {
            ConsolePrinter printer = new ConsolePrinter();
            var services = new ServiceCollection()
                .AddSingleton<IGameSettings, GameSettings>()
                .AddSingleton<IPrinter, ConsolePrinter>()
                .AddSingleton<Game>();

            var serviceProvider = services.BuildServiceProvider();
            var Game = serviceProvider.GetService<Game>();

            while (true) {
                Game?.ProcessGame();
                string answer = printer.Read<string>("Хотите сыграть еще раз? да/нет ");
                if (answer != "да") { printer.Print("Спасибо за игру!"); return; }
                answer = printer.Read<string>("Хотите изменить границы? да/нет ");
                if (answer == "да") Game?.ChangeBorder();
            }
        }
    }
}
