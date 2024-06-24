using PR52.Classes.Contexts;
using PR52.Pages;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PR52.Items
{
    /// <summary>
    /// Логика взаимодействия для Student.xaml
    /// </summary>
    public partial class Student : UserControl
    {
        public Student(StudentContext student, Main main)
        {
            InitializeComponent();
            FioTBx.Text = $"{student.Lastname} {student.Name}";
            ExpelledCB.IsChecked = student.Expelled;
            List<DisciplineContext> StudentDisciplines = main.AllDisciplines.FindAll(x => x.IdGroup == student.IdGroup);
            int NecessaryCount = 0;
            int WorkCount = 0;
            int DoneCount = 0;
            int MissedCount = 0;
            foreach (DisciplineContext StudentDiscipline in StudentDisciplines)
            {
                List<WorkContext> StudentWorks = main.AllWorks.FindAll(x => (x.IdType == 1 || x.IdType == 2 || x.IdType == 3) && x.IdDiscipline == StudentDiscipline.Id);
                NecessaryCount += StudentWorks.Count;
                foreach (WorkContext StudentWork in StudentWorks)
                {
                    EvaluationContext evaluation = main.AllEvaluations.Find(x => x.IdWork == StudentWork.Id && x.IdStudent == student.Id);
                    if (evaluation != null && evaluation.Value.Trim() != "" && evaluation.Value.Trim() != "2")
                        DoneCount++;
                }
                StudentWorks = main.AllWorks.FindAll(x => x.IdType != 4 && x.IdType != 3);
                WorkCount += StudentWorks.Count;
                foreach (WorkContext StudentWork in StudentWorks)
                {
                    EvaluationContext evaluation = main.AllEvaluations.Find(x => x.IdWork == StudentWork.Id && x.IdStudent == student.Id);
                    if (evaluation != null && evaluation.Lateness.Trim() != "")
                        MissedCount += Convert.ToInt32(evaluation.Lateness);
                }
                doneWorks.Value = (100f / (float)NecessaryCount) * ((float)DoneCount);
                missedCount.Value = (100f / ((float)WorkCount * 90f)) * ((float)MissedCount);
                GroupTBx.Text = main.AllGroups.Find(x => x.Id == student.IdGroup).Name;
            }
        }
    }
}
