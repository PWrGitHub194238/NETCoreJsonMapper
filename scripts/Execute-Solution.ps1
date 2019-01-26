[string]$SLN_DIR = Resolve-Path -Path "$PSScriptRoot\..\NETCoreJsonMapper"

<#
    .SYNOPSIS
        Executes a given $Target with a given set of $Parameters.
    .DESCRIPTION
        Executes a given $Target with a given set of $Parameters.
    .PARAMETER Target
        The full path of the executable file.
    .PARAMETER Parameters
        The optional list of parameters that the $Target accepts.
#>
function local:Execute-Solution
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([string])]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$Target
    ,
        [Parameter(Mandatory = $false)]
        [string[]]$Parameters
    )
    Write-Host -ForegroundColor White "Executing with parameters:"
    Write-Host -ForegroundColor White "> $Parameters"
    dotnet $Target $Parameters
}

<#
    .SYNOPSIS
        Returns a list of arguments to be passed 
        to the NETCoreJsonMapper executable as parameters.
    .DESCRIPTION
        Given the current directory ($SLN_DIR), the cmdlet will recursively scan through 
        all directories on the same level as the main project (NETCoreJsonMapper) 
        and returns every 'JsonDataSource' directory from any project that contains it.
        It applies all resulted paths to the argument string for a NETCoreJsonMapper executable
        along with the output directory parameter, read from a user.
#>
function local:Get-ExecuteCommand
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([string[]])]
    param (
    )

    [string]$outuptDir = Read-HostWithDefault `
        -Prompt 'Input JSON output directory' `
        -Default "$(Resolve-Path "$SLN_DIR\..")\output"

    [string[]]$projParams = @()
    [string[]]$projFullPathArray = Get-ChildItem `
            -File `
            -Path $SLN_DIR `
            -Recurse `
            -Include '*.csproj' `
        | ? { Test-Path ($_.DirectoryName + '\JsonDataSource' ) } `
        | % { $_.DirectoryName }

    foreach ($projFullPath in $projFullPathArray)
    {
        $projParams += "-i"
        $projParams += "`"$projFullPath\JsonDataSource`""
    }
    $projParams += "-o"
        $projParams += "`"$outuptDir`""
    return $projParams
}

<#
    .SYNOPSIS
        A wrapper to the Read-Host smdlet with a default value $Default support.
    .DESCRIPTION
        The Read-Host cmdlet reads a line of input from the console.
        If no value was given, the $Default value will be returned.
    .PARAMETER Prompt
        Specifies the text of the prompt. Type a string. If the string includes spaces, 
        enclose it in quotation marks. PowerShell appends a colon (:) 
        to the text that you enter.
    .PARAMETER Default
        The value to be returned if no user input was given.
    .EXAMPLE
        Read-HostWithDefault `
            -Prompt 'Input JSON output directory' `
            -Default "$SLN_DIR\output"
#>
function local:Read-HostWithDefault
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([string])]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$Prompt
    ,
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$Default      
    )
    [string]$resultValue = Read-Host "$Prompt ('$Default' if empty)"
    return ($Default,$resultValue)[[bool]$resultValue]
}

Execute-Solution `
    -Target $(Resolve-Path -Path "$SLN_DIR\..\publish\NETCoreJsonMapper.dll") `
    -Parameters $(Get-ExecuteCommand)