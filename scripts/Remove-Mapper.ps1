[string]$SLN_DIR = Resolve-Path -Path "$PSScriptRoot\..\NETCoreJsonMapper"

<#
    .SYNOPSIS
    .DESCRIPTION

#>
function global:Remove-Mapper
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
        [Parameter(Mandatory = $false)]
        [string]$ProjectName
    ,
        [Switch]$RemoveOnly
    )

    if (!$ProjectName)
    {
        $ProjectName = Read-Host -Prompt 'Input your .NET Core Library project name'
    }
    if ($ProjectName -and $(Test-Path -Path "$SLN_DIR\$ProjectName"))
    {
        dotnet sln "$SLN_DIR\NETCoreJsonMapper.sln" remove "$SLN_DIR\$ProjectName"
        Remove-Item `
            -Path "$SLN_DIR\$ProjectName" `
            -Recurse -Force `
            -ErrorAction SilentlyContinue
        dotnet remove "$SLN_DIR\NETCoreJsonMapper" reference "..\$ProjectName\$ProjectName.csproj"
        
        if (-not $RemoveOnly)
        {
            dotnet build $SLN_DIR
        }
    }
    elseif (-not $RemoveOnly)
    {
        Write-Host -ForegroundColor Red "The given project: '$ProjectName' does not exists."
    }
}