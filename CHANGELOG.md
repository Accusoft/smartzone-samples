# CHANGELOG

## 2022-04-21

### Added

- SmartZone v7 has been released, [see our documentation](https://help.accusoft.com/SmartZone/latest/dotnet/webframe.html#overview.html) for full release notes.
- Added a `using` statement in the SmartZoneOCRSimpleRead sample's `Program.cs` to better handle disposal & cleanup.
- Added support .NET Standard 2.0.
- Added support for loading 8-, 24- and 32-bit images.
- Added Linux support.
- Added NuGet dependency packages, Accusoft.SmartZone.Runtime.Win/Lin, to Accusoft.SmartZone.Net package.
- Removed NuGet dependency package, Accusoft.SmartZone.Runtime.Win, from Accusoft.SmartZoneOCR.Net package.
- Running dotnet publish now copies the required OCR assets from the NuGet packages to the publishing folder.
- Microsoft Windows CRT libraries updated to version 14.30.30708.0.
- When you download SmartZone, an Evaluation license is enabled automatically for Windows and Linux.

### Changed

- The classes [Accusoft.SmartZoneICRSdk.SmartZoneException](https://help.accusoft.com/SmartZone/latest/dotnet/webframe.html#Accusoft.SmartZoneICR.Net~Accusoft.SmartZoneICRSdk.SmartZoneException.html)
    and [Accusoft.SmartZoneOCRSdk.SmartZoneException](https://help.accusoft.com/SmartZone/latest/dotnet/webframe.html#Accusoft.SmartZoneOCR.Net~Accusoft.SmartZoneOCRSdk.SmartZoneException.html)
    now derive from System.Exception instead of System.ApplicationException.
- Unsupported characters have been removed from all predefined character sets in the [CharacterSet](https://help.accusoft.com/SmartZone/latest/dotnet/webframe.html#Accusoft.SmartZoneOCR.Net~Accusoft.SmartZoneOCRSdk.CharacterSet.html) class. See our documentation page [Upgrading SmartZone OCR](https://help.accusoft.com/SmartZone/latest/dotnet/webframe.html#Upgrading_SmartZone_OCR.html) for more details.
- Some native libraries that were previously integrated in the **Accusoft.SmartZoneOCR.Net** assembly are now included as separate dlls. These libraries will be extracted to the same directory as the assembly when using the Nuget Package. The libraries are named **Accusoft.SmartZoneOCR.Common.dll** and **Accusoft.SmartZoneOCR.Common64.dll** on Windows. For Linux distributions, the name of the file is **Accusoft.SmartZoneOCR.CommonLinux64.so**. These need to be included when redistributing any application that uses the SmartZoneOCR SDK.
- Some native libraries that were previously integrated in the **Accusoft.SmartZoneICR.Net** assembly are now included as separate dlls. These libraries will be extracted to the same directory as the assembly when using the Nuget Package. The libraries are named **Accusoft.SmartZoneICR.Common.dll** and **Accusoft.SmartZoneICR.Common64.dll** on Windows. For Linux distributions, the name of the file is **Accusoft.SmartZoneICR.CommonLinux64.so**. These need to be included when redistributing any application that uses the SmartZoneICR SDK.

### Removed

- Removed method `GetOCRDataPath()` from the SmartZoneOCRSimpleRead sample.
- Removed `app.config` used by `GetOCRDataPath()` from the SmartZoneOCRSimpleRead sample.
- Made significant removals & modifications to `SmartZoneOCRSimpleRead.csproj` during transition from .NET Framework to .NET Standard.

## 2021-08-17

### Added

- First release

### Changed

### Removed
