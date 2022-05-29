using ClosedXML.Excel;
using Core;
using System.Text;

namespace GeneratorWeb.Service
{
    public class ExportService
    {
        public string GetCSV(List<Data> DataList, List<List<object>> GeneratedData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Join(",", DataList.Select(data => data.FieldName)));

            foreach (var line in GeneratedData)
            {
                stringBuilder.AppendLine(string.Join(",", line.Select(data => data.ToString())));
            }

            return stringBuilder.ToString();
        }

        private byte[] ConvertToByte(XLWorkbook workbook)
        {
            var stream = new MemoryStream();
            workbook.SaveAs(stream);

            var content = stream.ToArray();
            return content;
        }

        public byte[] CreateFullExport(List<Data> DataList, List<List<object>> GeneratedData)
        {
            var workbook = new XLWorkbook();
            workbook.Properties.Title = "GeneratedData";
            workbook.Properties.Author = "bozya02";
            workbook.Properties.Subject = "Full Export";

            CreateAuthorWorksheet(workbook, DataList, GeneratedData);

            return ConvertToByte(workbook);
        }

        public void CreateAuthorWorksheet(XLWorkbook package, List<Data> DataList, List<List<object>> GeneratedData)
        {
            var worksheet = package.Worksheets.Add("Export");

            for (int i = 0; i < DataList.Count(); i++)
            {
                worksheet.Cell(1, i + 1).Value = DataList[i].FieldName;
            }

            for (int row = 0; row < GeneratedData.Count(); row++)
            {
                for (int column = 0; column < GeneratedData[row].Count(); column++)
                {
                    worksheet.Cell(row + 1, column + 1).Value = GeneratedData[row][column].ToString();
                }
            }
        }
    }
}
