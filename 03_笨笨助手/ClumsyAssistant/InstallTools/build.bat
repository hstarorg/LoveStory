copy %CD%\ILMerge.exe %CD%\..\bin\debug\
echo "复制ILMerge.exe成功！"
cd ..\bin\debug\
ILMerge.exe /ndebug /target:winexe /targetplatform:v4  /out:笨笨助手.exe ClumsyAssistant.exe  /log Aspose.Cells.dll
echo "生成exe成功！"
cmd