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

    sealed class Contributor : Client
    {
        private double deposit;
        private double depositPercent;
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

        public Contributor (string name, double deposit, double depositPercent, string depositStart) : base(name, depositStart)
        {
            this.deposit = deposit;
            this.depositPercent = depositPercent;
        }

        public sealed override void Display()
        {
            Console.WriteLine("Вкладчик " + this.name + ":");
            Console.WriteLine("    Размер вклада: " + this.deposit);
            Console.WriteLine("    Процент вклада: " + this.depositPercent);
            Console.WriteLine("    Дата открытия вклада: " + this.depositStart);
        }
    }

    sealed class Creditor : Client
    {
        private double credit;
        private double creditPercent;
        private double debt;
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

        public Creditor(string name, double credit, double creditPercent, string creditStart, double debt = -1) : base(name, creditStart)
        {
            this.credit = credit;
            this.creditPercent = creditPercent;
            this.debt = debt == -1 ? credit : debt;
        }

        public sealed override void Display()
        {
            Console.WriteLine("Кредитор " + this.name + ":");
            Console.WriteLine("    Размер кредита: " + this.credit);
            Console.WriteLine("    Процент кредита: " + this.creditPercent);
            Console.WriteLine("    Дата выдачи кредита: " + this.creditStart);
            Console.WriteLine("    Остаток долга: " + this.debt);
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
