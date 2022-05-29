using ClosedXML.Excel;
using Core;
using Microsoft.JSInterop;
using System.Text;

namespace GeneratorWeb.Service
{
    public class ExportService
    {
        public byte[] Export(string type, List<Data> DataList, List<List<object>> GeneratedData)
        {
            byte[] fileBytes;

            switch (type)
            {
                case "csv":
                    fileBytes = CreateFullExportCSV(DataList, GeneratedData);
                    break;
                case "xlsx":
                    fileBytes = CreateFullExportExcel(DataList, GeneratedData);
                    break;
                case "json":
                    fileBytes = CreateFullExportJSON(DataList, GeneratedData);
                    break;
                default:
                    fileBytes = new byte[0];
                    break;
            }

            return fileBytes;
        }

        private byte[] ConvertToByte(string content)
        {
            return Encoding.ASCII.GetBytes(content);
        }

        private byte[] ConvertToByte(XLWorkbook workbook)
        {
            var stream = new MemoryStream();
            workbook.SaveAs(stream);

            var content = stream.ToArray();
            return content;
        }

        public byte[] CreateFullExportCSV(List<Data> DataList, List<List<object>> GeneratedData)
        {
            var csv = CreateCSV(DataList, GeneratedData);

            return ConvertToByte(csv);
        }

        private string CreateCSV(List<Data> DataList, List<List<object>> GeneratedData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Join(",", DataList.Select(data => data.FieldName)));

            foreach (var line in GeneratedData)
            {
                stringBuilder.AppendLine(string.Join(",", line.Select(data => data.ToString())));
            }

            return stringBuilder.ToString();
        }

        public byte[] CreateFullExportExcel(List<Data> DataList, List<List<object>> GeneratedData)
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
                    worksheet.Cell(row + 2, column + 1).Value = GeneratedData[row][column].ToString();
                }
            }
        }

        public string CreateJSON(List<Data> DataList, List<List<object>> GeneratedData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[");

            foreach (var data in GeneratedData)
            {
                stringBuilder.AppendLine("{");

                stringBuilder.AppendLine(string.Join(",\n",
                    Enumerable.Range(0, data.Count()).Select(x => $"\"{DataList[x].FieldName}\" : \"{data[x]}\"")));

                stringBuilder.AppendLine(data != GeneratedData[^1] ? "}," : "}");
            }

            stringBuilder.AppendLine("]");
            return stringBuilder.ToString();
        }

        public byte[] CreateFullExportJSON(List<Data> DataList, List<List<object>> GeneratedData)
        {
            var json = CreateJSON(DataList, GeneratedData);

            return ConvertToByte(json);
        }
    }
}
