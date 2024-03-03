using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkSeminar6
{
    internal struct Worker
    {
        public Worker()
        {
        }
        public int ID { get; set; } = -1;
        public DateTime CreationDateTime { get; set; } = DateTime.MinValue;
        public string FullName { get; set; } = "";
        public int Age => DateTime.Today.Year - DateOfBirth.Year;
        public int Height { get; set; } = 0;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string PlaceOfBirth { get; set; } = "";
    }
}
