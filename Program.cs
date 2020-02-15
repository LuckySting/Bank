using System;

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
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
