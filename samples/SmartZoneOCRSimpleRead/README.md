# SmartZoneOCRSimpleRead

This sample program demonstrates the basics of SmartZone functionality in a simple command line format.
Sample supports Microsoft Visual Studio and Microsoft Visual Studio Code.

## Building the Sample

To build this sample with Visual Studio 2017 or later, open the .sln file in the project directory, select a Solution Configuration (Debug or Release) and a Solution Platform (AnyCPU), then build with Build Solution located in the Build menu.
To build this sample from the command line, make sure `msbuild.exe` is in your path. Navigate to the sample directory and run the command `msbuild -t:Restore;Rebuild SmartZoneOCRSimpleRead.sln`.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

NOTE: SmartZone .NET runs in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, please contact Accusoft at info@accusoft.com.

## Important files

Provided SmartZone source files demonstrate recognition engine parameters that may be tuned to achieve the best results.

### Main module required to load the image and print the results

Program.cs

### Context specific configuration parameters

app.config
