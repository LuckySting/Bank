using System;
using System.Globalization;
using System.IO;

namespace Bank
{
    /// <summary>
    /// Абстрактный класс клиента банка
    /// </summary>
    abstract class Client
    {
        /// <summary>
        /// Имя клиента
        /// </summary>
        protected string name;

        /// <summary>
        /// Дата заключения договора с банком
        /// </summary>
        protected string accessionDate;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя клиента</param>
        /// <param name="accessionDate">Дата присоединения</param>
        public Client(string name, string accessionDate)
        {
            this.name = name;
            this.accessionDate = accessionDate;
        }

        /// <summary>
        /// Проверка на принадлежность к дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Булево значение: совпадает дата с искомой или нет?</returns>
        public bool CheckDate(string date)
        {
            return date == this.accessionDate;
        }

        /// <summary>
        /// Метод вывода информации о клиенте на экран
        /// </summary>
        public abstract void Display();
    }

    /// <summary>
    /// Класс для вкладчика
    /// </summary>
    sealed class Contributor : Client
    {
        /// <summary>
        /// Размер вклада
        /// </summary>
        private double deposit;

        /// <summary>
        /// Процент по вкладу
        /// </summary>
        private double depositPercent;

        /// <summary>
        /// Дата открытия вклада
        /// </summary>
        private string depositStart
        {
            get
            {
                return this.accessionDate;
            }
            
            set
            {
                this.accessionDate = value;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Фамилия вкладчика</param>
        /// <param name="deposit">Размер вклада</param>
        /// <param name="depositPercent">Процент по вкладу</param>
        /// <param name="depositStart">Дата открытия вклада</param>
        public Contributor (string name, double deposit, double depositPercent, string depositStart) : base(name, depositStart)
        {
            this.deposit = deposit;
            this.depositPercent = depositPercent;
        }

        /// <summary>
        /// Метод выводит информацию о вкладчике на экран
        /// </summary>
        public sealed override void Display()
        {
            Console.WriteLine("Вкладчик " + this.name + ":");
            Console.WriteLine("    Размер вклада: " + this.deposit);
            Console.WriteLine("    Процент вклада: " + this.depositPercent);
            Console.WriteLine("    Дата открытия вклада: " + this.depositStart);
        }
    }

    /// <summary>
    /// Класс кредитора
    /// </summary>
    sealed class Creditor : Client
    {
        /// <summary>
        /// Сумма кредита
        /// </summary>
        private double credit;

        /// <summary>
        /// Процент по кредиту
        /// </summary>
        private double creditPercent;

        /// <summary>
        /// Остаток долга
        /// </summary>
        private double debt;

        /// <summary>
        /// Дата открытия кредита
        /// </summary>
        private string creditStart
        {
            get
            {
                return this.accessionDate;
            }

            set
            {
                this.accessionDate = value;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя кредитора</param>
        /// <param name="credit">Сумма кредита</param>
        /// <param name="creditPercent">Процент по кредиту</param>
        /// <param name="creditStart">Дата открытия кредита</param>
        /// <param name="debt">Остаток долга, по умлочанию равен сумме кредита</param>
        public Creditor(string name, double credit, double creditPercent, string creditStart, double debt = -1) : base(name, creditStart)
        {
            this.credit = credit;
            this.creditPercent = creditPercent;
            this.debt = debt == -1 ? credit : debt;
        }

        /// <summary>
        /// Метод выводит информацию о кредиторе на экран
        /// </summary>
        public sealed override void Display()
        {
            Console.WriteLine("Кредитор " + this.name + ":");
            Console.WriteLine("    Размер кредита: " + this.credit);
            Console.WriteLine("    Процент кредита: " + this.creditPercent);
            Console.WriteLine("    Дата выдачи кредита: " + this.creditStart);
            Console.WriteLine("    Остаток долга: " + this.debt);
        }
    }

    /// <summary>
    /// Класс организациии
    /// </summary>
    sealed class Organization : Client
    {
        /// <summary>
        /// Номер счёта
        /// </summary>
        private string accountNumber;

        /// <summary>
        /// Остаток на счёте
        /// </summary>
        private double accountValue;

        /// <summary>
        /// Дата открытия счёта
        /// </summary>
        private string accountStart
        {
            get
            {
                return this.accessionDate;
            }

            set
            {
                this.accessionDate = value;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя организации</param>
        /// <param name="accountNumber">Номер счёта</param>
        /// <param name="accountStart">Дата открытия счёта</param>
        /// /// <param name="accountValue">Остаток на счёте, по умолчанию 0</param>
        public Organization(string name, string accountNumber, string accountStart, double accountValue = 0) : base(name, accountStart)
        {
            this.accountNumber = accountNumber;
            this.accountValue = accountValue;
        }

        /// <summary>
        /// Метод выводит информацию об Организации на экран
        /// </summary>
        public sealed override void Display()
        {
            Console.WriteLine("Организация \"" + this.name + "\":");
            Console.WriteLine("    Номер счёта: " + this.accountNumber);
            Console.WriteLine("    Остаток на счёте: " + this.accountValue);
            Console.WriteLine("    Дата открытия счёта: " + this.accountStart);
        }
    }

    class Program
    {
        /// <summary>
        /// Загружает базу из файла
        /// </summary>
        /// <param name="fileName">Путь или имя файла</param>
        /// <returns>Массив клиентов банка</returns>
        private static Client[] ImportFromFile(string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                int clientCount = int.Parse(lines[0]);
                Client[] clients = new Client[clientCount];
                for (int i = 0; i < clientCount; i++)
                {
                    string clientType = lines[i + 1].Split(":")[0].Trim();
                    string[] args = lines[i + 1].Split(":")[1].Split(",");
                    switch (clientType)
                    {
                        case "contributor":
                            clients[i] = new Contributor(args[0].Trim(), double.Parse(args[1].Trim(), CultureInfo.InvariantCulture), double.Parse(args[2].Trim(), CultureInfo.InvariantCulture), args[3].Trim());
                            break;
                        case "creditor":
                            if (args.Length == 4)
                            {
                                clients[i] = new Creditor(args[0].Trim(), double.Parse(args[1].Trim(), CultureInfo.InvariantCulture), double.Parse(args[2].Trim(), CultureInfo.InvariantCulture), args[3].Trim());
                            } else
                            {
                                clients[i] = new Creditor(args[0].Trim(), double.Parse(args[1].Trim(), CultureInfo.InvariantCulture), double.Parse(args[2].Trim(), CultureInfo.InvariantCulture), args[3].Trim(), double.Parse(args[4].Trim(), CultureInfo.InvariantCulture));
                            }
                            
                            break;

                        case "organization":
                            if (args.Length == 3)
                            {
                                clients[i] = new Organization(args[0].Trim(), args[1].Trim(), args[2].Trim());
                            }
                            else { 
                                clients[i] = new Organization(args[0].Trim(), args[1].Trim(), args[2].Trim(), double.Parse(args[3].Trim(), CultureInfo.InvariantCulture));
                            }
                            break;

                         default:
                            break;
                     }
                }
                return clients;
            } catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Файл не найден");
                Console.ResetColor();
                return new Client[] { };
            }
        }

        /// <summary>
        /// Выводит всех клиентов банка в консоль
        /// </summary>
        /// <param name="clients">Массив клиентов банка</param>
        private static void DisplayAll(Client[] clients)
        {
            if (clients.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Нет ни одного клиента");
                Console.ResetColor();
                return;
            }
            foreach (var client in clients)
            {
                client.Display();
            }
        }

        /// <summary>
        /// Поиск по дате
        /// </summary>
        /// <param name="clients">массив клиентов банка</param>
        /// <param name="searchDate">Дата для поиска</param>
        private static void SearchByDate(Client[] clients, string searchDate)
        {
            bool zero = true;
            foreach(var client in clients)
            {
                if (client.CheckDate(searchDate))
                {
                    client.Display();
                    zero = false;
                }
            }
            if (zero)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Поиск не дал результатов");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Сценарий импорта из фйла
        /// </summary>
        /// <returns>массив клиентов банка</returns>
        static Client[] ShowImportFromFile()
        {
            Console.Write("Введите имя файла для импорта: ");
            string fileName = Console.ReadLine();
            return ImportFromFile(fileName);
        }

        /// <summary>
        /// Показать список доступных команд
        /// </summary>
        static void ShowHelp()
        {
            Console.WriteLine("Управление банком:");
            Console.WriteLine("    Помощь - help");
            Console.WriteLine("    Вывести всех клиентов - list");
            Console.WriteLine("    Поиск по дате - find");
            Console.WriteLine("    Очистить консоль - clear");
            Console.WriteLine("    Выход - quit");
        }

        /// <summary>
        /// Показать всех клиентов банка
        /// </summary>
        /// <param name="clients">массив клиентов банка</param>
        static void ShowClients(Client[] clients)
        {
            DisplayAll(clients);
        }

        /// <summary>
        /// Показать сценарий поиска по дате
        /// </summary>
        /// <param name="clients">массив клиентов банка</param>
        static void ShowSearch(Client[] clients)
        {
            Console.Write("Поиск по дате (дд.мм.гггг): ");
            string searchString = Console.ReadLine();
            SearchByDate(clients, searchString);
        }

        /// <summary>
        /// Основная логика программы и CLI
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Client[] clients = ShowImportFromFile();
            ShowHelp();
            while (true)
            {
                Console.Write(">");
                string cmd = Console.ReadLine().Trim();
                switch (cmd)
                {
                    case "help":
                        ShowHelp();
                        break;
                    case "list":
                        ShowClients(clients);
                        break;
                    case "find":
                        ShowSearch(clients);
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "quit":
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }
    }
}
