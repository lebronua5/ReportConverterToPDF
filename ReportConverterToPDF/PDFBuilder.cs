using iTextSharp.text;
using iTextSharp.text.pdf;
using ReportConverterToPDF.Helpers;
using ReportConverterToPDF.Helpers.PDF_Tests;
using ReportConverterToPDF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PdfDocument = iTextSharp.text.Document;

namespace ReportConverterToPDF
{
    public class PDFBuilder
    {       
        public string CreateReportInPDF(Model.ReportDetails report)
        {
            using (var ms = new MemoryStream())
            {
                var pdfDoc = new PdfDocument(PageSize.LETTER, 40f, 40f, 30f, 30f);
                var writer = PdfWriter.GetInstance(pdfDoc, ms);
                var PageEventHandler = new TwoColumnHeaderFooter();
                writer.PageEvent = PageEventHandler;

                pdfDoc.Open();

                CreateHeader(report, pdfDoc);
                CreateMainInfo(report, pdfDoc);
                CreateTireTables(report, pdfDoc);
                CreateLiquidsTable(report, pdfDoc);
                CreateEquipmentTables(report, pdfDoc);
                CreateCarKit(report, pdfDoc);
                CreatePhotoList(report, pdfDoc);
                CreateFinalSignaturesTable(report, pdfDoc);

                pdfDoc.Close();

                var result = ms.ToArray();
                return Convert.ToBase64String(result, 0, result.Length);                             
            }           
        }

        private void CreateHeader(Model.ReportDetails report, PdfDocument pdfDoc)
        {
            var parHeader1 = new Paragraph();
            parHeader1.IndentationLeft = 280f;
            var chankHeader1 = new Chunk("«Затверджую»\n", FontsTextSharp.BoldTimesFont16);
            parHeader1.Alignment = Element.ALIGN_CENTER;
            parHeader1.Add(chankHeader1);
            pdfDoc.Add(parHeader1);

            var parHeader2 = new Paragraph();

            parHeader2.IndentationLeft = 280f;
            var chankHeader2 = new Chunk("Заступник операційного директору з транспорту", FontsTextSharp.NormalTimesFont12);
            parHeader2.Add(chankHeader2);
            pdfDoc.Add(parHeader2);

            var parHeader3 = new Paragraph();
            parHeader3.IndentationLeft = 280f;
            parHeader3.Alignment = Element.ALIGN_CENTER;
            var chankHeader3 = new Chunk(string.Format("\"{0}\"", report.UrFace), FontsTextSharp.NormalTimesFont12);
            parHeader3.Add(chankHeader3);
            pdfDoc.Add(parHeader3);

            var parHeader4 = new Paragraph();
            parHeader4.IndentationLeft = 280f;
            var chankHeader4 = new Chunk("«___»__________20__ р. __________ ", FontsTextSharp.NormalTimesFont11);
            var chankHeader5 = new Chunk(report.Boss, FontsTextSharp.BoldTimesFont12);
            parHeader4.Add(chankHeader4);
            parHeader4.Add(chankHeader5);
            pdfDoc.Add(parHeader4);

            var parTitle = new Paragraph();
            parTitle.Alignment = Element.ALIGN_CENTER;
            parTitle.SpacingBefore = 5f;
            var chankTitle1 = new Chunk($"Акт № {report.IdVCMH}\n", FontsTextSharp.BoldTimesFont18);
            var chankTitle2 = new Chunk("прийому-передачі автомобіля", FontsTextSharp.BoldTimesFont14);
            parTitle.Add(chankTitle1);
            parTitle.Add(chankTitle2);
            pdfDoc.Add(parTitle);
        }

