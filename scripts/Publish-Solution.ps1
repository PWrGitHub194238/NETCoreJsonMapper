[string]$SLN_DIR = Resolve-Path -Path "$PSScriptRoot\..\NETCoreJsonMapper"

<#
    .SYNOPSIS
        Publsh the current solution ('NETCoreJsonMapper') to a given $Target directory.
    .DESCRIPTION
        Publsh the current solution ('NETCoreJsonMapper') to a given $Target directory.
    .PARAMETER Target
        The full path to a directory where the published solution will be put.
#>
function global:Publish-Solution
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$Target
    )
    Remove-Item -Path $Target -Recurse -Force -ErrorAction SilentlyContinue

    dotnet clean $SLN_DIR
    dotnet restore $SLN_DIR
    dotnet build $SLN_DIR
    dotnet publish "$SLN_DIR\NETCoreJsonMapper" `
        --configuration Release `
        --output $Target
}

Publish-Solution -Target $(Resolve-Path -Path  "$SLN_DIR\..\publish")