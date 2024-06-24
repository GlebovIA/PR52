using System;

namespace PR52.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public bool Expelled { get; set; }
        public DateTime DateExpelled { get; set; }
        public Students(int id, string surname, string name, string lastname, bool expelled, DateTime dateExpelled)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Lastname = lastname;
            Expelled = expelled;
            DateExpelled = dateExpelled;
        }
    }
}
