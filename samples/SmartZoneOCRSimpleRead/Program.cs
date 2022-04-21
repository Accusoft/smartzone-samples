// Copyright Accusoft Corporation

using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using Accusoft.SmartZoneOCRSdk;

namespace SmartZoneOCRSimpleRead
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string ocrImagePath = @"../../input/OCR/MultiLine.bmp";
            using (Bitmap ocrImage = Image.FromFile(ocrImagePath) as Bitmap)
            {
                string ocrResults =  Process(ocrImage);
                PrintResults(ocrImagePath, ocrResults, "OCR");
            }
        }

        public static string Process(Bitmap bitmap)
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
                instance.Reader.Area = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                TextBlockResult result = instance.Reader.AnalyzeField(bitmap);
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
