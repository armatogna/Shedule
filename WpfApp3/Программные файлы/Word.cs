using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.EF;
using Xceed.Document.NET;
using Xceed.Words.NET;
namespace WpfApp3
{
    class Word
    {
        public void CreateWord()
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.docx");
            using (ApplicationContext db = new ApplicationContext())
            {
                using (DocX document = DocX.Create(filePath))
                {

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                    List<Subjects> d = db.Subjects.Where(r => r.lessons != null).ToList();
                    List<int> subId = d.Select(p => p.id).ToList();

                    foreach (Groups group in db.Groups.Where(h => h.GS.Any(i => subId.Contains(i.Subject))))
                    {
                        // Добавляем название группы
                        document.InsertParagraph($"Группа: {group.Name}");

                        // Создаем таблицу
                        Xceed.Document.NET.Table table = document.AddTable(1 + db.Lessons.Where(r => r.group == group.Name).Count(), 4);
                        table.Design = TableDesign.LightShadingAccent1;

                        // Добавляем заголовки столбцов
                        table.Rows[0].Cells[0].Paragraphs[0].Append("День");
                        table.Rows[0].Cells[1].Paragraphs[0].Append("Номер");
                        table.Rows[0].Cells[2].Paragraphs[0].Append("Название");
                        table.Rows[0].Cells[2].Paragraphs[0].Append("Кабинет");

                        int rowIndex = 1;
                        foreach (var lesson in db.Lessons.Where(r => r.group == group.Name))
                        {
                            // Добавляем строки таблицы
                            table.Rows[rowIndex].Cells[0].Paragraphs[0].Append(lesson.Day);
                            table.Rows[rowIndex].Cells[1].Paragraphs[0].Append(lesson.NumDay.ToString());
                            table.Rows[rowIndex].Cells[2].Paragraphs[0].Append(lesson.Name);
                            table.Rows[rowIndex].Cells[3].Paragraphs[0].Append(lesson.Cabinet);
                            rowIndex++;
                        }

                        // Добавляем таблицу в документ
                        document.InsertTable(table);

                    }
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                    document.Save();
                }
            }
        }
    }
}
