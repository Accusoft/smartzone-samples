# SmartZoneOCRFontAttributes

This sample program demonstrates in a simple command line format how to use SmartZone access information about a character's font style (bold, italic), font category details, and font size.

The sample supports Microsoft dotnet command line and Visual Studio Code.

## Building the Sample

From the sample directory run the command:

```bash
dotnet build SmartZoneOCRFontAttributes.csproj
```

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. The following command will run this sample.

```bash
dotnet run --project SmartZoneOCRFontAttributes.csproj
```

## Running the Sample in Visual Studio Code

Use `File->Open Folder...` menu and select samples/SmartZoneOCRFontAttributes directory.
Use Visual Studio Code `Run` and `Run and Debug` modes to work with the Sample.

### Note

When running in evaluation mode, SmartZone will replace portions of recognition results with the text " [Accusoft Evaluation] " and asterisk "*" characters. This is an appropriate evaluation mode for applications that have no user interface.

Information about a character's FontAttributes and FontSize is currently only supported on Windows platforms.

## Important files

Provided SmartZone source files demonstrate recognition engine parameters that may be tuned to achieve the best results.

### Main module required to load the image and print the results

Program.cs
