namespace events {
    internal class Program {
        public class UserEnteredNumberEventArgs {
            public int Input { get; set; }
            public DateTime EnteredAt { get; set; }
        }

        // Событие
        public static event EventHandler<UserEnteredNumberEventArgs> UserEnteredNumber;

        // Функция вывода числа введенного пользователем
        private static void PrintUserEnteredNumber(object sender, UserEnteredNumberEventArgs e) {
            Console.WriteLine($"\nПользователь ввел число {e.Input} в {e.EnteredAt}\n");
        }

        static void Main(string[] args) {
            string input;
            int number;

            UserEnteredNumber += PrintUserEnteredNumber;

            while (true) {
                Console.Write("Введите число > ");

                input = Console.ReadLine();

                // Проверка что строка является числом
                if (int.TryParse(input, out number)) {
                    // Вызываем событие с объектом аргументов
                    UserEnteredNumber?.Invoke(null, new UserEnteredNumberEventArgs { Input = number, EnteredAt = DateTime.Now });
                }
            }
        }
    }
}