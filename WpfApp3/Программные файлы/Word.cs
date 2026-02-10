using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WpfApp3.EF;
using Xceed.Document.NET;
using Xceed.Words.NET;
namespace WpfApp3
{
    class Word
    {
        public void CreateWord(DocumentViewer documentViewer)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "output.docx");
            MessageBox.Show(filePath);
            using (DocX document = DocX.Create(filePath))
            {

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                List<Subjects> d = new();
                List<Groups> groups = new();
                using (ApplicationContext db = new ApplicationContext())
                {
                    d = db.Subjects.Where(r => r.lessons != null).ToList();
                    List<int> subId = d.Select(p => p.id).ToList();
                
                    groups = db.Groups.Where(h => h.GS.Any(i => subId.Contains(i.Subject))).ToList();
                }
                foreach (Groups group in groups)
                {
                    // Добавляем название группы
                    document.InsertParagraph($"Группа: {group.Name}");

                    // Создаем таблицу
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        Xceed.Document.NET.Table table = document.AddTable(1 + db.Lessons.Where(r => r.Group == group.Name).Count(), 4);
                        table.Design = TableDesign.LightShadingAccent1;

                        // Добавляем заголовки столбцов
                        table.Rows[0].Cells[0].Paragraphs[0].Append("День");
                        table.Rows[0].Cells[1].Paragraphs[0].Append("Номер");
                        table.Rows[0].Cells[2].Paragraphs[0].Append("Название");
                        table.Rows[0].Cells[3].Paragraphs[0].Append("Кабинет");

                        int rowIndex = 1;
                        foreach (var lesson in db.Lessons.Where(r => r.Group == group.Name))
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
                   /* using (DocX document1 = DocX.Load(filePath))
                    {
                        // Преобразуем документ в FlowDocument
                        FlowDocument flowDocument = new FlowDocument();

                        foreach (var paragraph in document.Paragraphs)
                        {
                            flowDocument.Blocks.Add(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(paragraph.Text)));
                        }

                        // Добавляем таблицу
                        foreach (var table in document.Tables)
                        {
                            var tableFlow = new System.Windows.Documents.Table();
                            tableFlow.Columns.Add(new TableColumn());
                            tableFlow.Columns.Add(new TableColumn());
                            tableFlow.Columns.Add(new TableColumn());
                            tableFlow.Columns.Add(new TableColumn()); // Добавляем четвертый столбец
                                var rowGroup = new TableRowGroup();

                            foreach (var row in table.Rows)
                            {
                                var rowFlow = new TableRow();
                                foreach (var cell in row.Cells)
                                {
                                    rowFlow.Cells.Add(new TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(cell.Paragraphs[0].Text))));
                                }

                                rowGroup.Rows.Add(rowFlow);


                            }
                                tableFlow.RowGroups.Add(rowGroup);
                            flowDocument.Blocks.Add(tableFlow);
                        }
                        var fixedDocument = new FixedDocument();
                        var pageContent = new PageContent();
                        var fixedPage = new FixedPage();
                        fixedPage.Children.Add(new System.Windows.Controls.Border
                        {
                            Child = new FlowDocumentScrollViewer
                            {
                                Document = flowDocument,
                                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                            }
                        });
                        pageContent.Child = fixedPage;
                        fixedDocument.Pages.Add(pageContent);

                        // Отображение документа
                        documentViewer.Document = fixedDocument;
                    }*/
                }
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                document.Save();
            }
            
        }
    }
}
