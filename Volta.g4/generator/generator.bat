set g4Dir=../
if not exist "./antlr-4.8-complete.jar" (
    set g4Dir=../Volta.g4/
)
echo %g4Dir%
java -jar %g4Dir%/generator/antlr-4.8-complete.jar -Dlanguage=CSharp %g4Dir%/VoltaScanner.g4 -o %g4Dir%/../Volta/Compiler
java -jar %g4Dir%/generator/antlr-4.8-complete.jar -Dlanguage=CSharp %g4Dir%/VoltaParser.g4 -o %g4Dir%/../Volta/Compiler
pause