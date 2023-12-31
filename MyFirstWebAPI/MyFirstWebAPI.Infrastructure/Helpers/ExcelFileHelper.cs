﻿using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFirstWebAPI.Infrastructure.Helpers
{
    public class ExcelFileHelper
    {

        /// <summary>
        /// Converts a given XLS into CSV file format.
        /// </summary>
        public static bool SaveAsCsv(string excelFilePath, string destinationCsvFilePath)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IExcelDataReader reader = null;
                if (excelFilePath.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (excelFilePath.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                if (reader == null)
                    return false;

                var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = false
                    }
                });

                var csvContent = string.Empty;
                int row_no = 0;
                while (row_no < ds.Tables[0].Rows.Count)
                {
                    var arr = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        var data = ds.Tables[0].Rows[row_no][i].ToString();
                        arr.Add(data.Replace(",", ""));
                    }
                    row_no++;
                    csvContent += string.Join(",", arr) + "\n";
                }
                StreamWriter csv = new StreamWriter(destinationCsvFilePath, false);
                csv.Write(csvContent);
                csv.Close();
                return true;
            }
        }

        /// <summary>
        /// Converts a given XLS into CSV file format.
        /// </summary>
        public static bool SaveExcelContentAsCsv(string csvContent, string destinationCsvFilePath)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            StreamWriter csv = new StreamWriter(destinationCsvFilePath, false);
            csv.Write(csvContent);
            csv.Close();
            csv.Dispose();
            return true;
        }
    }
}
