using System;   

using Microsoft.Extensions.DependencyInjection;


namespace GameGuessNumberLib {
    internal class Program {
        static void Main(string[] args) {
            var Game = new GuessGame();
            Game.Start();
        }
    }
}