        private void CreateMainInfo(Model.ReportDetails report, PdfDocument pdfDoc)
        {
            var parDriverInfo1 = new Paragraph();

            parDriverInfo1.Alignment = Element.ALIGN_JUSTIFIED;
            parDriverInfo1.SpacingBefore = 20f;
            var chankDriverInfo1 = new Chunk("Водій ", FontsTextSharp.BoldTimesFont11);
            parDriverInfo1.Add(chankDriverInfo1);
            var chankDriverInfo2 = new Chunk($"  {report.DriverFIO}  ", FontsTextSharp.NormalTimesFont12);
            chankDriverInfo2.SetUnderline(0, -2);
            parDriverInfo1.Add(chankDriverInfo2);
            var chankDriverInfo3 = new Chunk(" передав, а водій ", FontsTextSharp.BoldTimesFont11);
            parDriverInfo1.Add(chankDriverInfo3);
            var chankDriverInfo4 = new Chunk($" {report.DriverAcceptFIO} ", FontsTextSharp.BoldTimesFont11);
            chankDriverInfo4.SetUnderline(0, -2);
            parDriverInfo1.Add(chankDriverInfo4);
            var chankDriverInfo14 = new Chunk(" прийняв", FontsTextSharp.BoldTimesFont11);
            parDriverInfo1.Add(chankDriverInfo14);

            pdfDoc.Add(parDriverInfo1);

            var carDescriptionTable = new PdfPTable(new float[] { 2f, 1.8f, 1f, 2f });
            carDescriptionTable.WidthPercentage = 100f;

            var brandAutoNameDescriptionCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var brandAutoNameDescription = new Paragraph($"автомобіль(автобус) марки", FontsTextSharp.BoldTimesFont11);
            brandAutoNameDescriptionCell.AddElement(brandAutoNameDescription);

            var brandAutoNameCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var brandAutoName = new Paragraph();
            var chankDriverInfo6 = new Chunk($"{report.MarkaName}   ", FontsTextSharp.BoldTimesFont11);
            chankDriverInfo6.SetUnderline(0, -2);
            brandAutoName.Add(chankDriverInfo6);
            brandAutoNameCell.AddElement(brandAutoName);

            var NumberGosRegDescriptionCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var NumberGosRegDescription = new Paragraph("держ. №", FontsTextSharp.BoldTimesFont11);
            NumberGosRegDescriptionCell.AddElement(NumberGosRegDescription);

            var NumberGosRegCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var NumberGosReg = new Paragraph();
            var chankDriverInfo8 = new Chunk(report.NumberGosReg, FontsTextSharp.BoldTimesFont11);
            chankDriverInfo8.SetUnderline(0, -2);
            NumberGosReg.Add(chankDriverInfo8);
            NumberGosRegCell.AddElement(NumberGosReg);


            var trailerBrandAutoNameDescriptionCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var trailerbrandAutoNameDescription = new Paragraph($"напівпричеп", FontsTextSharp.BoldTimesFont11);
            trailerBrandAutoNameDescriptionCell.AddElement(trailerbrandAutoNameDescription);

            var trailerBrandAutoNameCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var trailerBrandAutoName = new Paragraph();

            trailerBrandAutoNameCell.AddElement(trailerBrandAutoName);

            var trailerNumGosRegCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var trailerNumGosReg = new Paragraph();
            var chankDriverInfo10 = new Chunk($"{report.TrailerNumGosReg} ", FontsTextSharp.BoldTimesFont11);
            chankDriverInfo10.SetUnderline(0, -2);
            trailerNumGosReg.Add(chankDriverInfo10);
            trailerNumGosRegCell.AddElement(trailerNumGosReg);

            carDescriptionTable.AddCell(brandAutoNameDescriptionCell);
            carDescriptionTable.AddCell(brandAutoNameCell);
            carDescriptionTable.AddCell(NumberGosRegDescriptionCell);
            carDescriptionTable.AddCell(NumberGosRegCell);

            carDescriptionTable.AddCell(trailerBrandAutoNameDescriptionCell);
            carDescriptionTable.AddCell(trailerBrandAutoNameCell);
            carDescriptionTable.AddCell(NumberGosRegDescriptionCell);
            carDescriptionTable.AddCell(trailerNumGosRegCell);

            pdfDoc.Add(carDescriptionTable);

            pdfDoc.Add(new Paragraph("На момент прийому–передачі встановлено наступне: ", FontsTextSharp.BoldTimesFont12));
        }

        private void CreateTireTables(Model.ReportDetails report, PdfDocument pdfDoc)
        {
            var tireCondition = new Paragraph("Тех. стан  шин:", FontsTextSharp.ItalicTimesFont12);
            tireCondition.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(tireCondition);

            if (!ReferenceEquals(report.CarTires, null))
            {
                var tiresList = report.CarTires.Tire;
                var haveTires = tiresList.Count != 0;

                if (haveTires)
                {
                    var parTireCount = new Paragraph("Шини тягач (кількість) ", FontsTextSharp.NormalTimesFont12);
                    var chTireCount = new Chunk($"  {tiresList.Count}  ", FontsTextSharp.NormalTimesFont12);
                    chTireCount.SetUnderline(0, -2);
                    parTireCount.Add(chTireCount);
                    pdfDoc.Add(parTireCount);
                    pdfDoc.Add(CreateTireTable(tiresList));
                }
            }

            if (!ReferenceEquals(report.TrailerTires, null))
            {
                var tiresTrailerList = report.TrailerTires.Tire;
                var haveTrailerTires = tiresTrailerList.Count != 0;
                var parTireTrailerCount = new Paragraph("Шини н/причеп (кількість) ", FontsTextSharp.NormalTimesFont12);

                if (haveTrailerTires)
                {
                    var chTireTrailerCount = new Chunk($"  {tiresTrailerList.Count}  ", FontsTextSharp.NormalTimesFont12);
                    chTireTrailerCount.SetUnderline(0, -2);
                    parTireTrailerCount.Add(chTireTrailerCount);
                    pdfDoc.Add(parTireTrailerCount);

                    pdfDoc.Add(CreateTireTable(tiresTrailerList));
                }
            }
        }

        private void CreateEquipmentTables(Model.ReportDetails report, PdfDocument pdfDoc)
        {
            if (!ReferenceEquals(report.Car_SpecEqipments, null) && !ReferenceEquals(report.Car_SpecEqipments, string.Empty))
            {
                var spEquipmentList = report.Car_SpecEqipments.Car_SpecEqipment;              

                if (spEquipmentList.Count != 0)
                    СreateEquipmenTableForCar("Спец. обладнання авто:", spEquipmentList, pdfDoc);
            }

            if (!ReferenceEquals(report.Trailer_SpecEqipments, null) && !ReferenceEquals(report.Trailer_SpecEqipments, string.Empty))
            {
                var spEquipmentList = report.Trailer_SpecEqipments.Trailer_SpecEqipment;

                if (spEquipmentList.Count != 0)
                    СreateEquipmenTableForTrailer("Спец. обладнання прицеп:", spEquipmentList, pdfDoc);
            }
        }

