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
            string ocrImagePath = @"..\..\..\..\input\OCR\Multiline.bmp";
            Bitmap ocrImage = new Bitmap(ocrImagePath);
            string ocrResults =  Process(ocrImage);
            PrintResults(ocrImagePath, ocrResults, "OCR");
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

                // This call will set the OCRDataPath according to the value in app.config.
                // Uncomment if you are deploying the OCR asset files in a non-default location.
                // By default, the OCR asset files will be unpacked to the build output directory.
				// Refer to https://help.accusoft.com/SmartZone/latest/netframework/webframe.html#Distributing_SmartZone_OCR.html for details.
                //instance.OCRDataPath = GetOCRDataPath();

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

        private static string GetOCRDataPath()
        {
            // OCRDataPath property should point to the assets directory.
            string OCRDataPath = new AppSettingsReader().GetValue("OCRDataPath", typeof(string)) as string;
            OCRDataPath = OCRDataPath.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

            if (!string.IsNullOrEmpty(OCRDataPath))
            {
                if (Path.IsPathRooted(OCRDataPath))
                {
                    // we should remove possible .. and . symbols from the path.
                    OCRDataPath = Path.GetFullPath(OCRDataPath);
                }
                else
                {
                    // assume relative path base uses AppDomain.CurrentDomain.BaseDirectory as a root.
                    OCRDataPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, OCRDataPath));
                }
            }
            else
            {
                // assume assets directory already placed nearby the executable.
                OCRDataPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
            }
            return OCRDataPath;
        }
    };
}
