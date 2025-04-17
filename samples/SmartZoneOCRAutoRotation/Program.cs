// Copyright Accusoft Corporation

using System;
using System.IO;
using Accusoft.SmartZoneOCRSdk;
using ImageGear.Processing;
using ImageGear.Core;
using ImageGear.Formats;
using System.Reflection;
using System.Management;

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

                string ocrImagePath = Path.Combine(GetProjectDir(), @"../../input/OCR/INV645694-180-SKEW.bmp");
                string ocrResults;

                ocrResults = UsingSmartZoneOnly(instance, ocrImagePath);
                PrintResults(ocrImagePath, ocrResults, "Using SmartZone only");

                ocrResults = AutoOrientationSmartZone(instance, ocrImagePath);
                PrintResults(ocrImagePath, ocrResults, "Deskew by ImageGear and auto rotate and OCR by SmartZone");

                ocrResults = AutoOrientationImageGear(instance, ocrImagePath);
                PrintResults(ocrImagePath, ocrResults, "Deskew and rotate by ImageGear and OCR by SmartZone");
            }
        }

        public static string GetProjectDir()
        {
            var localDir = Assembly.GetExecutingAssembly().Location;
            while(!localDir.EndsWith("SmartZoneOCRAutoRotation"))
                localDir = Path.GetDirectoryName(localDir);
            return localDir;
        }

        // Recognize field using SmartZone with default settings
        public static string UsingSmartZoneOnly(SmartZoneOCR instance, string imagePath)
        {
            instance.Reader.CharacterSet = CharacterSet.AllCharacters;
            instance.Reader.CharacterSet.Language = Language.WesternEuropean;

            instance.Reader.UseAutoRotate = false;

            instance.Reader.Zone = new Zone(710, 2855, 690, 200);
            using (Stream fs = File.Open(imagePath, FileMode.Open))
            {
                TextBlockResult result = instance.Reader.AnalyzeField(fs);
                return result.Text;
            }
        }

        // Recognize field using SmartZone with auto orientation
        public static string AutoOrientationSmartZone(SmartZoneOCR instance, string imagePath)
        {
            instance.Reader.CharacterSet = CharacterSet.AllCharacters;
            instance.Reader.CharacterSet.Language = Language.WesternEuropean;

            instance.Reader.UseAutoRotate = true;

            instance.Reader.Zone = new Zone(710, 2855, 690, 200);
            using (Stream fs = File.Open(imagePath, FileMode.Open))
            {
                TextBlockResult result = instance.Reader.AnalyzeField(fs);
                return result.Text;
            }
        }

        // Use ImageGear for pre-processing
        public static string AutoOrientationImageGear(SmartZoneOCR instance, string imagePath)
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            ImGearPage imGearPage = null;

            using (FileStream inputStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

            // Used ImageGear Deskew() to have better OCR accuracy.
            ImGearRasterProcessing.Deskew((ImGearRasterPage)imGearPage, 0.1, ImGearRotationModes.CLIP, null);

            Image image = new Image(imGearPage);

            OCROrientationMode szOcrOrientationMode = instance.Reader.DetectOrient(image);
            ImGearProcessing.Rotate(imGearPage, GetRotationalValue(szOcrOrientationMode));

            instance.Reader.CharacterSet = CharacterSet.AllCharacters;
            instance.Reader.CharacterSet.Language = Language.WesternEuropean;

            instance.Reader.Zone = new Zone(710, 2855, 690, 200);

            image = new Image(imGearPage);
            TextBlockResult result = instance.Reader.AnalyzeField(image);

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
