using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleCreator.Models
{
    public class SubjectHours
    {
        public string Subject { get; set; } // Название предмета
        public int HoursPerWeek { get; set; } // Количество часов в неделю
    }
}
