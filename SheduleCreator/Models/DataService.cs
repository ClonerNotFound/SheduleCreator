using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SheduleCreator.Models
{
    public class DataService
    {
        private const string SubjectsFilePath = "subjects.json";
        private const string TeachersFilePath = "teachers.json";

        public List<Subject> LoadSubjects()
        {
            if (File.Exists(SubjectsFilePath))
            {
                string json = File.ReadAllText(SubjectsFilePath);
                return JsonConvert.DeserializeObject<List<Subject>>(json);
            }
            return new List<Subject>();
        }

        public List<Teacher> LoadTeachers()
        {
            if (File.Exists(TeachersFilePath))
            {
                string json = File.ReadAllText(TeachersFilePath);
                return JsonConvert.DeserializeObject<List<Teacher>>(json);
            }
            return new List<Teacher>();
        }

        public void SaveSubjects(List<Subject> subjects)
        {
            string json = JsonConvert.SerializeObject(subjects, Formatting.Indented);
            File.WriteAllText(SubjectsFilePath, json);
        }

        public void SaveTeachers(List<Teacher> teachers)
        {
            string json = JsonConvert.SerializeObject(teachers, Formatting.Indented);
            File.WriteAllText(TeachersFilePath, json);
        }
    }
}
