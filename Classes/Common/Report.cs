using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using PR52.Classes.Contexts;
using PR52.Pages;
using System;
using System.Collections.Generic;

namespace PR52.Classes.Common
{
    public class Report
    {
        public static void Group(int idGroup, Main main)
        {
            SaveFileDialog SFD = new SaveFileDialog()
            {
                InitialDirectory = @"C:\",
                Filter = "Excel (*.xlsx)|*.xlsx"
            };
            SFD.ShowDialog();
            if (SFD.FileName != "")
            {
                GroupContext Group = main.AllGroups.Find(x => x.Id == idGroup);
                var ExcelApp = new Application();
                try
                {
                    ExcelApp.Visible = false;
                    Workbook workbook = ExcelApp.Workbooks.Add(Type.Missing);
                    Worksheet worksheet = workbook.ActiveSheet;
                    (worksheet.Cells[1, 1] as Range).Value = $"Отчёт о группе {Group.Name}";
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 5]].Merge();
                    Styles(worksheet.Cells[1, 1], 18);
                    (worksheet.Cells[3, 1] as Range).Value = $"Список группы:";
                    worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[3, 5]].Merge();
                    Styles(worksheet.Cells[3, 1], 12, XlHAlign.xlHAlignLeft);
                    (worksheet.Cells[4, 1] as Range).Value = $"ФИО";
                    Styles(worksheet.Cells[4, 1], 12, XlHAlign.xlHAlignCenter, true);
                    (worksheet.Cells[4, 1] as Range).ColumnWidth = 35.0f;
                    (worksheet.Cells[4, 2] as Range).Value = $"Кол-во не сданных практических";
                    Styles(worksheet.Cells[4, 2], 12, XlHAlign.xlHAlignCenter, true);
                    (worksheet.Cells[4, 3] as Range).Value = $"Кол-во не сданных теоретических";
                    Styles(worksheet.Cells[4, 3], 12, XlHAlign.xlHAlignCenter, true);
                    (worksheet.Cells[4, 4] as Range).Value = "Отсутствовал на паре";
                    Styles(worksheet.Cells[4, 4], 12, XlHAlign.xlHAlignCenter, true);
                    (worksheet.Cells[4, 5] as Range).Value = $"Опоздал";
                    Styles(worksheet.Cells[4, 5], 12, XlHAlign.xlHAlignCenter, true);

                    int Height = 5;
                    List<StudentContext> students = main.AllStudents.FindAll(x => x.IdGroup == idGroup);
                    foreach (StudentContext student in students)
                    {
                        List<DisciplineContext> StudentDisciplines = main.AllDisciplines.FindAll(x => x.IdGroup == student.IdGroup);
                        int PracticeCount = 0;
                        int TheoryCount = 0;
                        int AbsenteeismCount = 0;
                        int LateCount = 0;
                        foreach (DisciplineContext StudentDiscipline in StudentDisciplines)
                        {
                            List<WorkContext> StudentWorks = main.AllWorks.FindAll(x => x.IdDiscipline == StudentDiscipline.Id);
                            foreach (WorkContext StudentWork in StudentWorks)
                            {
                                EvaluationContext evaluation = main.AllEvaluations.Find(x => x.IdWork == StudentWork.Id && x.IdStudent == student.Id);
                                if ((evaluation != null && (evaluation.Value.Trim() == "" || evaluation.Value.Trim() == "2")) || evaluation == null)
                                {
                                    if (StudentWork.IdType == 1) PracticeCount++;
                                    else if (StudentWork.IdType == 2) TheoryCount++;
                                }
                                if (evaluation != null && evaluation.Lateness.Trim() != "")
                                {
                                    if (Convert.ToInt32(evaluation.Lateness) == 90) AbsenteeismCount++;
                                    else LateCount++;
                                }
                            }
                        }
                        (worksheet.Cells[Height, 1] as Range).Value = $"{student.Lastname} {student.Name}";
                        Styles(worksheet.Cells[Height, 1], 12, XlHAlign.xlHAlignLeft, true);
                        (worksheet.Cells[Height, 2] as Range).Value = PracticeCount.ToString();
                        Styles(worksheet.Cells[Height, 2], 12, XlHAlign.xlHAlignCenter, true);
                        (worksheet.Cells[Height, 3] as Range).Value = TheoryCount.ToString();
                        Styles(worksheet.Cells[Height, 3], 12, XlHAlign.xlHAlignLeft, true);
                        (worksheet.Cells[Height, 4] as Range).Value = AbsenteeismCount.ToString();
                        Styles(worksheet.Cells[Height, 4], 12, XlHAlign.xlHAlignLeft, true);
                        (worksheet.Cells[Height, 5] as Range).Value = LateCount.ToString();
                        Styles(worksheet.Cells[Height, 5], 12, XlHAlign.xlHAlignLeft, true);
                        Height++;
                    }
                    workbook.SaveAs(SFD.FileName);
                    workbook.Close();
                }
                catch (Exception ex) { }
                ExcelApp.Quit();
            }
        }
        public static void Styles(Range Cell, int FontSize, XlHAlign Position = XlHAlign.xlHAlignCenter, bool Border = false)
        {
            Cell.Font.Name = "Bahnschrift Light Condensed";
            Cell.Font.Size = FontSize;
            Cell.HorizontalAlignment = Position;
            Cell.VerticalAlignment = XlHAlign.xlHAlignCenter;
            if (Border)
            {
                Borders border = Cell.Borders;
                border.LineStyle = XlLineStyle.xlDouble;
                border.Weight = XlBorderWeight.xlThin;
                Cell.WrapText = true;
            }
        }
    }
}
