<#
    .SYNOPSIS
    .DESCRIPTION
    .EXAMPLE
#>
function global:Add-Mapper
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
    )
    [string]$projectName = Read-Host -Prompt 'Input your .NET Core Library project name'

    if (-not $(Test-Path  -Path "$outuptDir"))
    {
        New-Item -Type Directory -Path "$outuptDir"
    }

    [string]$targetFramework = Get-ProjectTargetFramework

    dotnet sln "$PSScriptRoot\SitecoreJsonMapper.sln" remove "$PSScriptRoot\$projectName"
    Remove-Item `
        -Path "$PSScriptRoot\$projectName" `
        -Recurse -Force `
        -ErrorAction SilentlyContinue
    dotnet remove "$PSScriptRoot\SitecoreJsonMapper" reference "..\$projectName\$projectName.cproj"

    dotnet new classlib `
        --output "$PSScriptRoot\$projectName\" `
        --framework $targetFramework

    Remove-Item -Path "$PSScriptRoot\$ProjectName\*.cs" -Force

    dotnet add "$PSScriptRoot\SitecoreJsonMapper" reference "$PSScriptRoot\$projectName"
    dotnet add "$PSScriptRoot\$projectName" reference "$PSScriptRoot\SitecoreJsonMapper.Common\"
    dotnet add "$PSScriptRoot\$projectName" reference "$PSScriptRoot\SitecoreJsonMapper.Interface\"
    dotnet sln "$PSScriptRoot\SitecoreJsonMapper.sln" add "$PSScriptRoot\$projectName"

    Create-ProjectFiles -ProjectName $projectName

    dotnet build
}

<#
    .SYNOPSIS
    .DESCRIPTION
    .EXAMPLE
#>
function local:Get-ProjectTargetFramework
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
    )
    [Xml]$cprojFile = Get-Content "$PSScriptRoot\SitecoreJsonMapper\SitecoreJsonMapper.csproj"
    [Xml.XmlNode]$targetFrameworkNode = $cprojFile.SelectSingleNode('/Project/PropertyGroup/TargetFramework')
    $targetFrameworkNode.InnerText
}

<#
    .SYNOPSIS
    .DESCRIPTION
    .PARAMETER <Param1>
    .EXAMPLE
#>
function local:Create-ProjectFiles
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ProjectName
    )
    New-Item `
        -Type Directory `
        -Path "$PSScriptRoot\$ProjectName\Mappings"
    Create-JsonDataSource -ProjectName $ProjectName
    Create-JsonDataTarget -ProjectName $ProjectName

    New-Item `
        -Type Directory `
        -Path "$PSScriptRoot\$ProjectName\JsonDataSource"
    Create-ExampleJson -ProjectName $ProjectName
 }

 <#
    .SYNOPSIS
    .DESCRIPTION
    .PARAMETER <Param1>
    .EXAMPLE
#>
function local:Create-JsonDataSource
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ProjectName
    )
    New-Item `
        -Type File `
        -Path "$PSScriptRoot\$ProjectName\Mappings\JsonDataSource.cs"
    [string]$jsonDataSourceTemplateClass =
@"
using SitecoreJsonMapper.Common.Mappings;

namespace {0}.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        /// <summary>
        /// Put all properties of a generated RootObject class here.
        /// You can set JsonDataTarget properties by the set accessor.
        /// </summary>
        public string ExampleProperty {
            set {
                jsonDataTarget.ExampleProperty =  $"{value}-generated";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void PostProcess()
        {

        }
    }
}
"@
    $jsonDataSourceTemplateClass = $jsonDataSourceTemplateClass.Replace("{0}", "$ProjectName")

    Set-Content `
        -Path "$PSScriptRoot\$projectName\Mappings\JsonDataSource.cs" `
        -Value "$jsonDataSourceTemplateClass"
}

<#
    .SYNOPSIS
    .DESCRIPTION
    .PARAMETER <Param1>
    .EXAMPLE
#>
function local:Create-JsonDataTarget
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ProjectName
    )
    New-Item `
        -Type File `
        -Path "$PSScriptRoot\$ProjectName\Mappings\JsonDataTarget.cs"
    [string]$jsonDataTargetTemplateClass =
@"
using SitecoreJsonMapper.Interface.Mappings;

namespace {0}.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        /// <summary>
        /// 
        /// </summary>
        public string ExampleProperty { get; set; }
    }
}
"@
    $jsonDataTargetTemplateClass = $jsonDataTargetTemplateClass.Replace("{0}", "$ProjectName")

    Set-Content `
        -Path "$PSScriptRoot\$projectName\Mappings\JsonDataTarget.cs" `
        -Value "$jsonDataTargetTemplateClass"
}

<#
    .SYNOPSIS
    .DESCRIPTION
    .PARAMETER <Param1>
    .EXAMPLE
#>
function local:Create-ExampleJson
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ProjectName
    )
    New-Item `
        -Type File `
        -Path "$PSScriptRoot\$ProjectName\JsonDataSource\Example.json"
    [string]$exampleJsonDataSource =
@"
{
	"ExampleProperty": "ExampleValue"
}
"@

    Set-Content `
        -Path "$PSScriptRoot\$projectName\JsonDataSource\Example.json" `
        -Value "$exampleJsonDataSource"
}

Add-Mapper