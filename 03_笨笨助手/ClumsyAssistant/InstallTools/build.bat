copy %CD%\ILMerge.exe %CD%\..\bin\Release\
echo "����ILMerge.exe�ɹ���"
cd ..\bin\Release\
ILMerge.exe /ndebug /target:winexe /targetplatform:v4  /out:��������.exe ClumsyAssistant.exe  /log Aspose.Cells.dll
echo "����exe�ɹ���"
cmd