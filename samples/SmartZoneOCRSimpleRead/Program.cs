// Copyright Accusoft Corporation

using System;
using System.IO;
using System.Reflection;
using Accusoft.SmartZoneOCRSdk;

namespace SmartZoneOCRSimpleRead
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string ocrImagePath = Path.Combine(GetProjectDir(), @"../../input/OCR/MultiLine.bmp");
            Image ocrImage = new Image(ocrImagePath);
            string ocrResults =  Process(ocrImage);
            PrintResults(ocrImagePath, ocrResults, "OCR");
        }

        public static string GetProjectDir()
        {
            var localDir = Assembly.GetExecutingAssembly().Location;
            while (!localDir.EndsWith("SmartZoneOCRSimpleRead"))
                localDir = Path.GetDirectoryName(localDir);
            return localDir;
        }

        public static string Process(Image image)
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

                instance.Reader.CharacterSet = CharacterSet.AllCharacters;
                instance.Reader.CharacterSet.Language = Language.WesternEuropean;

                // Optional. Zonal recognition support. Can be changed to recognize specific parts of the image.
                instance.Reader.Zone = new Zone(0, 0, image.Width, image.Height);

                TextBlockResult result = instance.Reader.AnalyzeField(image);
                return result.Text;
            }
        }

        private static void PrintResults(string imagePath, string results, string mode)
        {
            Console.Out.WriteLine(string.Format("{0} recognition results for {1}:", mode, imagePath));
            Console.Out.WriteLine(results);
        }
    }
}
