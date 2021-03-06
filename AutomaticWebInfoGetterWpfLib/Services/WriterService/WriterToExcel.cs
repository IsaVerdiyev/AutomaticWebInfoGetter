﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomaticWebInfoGetterWpfLib.Models;
using OfficeOpenXml;
using OpenQA.Selenium.Remote;

namespace AutomaticWebInfoGetterWpfLib.Services.WriterService
{
    class WriterToExcel : IWriter
    {
        string writingPath;

        public WriterToExcel(string writingPath = null)
        {
            this.writingPath = writingPath;
        }

        public void WriteToExcel(List<string> infos, SettingsInfo settingsInfo, DownloadedPartOfPageSettingInfo downloadedPart)
        {
            if (writingPath != null)
            {
                Directory.CreateDirectory(writingPath);
            }
            var file = new FileInfo($"{writingPath}{settingsInfo.NameOfFileToWriteInfo}");
            using (var excelPackage = new ExcelPackage(file))
            {

                if (file.Exists)
                {
                    excelPackage.File.Attributes = FileAttributes.Normal;
                }

                ExcelWorksheet workSheet;
                InitializeWorkSheet(excelPackage, out workSheet);
                InstallCurrentWritingPositionIfIsNull(downloadedPart, settingsInfo, workSheet);
                for (int i = 0; i < infos.Count(); i++)
                {
                    workSheet.Cells[downloadedPart.CurrentWritingPosition.Row, downloadedPart.CurrentWritingPosition.Column].Value = infos[i];
                    if (settingsInfo.HorizontalOrientationOfWritingInfo)
                    {
                        downloadedPart.CurrentWritingPosition.Column += settingsInfo.BetweenLineDistance;
                    }
                    else
                    {
                        downloadedPart.CurrentWritingPosition.Row += settingsInfo.BetweenLineDistance;
                    }
                }
                excelPackage.Save();
                excelPackage.File.Attributes = FileAttributes.ReadOnly;
            }

        }
        public void WriteToExcel(string info, SettingsInfo settingsInfo, DownloadedPartOfPageSettingInfo downloadedPart)
        {
            if (writingPath != null)
            {
                Directory.CreateDirectory(writingPath);
            }
            var file = new FileInfo($"{writingPath}{settingsInfo.NameOfFileToWriteInfo}");
            using (var excelPackage = new ExcelPackage(file))
            {
                if (file.Exists)
                {
                    excelPackage.File.Attributes = FileAttributes.Normal;
                }

                ExcelWorksheet workSheet;
                InitializeWorkSheet(excelPackage, out workSheet);
                InstallCurrentWritingPositionIfIsNull(downloadedPart, settingsInfo, workSheet);
                workSheet.Cells[downloadedPart.CurrentWritingPosition.Row, downloadedPart.CurrentWritingPosition.Column].Value = info;
                if (settingsInfo.HorizontalOrientationOfWritingInfo)
                {
                    downloadedPart.CurrentWritingPosition.Column += settingsInfo.BetweenLineDistance;
                }
                else
                {
                    downloadedPart.CurrentWritingPosition.Row += settingsInfo.BetweenLineDistance;
                }
                excelPackage.Save();
                excelPackage.File.Attributes = FileAttributes.ReadOnly;
            }

        }
        private void InstallCurrentWritingPositionIfIsNull(DownloadedPartOfPageSettingInfo downloadedPartInfo, SettingsInfo settingsInfo, ExcelWorksheet workSheet)
        {
            if (downloadedPartInfo.CurrentWritingPosition == null)
            {
                if (!string.IsNullOrWhiteSpace(downloadedPartInfo.Header))
                {
                    workSheet.Cells[downloadedPartInfo.StartPositionOfWriting.Row, downloadedPartInfo.StartPositionOfWriting.Column].Value = downloadedPartInfo.Header;
                    if (settingsInfo.HorizontalOrientationOfWritingInfo)
                    {
                        downloadedPartInfo.CurrentWritingPosition = new Position
                        {
                            Row = downloadedPartInfo.StartPositionOfWriting.Row,
                            Column = downloadedPartInfo.StartPositionOfWriting.Column + settingsInfo.BetweenLineDistance
                        };
                    }
                    else
                    {
                        downloadedPartInfo.CurrentWritingPosition = new Position
                        {
                            Row = downloadedPartInfo.StartPositionOfWriting.Row + settingsInfo.BetweenLineDistance,
                            Column = downloadedPartInfo.StartPositionOfWriting.Column
                        };
                    }
                    workSheet.Cells[downloadedPartInfo.StartPositionOfWriting.Row, downloadedPartInfo.StartPositionOfWriting.Column].Style.Font.Color.SetColor(Color.Blue);
                }
                else
                {
                    downloadedPartInfo.CurrentWritingPosition = (Position)downloadedPartInfo.StartPositionOfWriting.Clone();
                }
            }
            else
            {
                if (settingsInfo.HorizontalOrientationOfWritingInfo)
                {
                    downloadedPartInfo.CurrentWritingPosition.Column += settingsInfo.BetweenWritingNewInfoDistance;
                }
                else
                {
                    downloadedPartInfo.CurrentWritingPosition.Row += settingsInfo.BetweenWritingNewInfoDistance;
                }
            }
        }
        private void InitializeWorkSheet(ExcelPackage excelPackage, out ExcelWorksheet worksheet)
        {
            if (excelPackage.Workbook.Worksheets.Count == 0)
            {
                worksheet = excelPackage.Workbook.Worksheets.Add("Worksheet 1");
            }
            else
            {
                worksheet = excelPackage.Workbook.Worksheets.First();
            }
        }
    }
}