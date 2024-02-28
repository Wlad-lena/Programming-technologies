using System.IO;
using System.Threading.Tasks;

namespace Laba_1 {
    class Program {
        static void Main(string[] args) {
            // переменные хранят массивы экземпляров: резервуаров, установок, фабрик
            var tanks = GetTanks("C:\\Users\\Владлена\\Рабочий стол\\Laba-1\\t.txt");
            var units = GetUnits("C:\\Users\\Владлена\\Рабочий стол\\Laba-1\\u.txt");
            var factories = GetFactories("C:\\Users\\Владлена\\Рабочий стол\\Laba-1\\f.txt");


            while (true) {
                Console.WriteLine("\nВам доступны следующие дейстсвия:");
                Console.WriteLine("Выход (0)");
                Console.WriteLine("Вывести информацию о резервуаре (1), установке (2), фабрике (3)");
                Console.WriteLine("Вывод всех резервуаров, включая имена цеха и фабрики, в которых они числятся (4)");
                Console.WriteLine("Вывод общего объема резервуаров (5)");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice)) Console.WriteLine("Некоректный ввод");
                switch (choice) {
                    case 0: return;
                    case 1:
                        PrintByName(tanks);
                        break;
                    case 2:
                        PrintByName(units);
                        break;
                    case 3:
                        PrintByName(factories);
                        break;
                    case 4:
                        task_4(tanks, units, factories);
                        break;
                    case 5:
                        var totalVolume = GetTotalVolume(tanks);
                        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");
                        break;
                }
            }
        }
        // возвращает массив резервуаров считанный с файла
        public static Tank[] GetTanks(string path) {
            if (!File.Exists(path)) {
                Console.WriteLine("Файл не найден.");
                return new Tank[0];
            }

            // считываем из файла резервуары в массив строк
            string[] lines = File.ReadAllLines(path);
            // создаем массив для хранения резервуаров
            Tank[] tanks = new Tank[lines.Length];

            int i = 0;
            foreach (string line in lines) {
                string[] tmp = line.Split('\t'); // Разделяем строку на части с помощью табуляции
                // создаем i-ый резервуар и заполняем его данные из файла
                tanks[i] = new Tank {
                    ID = int.Parse(tmp[0]),
                    Name = tmp[1],
                    Description = tmp[2],
                    Volume = int.Parse(tmp[3]),
                    MaxVolume = int.Parse(tmp[4]),
                    UnitID = int.Parse(tmp[5])
                };
                i++;
            }
            return tanks;
        }
        // возвращает массив установок считанный с файла
        public static Unit[] GetUnits(string path) {
            if (!File.Exists(path)) {
                Console.WriteLine("Файл не найден.");
                return new Unit[0];
            }
            string[] lines = File.ReadAllLines(path);
            Unit[] units = new Unit[lines.Length];

            int i = 0;
            foreach (string line in lines) {
                string[] tmp = line.Split('\t');
                units[i] = new Unit {
                    ID = Convert.ToInt32(tmp[0]),
                    Name = tmp[1],
                    Description = tmp[2],
                    FactoryID = int.Parse(tmp[3]),
                };
                i++;
            }
            return units;
        }
        // возвращает массив заводов считанный с файла
        public static Factory[] GetFactories(string path) {
            if (!File.Exists(path)) {
                Console.WriteLine("Файл не найден.");
                return new Factory[0];
            }
            string[] lines = File.ReadAllLines(path);
            Factory[] factories = new Factory[10];

            int i = 0;
            foreach (string line in lines) {
                string[] tmp = line.Split('\t');
                factories[i] = new Factory {
                    ID = Convert.ToInt32(tmp[0]),
                    Name = tmp[1],
                    Description = tmp[2],
                };
                i++;
            }
            return factories;
        }

        // возвращает установку, которой принадлежит резервуар,
        // найденный в массиве резервуаров по имени
        public static Unit? FindUnit(Unit[] units, Tank[] tanks, string tankName) {
            int safe_unitID = -1;
            // пробегаемся по массиву резервуаров и находим нужный по имени
            for (int i = 0; i < tanks.Length; i++) {
                // сохраняем найденный ID установки
                if (tanks[i].Name == tankName) { safe_unitID = tanks[i].UnitID; break; }
            }
            if (safe_unitID == -1) return null;
            // пробегаемся по массиву установок чтобы найти нужный по сохраненному ID
            for (int i = 0; i < units.Length; i++) {
                if (units[i].ID == safe_unitID) return units[i];
            }
            //если дошли до сюда значит нужную установку не нашли
            return null;
        }

        // возвращает объект завода, соответствующий установке
        public static Factory? FindFactory(Factory[] factories, Unit unit) {
            for (int i = 0; i < factories.Length; i++) {
                if (factories[i].ID == unit.FactoryID) return factories[i];
            }
            return null;
        }

        // возвращает суммарный объем резервуаров в массиве
        public static int GetTotalVolume(Tank[] tanks) {
            int sum = 0;
            for (int i = 0; i < tanks.Length; i++)
                sum += tanks[i].Volume;
            return sum;
        }

        //вывод всех резервуаров, включая имена цеха и фабрики, в которых они числятся
        public static void task_4(Tank[] tanks, Unit[] units, Factory[] factories) {
            for (int i = 0; i < tanks.Length; i++) {
                Unit? unit = FindUnit(units, tanks, tanks[i].Name.ToString());
                if (unit == null) {
                    Console.WriteLine($"Мы не смогли найти {tanks[i].Name} в таблице установок");
                    break;
                }
                Factory? factory = FindFactory(factories, unit);
                if (factory == null) {
                    Console.WriteLine($"Мы не смогли найти нужный завод для установки: {unit}");
                    break;
                }
                Console.WriteLine($"{tanks[i].Name.ToString()} принадлежит установке {unit.Name} и заводу {factory.Name}");
            }
        }
        public static void PrintByName(Unit[] units) {
            string? name;
            do {
                Console.WriteLine("Введите название установки");
                name = Console.ReadLine();
            } while (name == "");
            for (int i = 0; i < units.Length; i++) {
                if (units[i].Name == name) {
                    Console.WriteLine($"ID = {units[i].ID}\nDescription = {units[i].Description}\nFactory ID = {units[i].FactoryID}\n");
                    break;
                }
                if (i == units.Length - 1) Console.WriteLine($"Мы не нашли {name}\n");
            }
        }
        public static void PrintByName(Factory[] factories) {
            string? name;
            do {
                Console.WriteLine("Введите название фабрики");
                name = Console.ReadLine();
            } while (name == "");
            for (int i = 0; i < factories.Length; i++) {
                if (factories[i].Name == name) {
                    Console.WriteLine($"ID = {factories[i].ID}\nDescription = {factories[i].Description}\n");
                    break;
                }
                if (i == factories.Length - 1) Console.WriteLine($"Мы не нашли {name}\n");
            }
        }
        public static void PrintByName(Tank[] tanks) {
            string? name;
            do {
                Console.WriteLine("Введите название резервуара");
                name = Console.ReadLine();
            } while (name == "");
            for (int i = 0; i < tanks.Length; i++) {
                if (tanks[i].Name == name) {
                    Console.WriteLine($"ID = {tanks[i].ID}\nDescription = {tanks[i].Description}\nVolume = {tanks[i].Volume}");
                    Console.WriteLine($"Max volume = {tanks[i].MaxVolume}\nID Unit = {tanks[i].UnitID}\n");
                    break;
                }
                if (i == tanks.Length - 1) Console.WriteLine($"Мы не нашли {name}\n");
            }
        }
    }

    /// <summary>
    /// Установка
    /// </summary>
    public class Unit {
        public Unit(string name, string desc, int iD, int facID) {
            Name = name;
            Description = desc;
            ID = iD;
            FactoryID = facID;
        }
        public Unit() { }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int ID { get; set; } = 0;
        public int FactoryID { get; set; } = 0;
    }

    /// <summary>
    /// Завод
    /// </summary>
    public class Factory {
        public Factory(string name, string desc, int iD) {
            Name = name;
            Description = desc;
            ID = iD;
        }
        public Factory() { }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int ID { get; set; } = 0;
    }

    /// <summary>
    /// Резервуар
    /// </summary>
    public class Tank {
        public Tank(string name, string desc, int vol, int maxVol, int unitID, int iD) {
            Name = name;
            Description = desc;
            Volume = vol;
            MaxVolume = maxVol;
            UnitID = unitID;
            ID = iD;
        }
        public Tank() { }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Volume { get; set; } = 0;
        public int MaxVolume { get; set; } = 0;
        public int UnitID { get; set; } = 0;
        public int ID { get; set; } = 0;

    }

}
