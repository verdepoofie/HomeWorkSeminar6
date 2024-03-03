using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeWorkSeminar6
{
    internal class Exercise
    {
        private Repository repository = new Repository();
        private void DisplayRecords(bool byDate = false)
        {
            try
            {
                var recordsToDisplay = new Worker[0];
                if (byDate)
                {
                    Console.WriteLine("Введите с какой даты выводить: ");
                    DateTime fromDate = (DateTime)GetInput("Date");
                    Console.WriteLine("Введите до какой даты выводить: ");
                    DateTime toDate = (DateTime)GetInput("Date");
                    if (toDate <= fromDate)
                        throw new Exception("Даты введены неверно");
                    Console.WriteLine("Записи сотрудников сделанные с " + fromDate.ToString("yyyy.MM.dd") + " по " + toDate.ToString("yyyy.MM.dd"));;
                    recordsToDisplay = repository.GetWorkersBetweenTwoDates(fromDate, toDate);
                }
                else
                {
                    Console.WriteLine("Записи сотрудников");
                    recordsToDisplay = repository.GetAllWorkers();
                }
                Console.WriteLine("ID\tДата и время создания\tФ.И.О\tВозраст\tРост (см)\tДата рождения\tМесто рождения");
                foreach (var record in recordsToDisplay)
                    Console.WriteLine(
                        record.ID.ToString() + "\t" +
                        record.CreationDateTime.ToString("G") + "\t" +
                        record.FullName + "\t" +
                        record.Age.ToString() + "\t" +
                        record.Height.ToString() + "\t" +
                        record.DateOfBirth.ToString("yyyy.MM.dd") + "\t" +
                        record.PlaceOfBirth + "\t");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Произошла ошибка:\n" + ex.Message);
            }
            
        }
        private void AddRecord()
        {
            try
            {
                Worker worker = new Worker();
                string[] file = File.ReadAllLines(repository.FileName);
                if (file.Length > 0)
                    worker.ID = int.Parse(file[file.Length - 1].Split('#')[0]) + 1;
                else
                    worker.ID = 0;
                worker.CreationDateTime = DateTime.Now;
                Console.WriteLine("Введите Ф.И.О.");
                worker.FullName = (string)GetInput(nameof(worker.FullName));
                Console.WriteLine("Введите дату рождения в формате день.месяц.год");
                worker.DateOfBirth = (DateTime)GetInput(nameof(worker.DateOfBirth));
                Console.WriteLine("Введите место рождения");
                worker.PlaceOfBirth = (string)GetInput(nameof(worker.PlaceOfBirth));
                Console.WriteLine("Введите рост в см");
                worker.Height = (int)GetInput(nameof(worker.Height));
                repository.AddWorker(worker);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка:\n" + ex.Message);
            }
        }
        private object GetInput(string inputType)
        {
            while (true)
            {
                var input = Console.ReadLine();
                switch (inputType)
                {
                    case "FullName":
                        if (!string.IsNullOrEmpty(input) && input.Split(' ').Length == 3)
                            return input;
                        break;
                    case "Height":
                        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int height) && height > 0)
                            return height;
                        break;
                    case "DateOfBirth":
                        if (!string.IsNullOrEmpty(input) && DateTime.TryParse(input, out DateTime date) && date < DateTime.Today)
                            return date;
                        break;
                    case "PlaceOfBirth":
                        if (!string.IsNullOrEmpty(input))
                            return input;
                        break;
                    case "Date":
                        if(!string.IsNullOrEmpty(input) && DateTime.TryParse(input, out date))
                            return date;
                        break;
                    default:
                        return new object();
                }
                Console.WriteLine("Не верный ввод");
            }
        }
        private void DeleteRecord()
        {
            try
            {
                DisplayRecords();
                Console.WriteLine("Введите ID сотрудника, который хотите удалить");
                var input = Console.ReadLine();
                while (true) 
                {
                    if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int id))
                    {
                        repository.DeleteWorker(id);
                        break;
                    }
                    Console.WriteLine("Не верный ввод");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка:\n" + ex.Message);
            }
        }
        
        public void Execute()
        {
            Console.WriteLine("Задание, справочник «Сотрудники» продолжение");
            
            
            while (true)
            {
                Console.WriteLine("Введите 1 для вывода справочника на экран, " +
                    "2 для добавления сотрудника в справочник, " +
                    "3 для удаления сотрудника по ID, " +
                    "4 для вывода сотрудников по дате записи, " +
                    "Q для выхода");
                var answer = Console.ReadKey();
                Console.WriteLine();
                if (answer.Key != ConsoleKey.Q)
                {
                    switch (answer.Key)
                    {
                        case ConsoleKey.D1:
                            DisplayRecords();
                            break;
                        case ConsoleKey.D2:
                            AddRecord();
                            break;
                        case ConsoleKey.D3:
                            DeleteRecord();
                            break;
                        case ConsoleKey.D4:
                            DisplayRecords(true);
                            break;
                    }
                }
                else
                    break;
            }
        }
    }
}