        private void CreateLiquidsTable(Model.ReportDetails report, PdfDocument pdfDoc)
        {            
            var prLiquidInfoLabel = new Paragraph("Тех.рідини авто", FontsTextSharp.ItalicTimesFont12);
            prLiquidInfoLabel.Alignment = Element.ALIGN_CENTER;
            prLiquidInfoLabel.SpacingAfter = 10f;
            pdfDoc.Add(prLiquidInfoLabel);

            var carFuelList = report?.CarFuels?.CarFuel;
            var fuelAdBluInfo = carFuelList?.FirstOrDefault(n => n.FuelName == "AdBlue");
            carFuelList.Remove(fuelAdBluInfo);
            var fuelCarInfo = carFuelList?.FirstOrDefault();
            var fuelTrailerInfo = report?.TrailerFuels?.TrailerFuel;

            var table = new PdfPTable(new float[] {  2.5f,1f,1f,1f,1f });
            table.WidthPercentage = 50f;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            var cellLiquid1 = new PdfPCell() { Border = Rectangle.NO_BORDER }; 
            cellLiquid1.AddElement(new Paragraph("Залишок пального", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_LEFT });
            table.AddCell(cellLiquid1);

            var cellLiquid2 = new PdfPCell() { Border = Rectangle.NO_BORDER }; 
            var parLiquid1 = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            parLiquid1.SetLeading(1f, 1f);
          
            var chankFuelCar1 = new Chunk($"  {fuelCarInfo?.FuelName}   \n", FontsTextSharp.NormalTimesFont12);
            chankFuelCar1.SetUnderline(0, -2);
            parLiquid1.Add(chankFuelCar1);

            var chankFuelCar2 = new Chunk("(марка)", FontsTextSharp.NormalTimesFont8);           
            parLiquid1.Add(chankFuelCar2);
            cellLiquid2.AddElement(parLiquid1);
            table.AddCell(cellLiquid2);

            var cellLiquid3 = new PdfPCell() { Border = Rectangle.NO_BORDER }; 
            var parLiquidLiters1 = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            parLiquidLiters1.SetLeading(1f, 1f);

            var chankFuelCar3 = new Chunk($"{fuelCarInfo?.RestFuel}\n", FontsTextSharp.NormalTimesFont12);
            chankFuelCar3.SetUnderline(0, -2);
            parLiquidLiters1.Add(chankFuelCar3);

            var chLittersLabel = new Chunk("(в шл.л)", FontsTextSharp.NormalTimesFont8);
            var parLittersLabel = new Paragraph("літрів, ", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_LEFT };
            var chFactLabel = new Chunk("(по факту)", FontsTextSharp.NormalTimesFont8);
            parLittersLabel.SetLeading(1f, 1f);

            parLiquidLiters1.Add(chLittersLabel);
            cellLiquid3.AddElement(parLiquidLiters1);
            table.AddCell(cellLiquid3);

            var cellLiquid4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var parLiquidLiters2 = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            parLiquidLiters2.SetLeading(1f, 1f);
            
            var chankFuelCarRealFuel = new Chunk($"{fuelCarInfo?.RestFuelFact}\n", FontsTextSharp.NormalTimesFont12);
            chankFuelCarRealFuel.SetUnderline(0, -2);
            parLiquidLiters2.Add(chankFuelCarRealFuel);
            parLiquidLiters2.Add(chFactLabel);
            cellLiquid4.AddElement(parLiquidLiters2);
            table.AddCell(cellLiquid4);

            var cellLiquid5 = new PdfPCell() { Border = Rectangle.NO_BORDER };           
            cellLiquid5.AddElement(parLittersLabel);
            table.AddCell(cellLiquid5);

            // Прицеп
            var cellLiquidTr1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellLiquidTr1.AddElement(new Paragraph("РЕФ", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_LEFT });
            table.AddCell(cellLiquidTr1);

            var cellLiquidTr2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var parLiquidTr1 = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            parLiquidTr1.SetLeading(1f, 1f);

            var chankFuelCarTr1 = new Chunk($"{fuelTrailerInfo?.FuelName}   \n", FontsTextSharp.NormalTimesFont12);
            chankFuelCarTr1.SetUnderline(0, -2);
            parLiquidTr1.Add(chankFuelCarTr1);

            var chankFuelCarTr2 = new Chunk("(марка)", FontsTextSharp.NormalTimesFont8);
            parLiquidTr1.Add(chankFuelCarTr2);
            cellLiquidTr2.AddElement(parLiquidTr1);
            table.AddCell(cellLiquidTr2);

            var cellLiquidTr3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var parLiquidLitersTr1 = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            parLiquidLitersTr1.SetLeading(1f, 1f);

            var restFuel = !ReferenceEquals(fuelTrailerInfo?.RestFuel, null) && !ReferenceEquals(fuelTrailerInfo?.RestFuel, string.Empty) ? fuelTrailerInfo?.RestFuel : "______";

            var chankFuelCarTr3 = new Chunk($"{restFuel}\n", FontsTextSharp.NormalTimesFont12);
            chankFuelCarTr3.SetUnderline(0, -2);
            parLiquidLitersTr1.Add(chankFuelCarTr3);

            parLiquidLitersTr1.Add(chLittersLabel);
            cellLiquidTr3.AddElement(parLiquidLitersTr1);
            table.AddCell(cellLiquidTr3);

            var cellLiquidTr4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var parLiquidLitersTr2 = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            parLiquidLitersTr2.SetLeading(1f, 1f);          

            var restFuelFact = !ReferenceEquals(fuelTrailerInfo?.RestFuelFact, null) && !ReferenceEquals(fuelTrailerInfo?.RestFuelFact, string.Empty) ? fuelTrailerInfo?.RestFuelFact : "______";

            var chankFuelCarRealFuelTr = new Chunk($"{restFuelFact}\n", FontsTextSharp.NormalTimesFont12);
            chankFuelCarRealFuelTr.SetUnderline(0, -2);
            parLiquidLitersTr2.Add(chankFuelCarRealFuelTr);
            parLiquidLitersTr2.Add(chFactLabel);
            cellLiquidTr4.AddElement(parLiquidLitersTr2);
            table.AddCell(cellLiquidTr4);

            var cellLiquidTr5 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellLiquidTr5.AddElement(parLittersLabel);
            table.AddCell(cellLiquidTr5);

            // ADBLUE 
         
            var cellLiquidAd1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellLiquidAd1.AddElement(new Paragraph("Залишок ADBLUE", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_LEFT });
            table.AddCell(cellLiquidAd1);

            var cellLiquidAd2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var parLiquidAd1 = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            parLiquidAd1.SetLeading(1f, 1f);

            var adBlueRestFuel = !ReferenceEquals(fuelAdBluInfo?.RestFuel, null) && !ReferenceEquals(fuelAdBluInfo?.RestFuel, string.Empty) ? fuelAdBluInfo.RestFuel : "______";

            var chankFuelCarAd1 = new Chunk($"{adBlueRestFuel}\n", FontsTextSharp.NormalTimesFont12);
            chankFuelCarAd1.SetUnderline(0, -2);
            parLiquidAd1.Add(chankFuelCarAd1);
            
            parLiquidAd1.Add(chLittersLabel);
            cellLiquidAd2.AddElement(parLiquidAd1);
            table.AddCell(cellLiquidAd2);

            var cellLiquidAd3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var parLiquidLitersAd1 = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            parLiquidLitersAd1.SetLeading(1f, 1f);

            var adBlueRestFuelFact = !ReferenceEquals(fuelAdBluInfo?.RestFuelFact, null) && !ReferenceEquals(fuelAdBluInfo?.RestFuelFact, string.Empty) ? fuelAdBluInfo?.RestFuel : "______";

            var chankFuelCarAd3 = new Chunk($"{adBlueRestFuelFact}\n", FontsTextSharp.NormalTimesFont12);
            chankFuelCarAd3.SetUnderline(0, -2);
            parLiquidLitersAd1.Add(chankFuelCarAd3);

            parLiquidLitersAd1.Add(chFactLabel);
            cellLiquidAd3.AddElement(parLiquidLitersAd1);
            table.AddCell(cellLiquidAd3);          

            var cellLiquidAd5 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            cellLiquidAd5.Colspan = 2;
            cellLiquidAd5.AddElement(new Paragraph("літрів.", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_LEFT });
            table.AddCell(cellLiquidAd5);

            pdfDoc.Add(table);

            if (!ReferenceEquals(report.Ranges, null))
            {
                var fluidsList = report.Ranges.Range;

                var prRangeOil = new Paragraph();
                var oilInfo = fluidsList.OilRange;
                prRangeOil.Add(new Chunk("Рівень масла: ", FontsTextSharp.NormalTimesFont12));
                prRangeOil.Add(new Chunk(oilInfo == string.Empty ? "Ні" : "Так", FontsTextSharp.NormalTimesFont12));
                pdfDoc.Add(prRangeOil);

                var prCoolingLiquid = new Paragraph();
                var coolingLiquidInfo = fluidsList.Antifreeze;
                prCoolingLiquid.Add(new Chunk("Охолоджуюча рідина: ", FontsTextSharp.NormalTimesFont12));
                prCoolingLiquid.Add(new Chunk(coolingLiquidInfo == string.Empty ? "Ні" : "Так", FontsTextSharp.NormalTimesFont12));
                pdfDoc.Add(prCoolingLiquid);

                var prWindowWasher = new Paragraph();
                var windowWasherInfo = fluidsList.WindowWasher;
                prWindowWasher.Add(new Chunk("Омивач: ", FontsTextSharp.NormalTimesFont12));
                prWindowWasher.Add(new Chunk(windowWasherInfo == string.Empty ? "Ні" : "Так", FontsTextSharp.NormalTimesFont12));
                pdfDoc.Add(prWindowWasher);
            }
        }

