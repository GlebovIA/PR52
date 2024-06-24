using PR52.Classes.Common;
using PR52.Classes.Contexts;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace PR52.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public List<GroupContext> AllGroups = GroupContext.AllGroups();
        public List<StudentContext> AllStudents = StudentContext.AllStudents();
        public List<WorkContext> AllWorks = WorkContext.AllWorks();
        public List<EvaluationContext> AllEvaluations = EvaluationContext.AllEvaluations();
        public List<DisciplineContext> AllDisciplines = DisciplineContext.AllDisciplines();
        public Main()
        {
            InitializeComponent();
            CreateGropUI();
            CreateStudents(AllStudents);

        }
        public void CreateGropUI()
        {
            foreach (GroupContext group in AllGroups)
                GroupCB.Items.Add(group.Name);
            GroupCB.Items.Add("Выберите");
            GroupCB.SelectedIndex = GroupCB.Items.Count - 1;
        }
        public void CreateStudents(List<StudentContext> students)
        {
            Parent.Children.Clear();
            foreach (StudentContext student in students)
                Parent.Children.Add(new Items.Student(student, this));
        }
        private void SelectGroup(object sender, SelectionChangedEventArgs e)
        {
            if (GroupCB.SelectedIndex != GroupCB.Items.Count - 1)
            {
                int IdGroup = AllGroups.Find(x => x.Name == GroupCB.SelectedItem).Id;
                CreateStudents(AllStudents.FindAll(x => x.IdGroup == IdGroup));
            }
        }
        private void SelectStudents(object sender, KeyEventArgs e)
        {
            List<StudentContext> SearchStudent = AllStudents;
            if (GroupCB.SelectedIndex != GroupCB.Items.Count - 1)
            {
                int IdGroup = AllGroups.Find(x => x.Name == GroupCB.SelectedItem).Id;
                SearchStudent = AllStudents.FindAll(x => x.IdGroup == IdGroup);
            }
            CreateStudents(SearchStudent.FindAll(x => $"{x.Lastname}.{x.Name}".Contains(FioTBx.Text)));
        }

        private void ReportGeneration(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GroupCB.SelectedIndex != GroupCB.Items.Count - 1)
            {
                int IdGroup = AllGroups.Find(x => x.Name == GroupCB.SelectedItem).Id;
                Report.Group(IdGroup, this);
            }
        }
    }
}
