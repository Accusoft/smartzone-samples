# SmartZoneOCRSimpleRead

This sample program demonstrates the basics of SmartZone functionality in a simple command line format.
The sample supports Microsoft dotnet command line and Visual Studio Code.

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

## Running the Sample in Visual Studio Code

Use `File->Open Folder...` menu and select the top-level directory cloned from the github.
Use Visual Studio Code `Run` and `Run and Debug` modes to work with the Sample.

### Note

When running in evaluation mode, SmartZone will replace portions of recognition results with the text " [Accusoft Evaluation] " and asterisk "*" characters. This is an appropriate evaluation mode for applications that have no user interface.

## Important files

Provided SmartZone source files demonstrate recognition engine parameters that may be tuned to achieve the best results.

### Main module required to load the image and print the results

Program.cs