        private void CreateCarKit(Model.ReportDetails report, PdfDocument pdfDoc)
        {
            var carkitDocList = report.ComplectDocs.Document;
            var carKitEquipmentList = report.ComplectTechnic.Technic;

            var prSpecialistEquipment = new Paragraph("Комплектація автомобіля:", FontsTextSharp.ItalicTimesFont12);
            prSpecialistEquipment.Alignment = Element.ALIGN_CENTER;
            prSpecialistEquipment.SpacingAfter = 10f;
            pdfDoc.Add(prSpecialistEquipment);        

            var carKitDocCount = carkitDocList.Count;
            var carKitEquipmentCount = carKitEquipmentList.Count;

            var rowsCount = carKitDocCount >= carKitEquipmentCount ? carKitDocCount : carKitEquipmentCount;

            var table = new PdfPTable(new float[] { 2.5f, 10f, 10f, 2.5f, 10f, 10f });
            table.WidthPercentage = 100f;

            var prNumber = new Paragraph("№ п/п", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER };
            var prNameEquipment = new Paragraph("Найменування", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER };
            var prState = new Paragraph("Наявність", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER };

            var cellEq1 = new PdfPCell();           
            cellEq1.AddElement(prNumber);
            table.AddCell(cellEq1);

            var cellEq2 = new PdfPCell();           
            cellEq2.AddElement(prNameEquipment);
            table.AddCell(cellEq2);

            var cellEq3 = new PdfPCell();            
            cellEq3.AddElement(prState);
            table.AddCell(cellEq3);

            var cellEq4 = new PdfPCell();
            cellEq4.AddElement(prNumber);
            table.AddCell(cellEq4);

            var cellEq5 = new PdfPCell();
            cellEq5.AddElement(prNameEquipment);
            table.AddCell(cellEq5);

            var cellEq6 = new PdfPCell();
            cellEq6.AddElement(prState);
            table.AddCell(cellEq6);           

            for (var i = 0; i < rowsCount; i++)
            {
                var cellEqVal1 = new PdfPCell();
                cellEqVal1.AddElement(new Paragraph((i + 1).ToString(), FontsTextSharp.BoldTimesFont10) { Alignment = Element.ALIGN_CENTER });
                table.AddCell(cellEqVal1);

                // for Documents
                var cellEqVal2 = new PdfPCell();
                var cellEqVal3 = new PdfPCell();
                                
                if (carKitDocCount >= i + 1)
                {
                    cellEqVal2.AddElement(new Paragraph(carkitDocList[i].Name, FontsTextSharp.BoldTimesFont10) { Alignment = Element.ALIGN_CENTER });
                    table.AddCell(cellEqVal2);
                    var isChecked = carkitDocList[i].Checked == "1";

                    cellEqVal3.AddElement(new Paragraph(isChecked ? "Так" : "Ні", FontsTextSharp.BoldTimesFont10) { Alignment = Element.ALIGN_CENTER });

                    if (!isChecked)
                    {
                        cellEqVal3.AddElement(new Paragraph($"({carkitDocList[i].Comment})", FontsTextSharp.NormalTimesFont9) { Alignment = Element.ALIGN_CENTER });
                    }

                    table.AddCell(cellEqVal3);
                }
                else
                {
                    cellEqVal2.AddElement(new Paragraph());
                    cellEqVal3.AddElement(new Paragraph());
                    table.AddCell(cellEqVal2);
                    table.AddCell(cellEqVal3);
                }

                // for Equipment
                var cellEqVal4 = new PdfPCell();
                cellEqVal4.AddElement(new Paragraph((i + 1).ToString(), FontsTextSharp.BoldTimesFont10) { Alignment = Element.ALIGN_CENTER });
                table.AddCell(cellEqVal4);

                var cellEqVal5 = new PdfPCell();
                var cellEqVal6 = new PdfPCell();

                if (carKitEquipmentCount >= i + 1)
                {
                    cellEqVal5.AddElement(new Paragraph(carKitEquipmentList[i].Name, FontsTextSharp.BoldTimesFont10) { Alignment = Element.ALIGN_CENTER });
                    table.AddCell(cellEqVal5);
                    var isChecked = carKitEquipmentList[i].Checked == "1";
                    cellEqVal6.AddElement(new Paragraph(isChecked ? "Так" : "Ні", FontsTextSharp.BoldTimesFont10) { Alignment = Element.ALIGN_CENTER });

                    if (!isChecked)
                    {
                        cellEqVal3.AddElement(new Paragraph($"({carKitEquipmentList[i].Comment})", FontsTextSharp.NormalTimesFont9) { Alignment = Element.ALIGN_CENTER });
                    }

                    table.AddCell(cellEqVal6);
                }
                else
                {
                    cellEqVal5.AddElement(new Paragraph());
                    cellEqVal6.AddElement(new Paragraph());
                    table.AddCell(cellEqVal5);
                    table.AddCell(cellEqVal6);
                }
            }          

            pdfDoc.Add(table);
        }      

