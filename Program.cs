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
        /// Метод вывода информации о клиенте на экран
        /// </summary>
        public abstract void Display();
        /// <summary>
        /// Проверка на принадлежность к дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Булево значение: совпадает дата с искомой или нет?</returns>
        public bool CheckDate(string date)
        {
            return date == this.accessionDate;
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
