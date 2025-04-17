# SmartZoneOCRAutoRotation

This sample demonstrates three ways to detect orientation and rotates a document for recognition. These methods offer several degrees of performance, accuracy, and control to the user.

`UsingSmartZoneOnly()` demonstrates SmartZone's recognition engine's automatic orientation detection and rotation feature. This method is the fastest, but may have reduced accuracy because the document is not deskewed prior to recognition.

`AutoOrientationSmartZone()` demonstrates a way to enhance SmartZone's recognition by using ImageGear to deskew the image prior to auto-rotating the document with SmartZone. Deskewing will add processing time, but this will have higher accuracy than using SmartZone alone for rotation.

`AutoOrientationImageGear()` demonstrates a way to gather all information on a document and manually rotate it before recognition. It calls SmartZone's `DetectOrient()` method, which returns the orientation of the document to the user. This gives the user control by allowing them to integrate their own rotation process at any point in their program, rather than it always happening at the recognition step.

The sample supports Microsoft dotnet command line and Visual Studio Code.

## Building the Sample

From the sample directory run the command:

```bash
dotnet build SmartZoneOCRAutoRotation.csproj
```

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. The following command will run this sample.

```bash
dotnet run --project SmartZoneOCRAutoRotation.csproj
```

## Running the Sample in Visual Studio Code

Use `File->Open Folder...` menu and select the samples/SmartZoneOCRAutoRotation directory.
Use Visual Studio Code `Run` and `Run and Debug` modes to work with the Sample.

### Note

When running in SmartZone evaluation mode, SmartZone will replace portions of recognition results with the text " [Accusoft Evaluation] " and asterisk "*" characters. This is an appropriate evaluation mode for applications that have no user interface.

## Important files

Provided SmartZone source files demonstrate recognition engine parameters that may be tuned to achieve the best results.

### Main module required to load the image and print the results

Program.cs