        private void СreateEquipmenTableForCar(string equipmentName, List<Model.Car_SpecEqipment> equipmentList, PdfDocument pdfDoc)
        {
            var prSpecialistEquipment = new Paragraph(equipmentName, FontsTextSharp.ItalicTimesFont12);
            prSpecialistEquipment.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(prSpecialistEquipment);

            var spEquipmentTable = new PdfPTable(new float[] { 1f, 10f, 10f });
            spEquipmentTable.SpacingBefore = 10f;
            spEquipmentTable.WidthPercentage = 100f;

            var cellEq1 = new PdfPCell();
            cellEq1.AddElement(new Paragraph("№ п/п", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER});
            spEquipmentTable.AddCell(cellEq1);

            var cellEq2 = new PdfPCell();
            cellEq2.AddElement(new Paragraph("Найменування", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER});
            spEquipmentTable.AddCell(cellEq2);

            var cellEq3 = new PdfPCell();
            cellEq3.AddElement(new Paragraph("Стан", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER});
            spEquipmentTable.AddCell(cellEq3);


            for (var i = 0; i < equipmentList.Count; i++)
            {
                var cellEqVal1 = new PdfPCell();
                cellEqVal1.AddElement(new Paragraph((i + 1).ToString(), FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });               
                spEquipmentTable.AddCell(cellEqVal1);

                var cellEqVal2 = new PdfPCell();
                cellEqVal2.AddElement(new Paragraph(equipmentList[i].SpecEquipment.ToString(), FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });
                spEquipmentTable.AddCell(cellEqVal2);

                var cellEqVal3 = new PdfPCell();
                cellEqVal3.AddElement(new Paragraph(equipmentList[i].StateEq.ToString(), FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });
                spEquipmentTable.AddCell(cellEqVal3);
            }

            pdfDoc.Add(spEquipmentTable);
        }

