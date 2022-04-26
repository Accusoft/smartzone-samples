# SmartZoneOCRSimpleRead

This sample program demonstrates the basics of SmartZone functionality in a simple command line format.
Sample supports Microsoft dotnet command line.

## Building the Sample

From the sample directory run the command:

```bash
dotnet build SmartZoneOCRSimpleRead.csproj
```

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. The following command will run this sample.

```bash
dotnet run --project SmartZoneOCRSimpleRead.csproj
```

### Note

When running in evaluation mode, SmartZone will replace portions of recognition results with the text " [Accusoft Evaluation] " and asterisk "*" characters. This is an appropriate evaluation mode for applications that have no user interface.

## Important files

Provided SmartZone source files demonstrate recognition engine parameters that may be tuned to achieve the best results.

### Main module required to load the image and print the results

Program.cs
