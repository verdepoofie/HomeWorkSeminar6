namespace HomeWorkSeminar6
{
    internal class Repository
    {
        public string FileName => "repository.txt";
        public Repository()
        {
            if (!File.Exists(FileName))
                File.Create(FileName).Close();
        }
        public Worker[] GetAllWorkers()
        {
            string[] wholeFile = File.ReadAllLines(FileName);
            List<Worker> workers = new List<Worker>();
            if (wholeFile.Length > 0)
            {
                foreach (string worker in wholeFile)
                {
                    string[] workerElements = worker.Split('#');
                    workers.Add(new Worker
                    {
                        ID = int.Parse(workerElements[0]),
                        CreationDateTime = DateTime.Parse(workerElements[1]),
                        FullName = workerElements[2],
                        Height = int.Parse(workerElements[4]),
                        DateOfBirth = DateTime.Parse(workerElements[5]),
                        PlaceOfBirth = workerElements[6]
                    });
                }
                return workers.ToArray();
            }
            else
                return new Worker[0];
        }

        public Worker GetWorkerById(int id)
        {
            return GetAllWorkers().FirstOrDefault(x => x.ID == id);
        }

        public void DeleteWorker(int id)
        {
            var allWorkers = GetAllWorkers().ToList();
            var workerToDelete = GetWorkerById(id);
            allWorkers.Remove(workerToDelete);
            File.Create(FileName).Close();
            foreach (Worker worker in allWorkers)
                AddWorker(worker);
        }
        public void AddWorker(Worker worker)
        {
            File.AppendAllText(FileName,
                worker.ID.ToString() + "#" +
                worker.CreationDateTime.ToString("G") + "#" +
                worker.FullName + "#" +
                worker.Age.ToString() + "#" +
                worker.Height.ToString() + "#" +
                worker.DateOfBirth.ToString("yyyy.MM.dd") + "#" +
                worker.PlaceOfBirth + "\n");
        }

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            return GetAllWorkers().ToList().FindAll(x => x.CreationDateTime >= dateFrom && x.CreationDateTime < dateTo).ToArray();
        }
    }
}