        private void СreateEquipmenTableForTrailer(string equipmentName, List<Model.Trailer_SpecEqipment> equipmentList, PdfDocument pdfDoc)
        {
            var prSpecialistEquipment = new Paragraph(equipmentName, FontsTextSharp.ItalicTimesFont12);
            prSpecialistEquipment.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(prSpecialistEquipment);

            var spEquipmentTable = new PdfPTable(new float[] { 1f, 10f, 10f });
            spEquipmentTable.SpacingBefore = 10f;
            spEquipmentTable.WidthPercentage = 100f;

            var cellEq1 = new PdfPCell();
            cellEq1.AddElement(new Paragraph("№ п/п", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });
            spEquipmentTable.AddCell(cellEq1);

            var cellEq2 = new PdfPCell();
            cellEq2.AddElement(new Paragraph("Найменування", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });
            spEquipmentTable.AddCell(cellEq2);

            var cellEq3 = new PdfPCell();
            cellEq3.AddElement(new Paragraph("Стан", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });
            spEquipmentTable.AddCell(cellEq3);


            for (var i = 0; i < equipmentList.Count; i++)
            {
                var cellEqVal1 = new PdfPCell();
                cellEqVal1.AddElement(new Paragraph((i + 1).ToString(), FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });
                spEquipmentTable.AddCell(cellEqVal1);

                var cellEqVal2 = new PdfPCell();
                cellEqVal2.AddElement(new Paragraph(equipmentList[i].SpecEquipment.ToString(), FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });
                spEquipmentTable.AddCell(cellEqVal2);

                var cellEqVal3 = new PdfPCell();
                cellEqVal3.AddElement(new Paragraph(equipmentList[i].StateEq.ToString(), FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_CENTER });
                spEquipmentTable.AddCell(cellEqVal3);
            }

            pdfDoc.Add(spEquipmentTable);
        }

        private PdfPTable CreateTireTable(List<Model.Tire> tiresList)
        {
            var truckTires = new PdfPTable(new float[] { 10f, 10f, 10f, 10f, 10f, 10f });    
            truckTires.SpacingBefore = 10f;          
            truckTires.WidthPercentage = 100f;

            var cell1 = new PdfPCell();
            cell1.AddElement(new Paragraph("Марка шини", FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER});
            truckTires.AddCell(cell1);

            var cell2 = new PdfPCell();
            cell2.AddElement(new Paragraph("Серійний номер", FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER});
            truckTires.AddCell(cell2);

            var cell3 = new PdfPCell();
            cell3.AddElement(new Paragraph("Місце встановлення", FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER});
            truckTires.AddCell(cell3);

            var cell4 = new PdfPCell();
            cell4.AddElement(new Paragraph("Пробіг", FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER});
            truckTires.AddCell(cell4);

            var cell5 = new PdfPCell();
            cell5.AddElement(new Paragraph("Залишок \n протектора", FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER});
            truckTires.AddCell(cell5);

            var cell6 = new PdfPCell();
            cell6.AddElement(new Paragraph("Тиск", FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER});
            truckTires.AddCell(cell6);


            foreach (var tire in tiresList)
            {
                var tireModel = new PdfPCell();
                tireModel.AddElement(new Paragraph(tire.ModelName, FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER });
                truckTires.AddCell(tireModel);

                var tireSerial = new PdfPCell();
                tireSerial.AddElement(new Paragraph(tire.SerialNumber, FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER });
                truckTires.AddCell(tireSerial);

                var tirePlace = new PdfPCell();
                tirePlace.AddElement(new Paragraph(tire.PlaceName, FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER });
                truckTires.AddCell(tirePlace);

                var tireTotalKM = new PdfPCell();
                tireTotalKM.AddElement(new Paragraph(tire.TotalKM, FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER });
                truckTires.AddCell(tireTotalKM);

                var tireProtectorMM = new PdfPCell();

                //var protectorMM = !ReferenceEquals("Протектор ????", string.Empty) ? tire.protectorMM.ToString() : tire.state.ToString();
                tireProtectorMM.AddElement(new Paragraph(tire.ProtectorMM, FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER });
                truckTires.AddCell(tireProtectorMM);

                var tirePressure = new PdfPCell();
               // var pressure = !ReferenceEquals(tire.pressure.ToString(), string.Empty) ? tire.pressure.ToString() : string.Empty;
                tirePressure.AddElement(new Paragraph(tire.Pressure, FontsTextSharp.BoldTimesFont9) { Alignment = Element.ALIGN_CENTER });
                truckTires.AddCell(tirePressure);
            }

            return truckTires;
        }

