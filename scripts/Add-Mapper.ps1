[string]$SLN_DIR = Resolve-Path -Path "$PSScriptRoot\..\NETCoreJsonMapper"

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

    dotnet sln "$SLN_DIR\SitecoreJsonMapper.sln" remove "$SLN_DIR\$projectName"
    Remove-Item `
        -Path "$SLN_DIR\$projectName" `
        -Recurse -Force `
        -ErrorAction SilentlyContinue
    dotnet remove "$SLN_DIR\SitecoreJsonMapper" reference "..\$projectName\$projectName.cproj"

    dotnet new classlib `
        --output "$SLN_DIR\$projectName\" `
        --framework $targetFramework

    Remove-Item -Path "$SLN_DIR\$ProjectName\*.cs" -Force

    dotnet add "$SLN_DIR\SitecoreJsonMapper" reference "$SLN_DIR\$projectName"
    dotnet add "$SLN_DIR\$projectName" reference "$SLN_DIR\SitecoreJsonMapper.Common\"
    dotnet add "$SLN_DIR\$projectName" reference "$SLN_DIR\SitecoreJsonMapper.Interface\"
    dotnet sln "$SLN_DIR\SitecoreJsonMapper.sln" add "$SLN_DIR\$projectName"

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
    [Xml]$cprojFile = Get-Content "$SLN_DIR\SitecoreJsonMapper\SitecoreJsonMapper.csproj"
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
        -Path "$SLN_DIR\$ProjectName\Mappings"
    Create-JsonDataSource -ProjectName $ProjectName
    Create-JsonDataTarget -ProjectName $ProjectName

    New-Item `
        -Type Directory `
        -Path "$SLN_DIR\$ProjectName\JsonDataSource"
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
        -Path "$SLN_DIR\$ProjectName\Mappings\JsonDataSource.cs"
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
        -Path "$SLN_DIR\$projectName\Mappings\JsonDataSource.cs" `
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
        -Path "$SLN_DIR\$ProjectName\Mappings\JsonDataTarget.cs"
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
        -Path "$SLN_DIR\$projectName\Mappings\JsonDataTarget.cs" `
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
        -Path "$SLN_DIR\$ProjectName\JsonDataSource\Example.json"
    [string]$exampleJsonDataSource =
@"
{
	"ExampleProperty": "ExampleValue"
}
"@

    Set-Content `
        -Path "$SLN_DIR\$projectName\JsonDataSource\Example.json" `
        -Value "$exampleJsonDataSource"
}

Add-Mapper