@echo off
SET PFiles=%ProgramFiles(x86)%
SET CFiles=%CommonProgramFiles(x86)%
SET TTOptionsSys=-r System.Windows.Forms.dll 
SET TTOptions1=-P packages/Microsoft.OData.Client.6.11.0/lib/portable-net45+wp8+win8+wpa
SET TTOptions2=-P packages/Microsoft.OData.Core.6.11.0/lib/portable-net40+sl5+wp8+win8+wpa
SET TTOptions3=-P packages/Microsoft.OData.Edm.6.11.0/lib/portable-net40+sl5+wp8+win8+wpa
SET TTOptions4=-P packages/Microsoft.Spatial.6.11.0/lib/portable-net40+sl5+wp8+win8+wpa
SET TextTemplating="%PFiles%\Common Files\Microsoft Shared\TextTemplating\14.0\TextTransform.exe" %TTOptionsSys% %TTOptions1% %TTOptions2% %TTOptions3% %TTOptions4%
SET TF="%PFiles%\Microsoft Visual Studio 14.0\Common7\IDE\TF.exe"
FOR /R ..\ %%f in (*.tt) DO call:action "%%~df%%~pf%%~nf"

GOTO end

:action
SET BaseName=%1
SET TemplateName=%BaseName%.tt
ECHO Processing %TemplateName% 
REM %TF% checkout %BaseName%.*
%TextTemplating% %TemplateName% 
goto:eof

:end