        private void CreateFinalSignaturesTable(Model.ReportDetails report, PdfDocument pdfDoc)
        {
            pdfDoc.NewPage();
            pdfDoc.Add(new Paragraph($"Мені {report.DriverAcceptFIO} відомо, що прийнятий мною   автомобіль  (автобус) я  не маю право передати" +
                    "  іншій особі без дозволу мого прямого і безпосереднього керівника.Мені також відомо, що я несу   відповідальність за технічний стан і " +
                    "збереження прийнятого мною автомобіля(автобуса).", FontsTextSharp.BoldTimesFont10)
            { SpacingBefore = 15f });

            var table = new PdfPTable(new float[] { 2f, 1f, 3f });
            table.SpacingBefore = 10f;
            table.WidthPercentage = 100f;

            var autoFromCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var prAutoFrom = new Paragraph("Автомобіль Здав:", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_LEFT };
            prAutoFrom.SetLeading(1f, 1f);
            autoFromCell.AddElement(prAutoFrom);
            table.AddCell(autoFromCell);

            var autoFromSignCell1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var prAutoFromSign = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            prAutoFromSign.SetLeading(1f, 1f);
            var chankFIOInfoSign = new Chunk($"_____________\n", FontsTextSharp.NormalTimesFont12);
            prAutoFromSign.Add(chankFIOInfoSign);
            var chankSign = new Chunk("підпис", FontsTextSharp.NormalTimesFont9);
            prAutoFromSign.Add(chankSign);
            autoFromSignCell1.AddElement(prAutoFromSign);
            table.AddCell(autoFromSignCell1);

            var autoFromFIOCell1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var prAutoFromFIO = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            prAutoFromFIO.SetLeading(1f, 1f);
            var chankFIOInfo = new Chunk($"{report.DriverFIO}\n", FontsTextSharp.NormalTimesFont12);
            chankFIOInfo.SetUnderline(0, -2);
            prAutoFromFIO.Add(chankFIOInfo);
            var chankFIO = new Chunk("(П.І.Б водія)", FontsTextSharp.NormalTimesFont9);
            prAutoFromFIO.Add(chankFIO);
            autoFromFIOCell1.AddElement(prAutoFromFIO);
            table.AddCell(autoFromFIOCell1);

            var autoToCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var prAutoTo = new Paragraph("Автомобіль Прийняв: ", FontsTextSharp.NormalTimesFont12) { Alignment = Element.ALIGN_LEFT };
            prAutoTo.SetLeading(1f, 1f);
            autoToCell.AddElement(prAutoTo);
            table.AddCell(autoToCell);

            var autoToSignCell1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var prAutoToSign = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            prAutoToSign.SetLeading(1f, 1f);
            prAutoToSign.Add(chankFIOInfoSign);
            prAutoToSign.Add(chankSign);
            autoToSignCell1.AddElement(prAutoToSign);
            table.AddCell(autoToSignCell1);

            var autoToFIOCell1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var prAutoToFIO = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            prAutoToFIO.SetLeading(1f, 1f);
            var chankToFIOInfo = new Chunk($"{report.DriverAcceptFIO}\n", FontsTextSharp.NormalTimesFont12);
            chankToFIOInfo.SetUnderline(0, -2);
            prAutoToFIO.Add(chankToFIOInfo);           
            prAutoToFIO.Add(chankFIO);
            autoToFIOCell1.AddElement(prAutoToFIO);
            table.AddCell(autoToFIOCell1);

            var signSummeryCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            signSummeryCell.Colspan = 3;
            var prsignSummeryCell = new Paragraph($"Автомобіль Прийнято/Передано в присутності Начальника {report.Ak_name}", FontsTextSharp.BoldTimesFont11) { Alignment = Element.ALIGN_LEFT };
            signSummeryCell.AddElement(prsignSummeryCell);
            table.AddCell(signSummeryCell);


            table.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });          

            var endFIOCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var prEndFIO = new Paragraph() { Alignment = Element.ALIGN_CENTER };
            prEndFIO.SetLeading(1f, 1f);
            prEndFIO.Add(chankFIOInfoSign);
            prEndFIO.Add(chankSign);
            endFIOCell.AddElement(prEndFIO);
            table.AddCell(endFIOCell);

