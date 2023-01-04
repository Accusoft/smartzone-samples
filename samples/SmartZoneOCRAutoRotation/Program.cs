// Copyright Accusoft Corporation

using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using Accusoft.SmartZoneOCRSdk;
using ImageGear.Processing;
using ImageGear.Core;
using ImageGear.Formats;

namespace SmartZoneOCRAutoRotation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (SmartZoneOCR instance = new SmartZoneOCR())
            {
                // Licensing code
                // The SetSolutionName, SetSolutionKey and possibly the SetOEMLicenseKey methods must be
                // called to distribute the runtime. Note that the SolutionName, SolutionKey and
                // OEMLicenseKey values shown below are only examples.

                // instance.Licensing.SetSolutionName("Your Solution Name");
                // instance.Licensing.SetSolutionKey(12345, 12345, 12345, 12345);
                // instance.Licensing.SetOEMLicenseKey("AStringForOEMLicensingContactAccusoftSalesForMoreInformation...");

                // It would be better to have Accusoft ImageGear license to prevent ImageGear watermark effecting SmartZone OCR result.
                // ImGearLicense.SetSolutionName("Your Solution Name");
                // ImGearLicense.SetSolutionKey(12345, 12345, 12345, 12345);
                // ImGearLicense.SetOEMLicenseKey("AStringForOEMLicensingContactAccusoftSalesForMoreInformation...");

                const string ocrImagePath = @"../../input/OCR/INV645694-180-SKEW.bmp";
                string ocrResults;

                ocrResults = UsingSmartZoneOnly(instance, ocrImagePath);
                PrintResults(ocrImagePath, ocrResults, "Using SmartZone only");

                ocrResults = AutoOrientationSmartZone(instance, ocrImagePath);
                PrintResults(ocrImagePath, ocrResults, "Deskew by ImageGear and auto rotate and OCR by SmartZone");

                ocrResults = AutoOrientationImageGear(instance, ocrImagePath);
                PrintResults(ocrImagePath, ocrResults, "Deskew and rotate by ImageGear and OCR by SmartZone");
            }
        }

        public static string UsingSmartZoneOnly(SmartZoneOCR instance, string imagePath)
        {
            Bitmap bitmap = Image.FromFile(imagePath) as Bitmap;

            instance.Reader.CharacterSet = CharacterSet.AllCharacters;
            instance.Reader.CharacterSet.Language = Language.WesternEuropean;

            instance.Reader.UseAutoRotate = true;

            instance.Reader.Area = new Rectangle(710, 2855, 690, 200);

            TextBlockResult result = instance.Reader.AnalyzeField(bitmap);

            return result.Text;
        }

        public static string AutoOrientationSmartZone(SmartZoneOCR instance, string imagePath)
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            ImGearPage imGearPage = null;

            using (FileStream inputStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

            // Used ImageGear Deskew() to have better OCR accuracy.
            ImGearRasterProcessing.Deskew((ImGearRasterPage)imGearPage, 0.1, ImGearRotationModes.CLIP, null);

            Bitmap bitmap = ImGearFileFormats.ExportPageToBitmap(imGearPage);

            instance.Reader.CharacterSet = CharacterSet.AllCharacters;
            instance.Reader.CharacterSet.Language = Language.WesternEuropean;

            instance.Reader.UseAutoRotate = true;

            instance.Reader.Area = new Rectangle(710, 2855, 690, 200);

            TextBlockResult result = instance.Reader.AnalyzeField(bitmap);

            return result.Text;
        }

        public static string AutoOrientationImageGear(SmartZoneOCR instance, string imagePath)
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            ImGearPage imGearPage = null;

            using (FileStream inputStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

            // Used ImageGear Deskew() to have better OCR accuracy.
            ImGearRasterProcessing.Deskew((ImGearRasterPage)imGearPage, 0.1, ImGearRotationModes.CLIP, null);

            Bitmap bitmap = ImGearFileFormats.ExportPageToBitmap(imGearPage);

            OCROrientationMode szOcrOrientationMode = instance.Reader.DetectOrient(bitmap);

            ImGearProcessing.Rotate(imGearPage, GetRotationalValue(szOcrOrientationMode));

            // Save rotated image if it's required.
            using (FileStream outputStream = new FileStream(@"../../input/OCR/INV645694-180-SKEW-IG.bmp", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.BMP_UNCOMP);

            bitmap = ImGearFileFormats.ExportPageToBitmap(imGearPage);

            instance.Reader.CharacterSet = CharacterSet.AllCharacters;
            instance.Reader.CharacterSet.Language = Language.WesternEuropean;

            instance.Reader.Area = new Rectangle(710, 2855, 690, 200);

            TextBlockResult result = instance.Reader.AnalyzeField(bitmap);

            return result.Text;
        }

        private static ImGearRotationValues GetRotationalValue(OCROrientationMode szOcrOrientationMode)
        {
            ImGearRotationValues igRotationValues = ImGearRotationValues.VALUE_0;

            switch (szOcrOrientationMode)
            {
                case OCROrientationMode.No:
                    igRotationValues = ImGearRotationValues.VALUE_0;
                    break;
                case OCROrientationMode.Right:
                    igRotationValues = ImGearRotationValues.VALUE_90;
                    break;
                case OCROrientationMode.Down:
                    igRotationValues = ImGearRotationValues.VALUE_180;
                    break;
                case OCROrientationMode.Left:
                    igRotationValues = ImGearRotationValues.VALUE_270;
                    break;
            }

            return igRotationValues;
        }

        private static void PrintResults(string imagePath, string results, string mode)
        {
            Console.Out.WriteLine(string.Format("{0} recognition results for {1}:", mode, imagePath));
            Console.Out.WriteLine(results);
            Console.Out.WriteLine("");
        }
    }
}
