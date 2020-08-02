using namespace System.IO;

param([string]$SolutionDir, [string]$ConfigurationName)

$dir_bin_output = '{0}\bin\{1}\netcoreapp3.1' -f $SolutionDir, $ConfigurationName;
$dir_nabla_binaries = '{0}\bin\{1}\net48' -f $SolutionDir, $ConfigurationName;
$dir_compiler_binaries = '{0}\compilers' -f $dir_bin_output;

if([Directory]::Exists($dir_compiler_binaries)){
    [Directory]::Delete($dir_compiler_binaries, $true);
}
[Directory]::CreateDirectory($dir_compiler_binaries);

# copia de archivos.
[File]::Copy('{0}\VMCS\Minics.exe' -f $SolutionDir, '{0}\Minics.exe' -f $dir_compiler_binaries, $true);

foreach($file in [Directory]::GetFiles($dir_nabla_binaries)) {
    $fi = [FileInfo]::new($file);
    [File]::Copy($file, ('{0}\{1}' -f $dir_compiler_binaries, $fi.Name), $true);
}