            var endSignCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
            var prEndSign= new Paragraph() { Alignment = Element.ALIGN_CENTER };
            prEndSign.SetLeading(1f, 1f);
            var chankEndSign = new Chunk($"{report.NK_FIO}\n", FontsTextSharp.NormalTimesFont12);
            chankEndSign.SetUnderline(0, -2);
            prEndSign.Add(chankEndSign);
            prEndSign.Add(chankFIO);
            endSignCell.AddElement(prAutoToFIO);
            table.AddCell(endSignCell);

            pdfDoc.Add(table);
        }

        private void CreatePhotoList(Model.ReportDetails report, PdfDocument pdfDoc)
        {
            if (!ReferenceEquals(report.Photos, null))
            {
                pdfDoc.NewPage();

                var photoHearder = new Paragraph("Візуальний стан авто", FontsTextSharp.ItalicTimesFont12);
                photoHearder.Alignment = Element.ALIGN_CENTER;
                photoHearder.SpacingAfter = 10f;
                pdfDoc.Add(photoHearder);

                var ImageOrientationTypes = GetImageOrientationTypes(report.Photos.Range);

                var verticalImagesCount = ImageOrientationTypes.VerticalImages.Count;
                var horizontalImagesCount = ImageOrientationTypes.HorizontalImages.Count;

                if (verticalImagesCount > 0)
                {
                    var verticalImageTable = CreateImageTable(ImageOrientationTypes.VerticalImages, new float[] { 1f, 1f }, 230f);

                    if (verticalImagesCount % 2 != 0)
                        verticalImageTable.AddCell(new PdfPCell { Border = Rectangle.NO_BORDER });

                    pdfDoc.Add(verticalImageTable);
                }

                if (horizontalImagesCount > 0)
                {
                    if (verticalImagesCount > 0)
                        pdfDoc.NewPage();

                    var horizontalImageTable = CreateImageTable(ImageOrientationTypes.HorizontalImages, new float[] { 1f }, 260f);
                    pdfDoc.Add(horizontalImageTable);
                }
            }
        }

        private PdfPTable CreateImageTable(List<TextSharpImageContent> imageList, float[] columns, float scale)
        {
            var table = new PdfPTable(columns);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.WidthPercentage = 100f;

            foreach (var verticalPhoto in imageList)
            {
                var parentPictureCell = new PdfPCell { Border = Rectangle.NO_BORDER };
                parentPictureCell.HorizontalAlignment = Element.ALIGN_CENTER;

                parentPictureCell.AddElement(CreateNewImageCell(verticalPhoto, scale));
                table.AddCell(parentPictureCell);
            }

            return table;
        }

        private PdfPTable CreateNewImageCell(TextSharpImageContent photo, float scale)
        {
            var innerTable = new PdfPTable(1);

            var childPictureCell = new PdfPCell { Border = Rectangle.NO_BORDER };
            var picture = photo.Image;
            picture.Alignment = Element.ALIGN_CENTER;        
            childPictureCell.AddElement(picture);            

            var parForPictureCell = new PdfPCell { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            var parForPicture = new Paragraph();
            parForPicture.Alignment = Element.ALIGN_CENTER;
            var commentInfo = !ReferenceEquals(photo.ImageContent.Comment, null) ? $"({photo.ImageContent.Comment})" : string.Empty;
            var chankPicture = new Chunk($"{photo.ImageContent.Comment} {commentInfo}", FontsTextSharp.NormalTimesFont11);
            parForPicture.Add(chankPicture);
            parForPictureCell.AddElement(parForPicture);

            innerTable.AddCell(childPictureCell);
            innerTable.AddCell(parForPictureCell);

            return innerTable;
        }

        private ImageOrientation GetImageOrientationTypes(List<Range> photos) 
        {
            var imageOrientation = new ImageOrientation();

            foreach (var photo in photos)
            {
                if (!ReferenceEquals(photo.URL_photo, null))
                {                        
                    var imageInstance = Image.GetInstance(photo.ImageContent);

                    imageInstance.ScaleAbsoluteWidth(230f);
                    imageOrientation.VerticalImages.Add(new TextSharpImageContent { Image = imageInstance, ImageContent = photo });


                    //MemoryStream ms = new MemoryStream(photo.ImageContent, 0, photo.ImageContent.Length);
                    //ms.Write(photo.ImageContent, 0, photo.ImageContent.Length);
                    //var returnImage = System.Drawing.Image.FromStream(ms, true);
                    //var directories = ImageMetadataReader.ReadMetadata(@"C:\11.jpg");

                    //if (imageInstance.Width < imageInstance.Height)
                    //{
                    //    imageOrientation.HorizontalImages.Add(new TextSharpImageContent { Image = imageInstance, ImageContent = photo });
                    //    imageInstance.Rotation = 1.57f;
                    //    imageInstance.ScaleAbsolute(260f, 400f);
                    //}
                    //else
                    //{
                    //    imageInstance.Rotation = -1.57f;
                    //    imageInstance.ScaleAbsolute(180f, 160f);
                    //    imageInstance.ScaleToFitLineWhenOverflow = true;
                    //    imageOrientation.VerticalImages.Add(new TextSharpImageContent { Image = imageInstance, ImageContent = photo });
                    //}
                }
            }

            return imageOrientation;
        }        
    }
}
