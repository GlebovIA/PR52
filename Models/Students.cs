using System;

namespace PR52.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int IdGroup { get; set; }
        public bool Expelled { get; set; }
        public DateTime DateExpelled { get; set; }
        public Students(int id, string name, string lastname, int idGroup, bool expelled, DateTime dateExpelled)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            IdGroup = idGroup;
            Expelled = expelled;
            DateExpelled = dateExpelled;
        }
    }
}
