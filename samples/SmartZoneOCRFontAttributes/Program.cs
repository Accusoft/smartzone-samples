﻿// Copyright Accusoft Corporation

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Accusoft.SmartZoneOCRSdk;

namespace SmartZoneOCRSimpleRead
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            string ocrImagePath = Path.Combine(GetProjectDir(), @"../../input/OCR/couriernew_serif_bolditalic_monospace.bmp");
            Image image = new Image(ocrImagePath);

            TextBlockResult ocrResults = Process(image);

            PrintResults(ocrImagePath, ocrResults.Text);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                PrintFontAttributes(ocrImagePath, ocrResults);
            }
            else
            {
                Console.WriteLine("Accessing font attribute information is only supported for Windows platforms. Please read this sample's README.md for more information.");
            }
        }
        public static string GetProjectDir()
        {
            var localDir = Assembly.GetExecutingAssembly().Location;
            while (!localDir.EndsWith("SmartZoneOCRFontAttributes"))
                localDir = Path.GetDirectoryName(localDir);
            return localDir;
        }

        public static TextBlockResult Process(Image image)
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

                return result;
            }
        }

        private static void PrintResults(string imagePath, string results)
        {
            Console.Out.WriteLine(string.Format("OCR recognition results for {0}:\n", imagePath));
            Console.Out.WriteLine("{0}\n", results);
        }

        private static void PrintFontAttributes(string imagePath, TextBlockResult ocrResults)
        {
            Console.Out.WriteLine(string.Format("OCR character attribute results for {0}:\n", imagePath));

            for (int i = 0; i < ocrResults.NumberTextLines; i++)
            {
                for (int j = 0; j < ocrResults.TextLine(i).NumberCharacters; j++)
                {
                    CharacterResult characterResult = ocrResults.TextLine(i).Character(j);

                    if (characterResult != null)
                    {
                        Console.Out.WriteLine(string.Format("Character: {0}", characterResult.Text));
                        Console.Out.WriteLine(string.Format("Font Attributes: {0}", characterResult.FontAttribute.ToString()));
                        Console.Out.WriteLine(string.Format("Font Size: {0}", characterResult.FontSize));
                        Console.Out.WriteLine(string.Format("Capital Letter Height: {0}", characterResult.CapitalLetterHeight));
                        Console.Out.WriteLine(string.Format("Base Line: {0}\n", characterResult.Baseline));
                    }
                }
            }
        }
    }
}
