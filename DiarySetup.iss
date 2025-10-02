[Setup]
AppName=My Diary
AppVersion=1.0
DefaultDirName={pf}\My Diary
DefaultGroupName=My Diary
OutputDir=Output
OutputBaseFilename=MyDiarySetup
Compression=lzma
SolidCompression=yes

[Files]
Source: "bin\Release\net9.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\My Diary"; Filename: "{app}\Diary.exe"
Name: "{commondesktop}\My Diary"; Filename: "{app}\Diary.exe"

[Run]
Filename: "{app}\Diary.exe"; Description: "Launch My Diary"; Flags: nowait postinstall skipifsilent