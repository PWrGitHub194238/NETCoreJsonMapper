. $PSScriptRoot\Remove-Mapper.ps1

[string]$SLN_DIR = Resolve-Path -Path "$PSScriptRoot\..\NETCoreJsonMapper"

<#
    .SYNOPSIS
		Add a new .NET Core project to the existing solution.
    .DESCRIPTION
		Add a new .NET Core project to the existing solution. Each projects consists of a single
		JsonDataSource class that contains a .NET representation of a JSON formatted string 
		in a example *.json file (also generated by this cmdlet, amed Example.json in a 'JsonDataSource' directory) 
		and JsonDataTarget class that represents a .NET object to be serialised to target *-result.json file.
#>
function global:Add-Mapper
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
    )
    [string]$projectName = Read-Host -Prompt 'Input your .NET Core Library project name'
	[string]$targetFramework = Get-ProjectTargetFramework

    Remove-Mapper -ProjectName "$projectName" -RemoveOnly | Out-Null
    dotnet new classlib `
		--name "$projectName" `
        --output "$SLN_DIR\$projectName\" `
        --framework $targetFramework

    Remove-Item -Path "$SLN_DIR\$ProjectName\*.cs" -Force

    dotnet add "$SLN_DIR\NETCoreJsonMapper" reference "$SLN_DIR\$projectName"
    dotnet add "$SLN_DIR\$projectName" reference "$SLN_DIR\NETCoreJsonMapper.Commons\NETCoreJsonMapper.Commons.csproj"
    dotnet add "$SLN_DIR\$projectName" reference "$SLN_DIR\NETCoreJsonMapper.Interfaces\NETCoreJsonMapper.Interfaces.csproj"
    dotnet sln "$SLN_DIR\NETCoreJsonMapper.sln" add "$SLN_DIR\$projectName"

    Create-ProjectFiles -ProjectName $projectName

    dotnet build $SLN_DIR
}

<#
    .SYNOPSIS
		Read from a *.csproj file a target framework for the entire solution.
    .DESCRIPTION
		Read from the main project's (NETCoreJsonMapper) *.csproj file a target framework for the entire solution.		
#>
function local:Get-ProjectTargetFramework
{
    [CmdletBinding(PositionalBinding = $false)]
    [OutputType([void])]
    param (
    )
    [Xml]$cprojFile = Get-Content "$SLN_DIR\NETCoreJsonMapper\NETCoreJsonMapper.csproj"
    [Xml.XmlNode]$targetFrameworkNode = $cprojFile.SelectSingleNode('/Project/PropertyGroup/TargetFramework')
    $targetFrameworkNode.InnerText
}

<#
    .SYNOPSIS
		Generates a set of initial files for a new project named $ProjectName.
    .DESCRIPTION
		Given a project name $ProjectName, this cmdlet will generate following files:
		- .\Mappings\JsonDataSource.cs,
		- .\Mappings\JsonDataTarget.cs,
		- .\JsonDataSource\Example.json.
    .PARAMETER ProjectName
		The name of a project that generated files will be added to.
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
        -Path "$SLN_DIR\$ProjectName\Mappings" | Out-Null
    Create-JsonDataSource -ProjectName $ProjectName
    Create-JsonDataTarget -ProjectName $ProjectName

    New-Item `
        -Type Directory `
        -Path "$SLN_DIR\$ProjectName\JsonDataSource" | Out-Null
    Create-ExampleJson -ProjectName $ProjectName
 }

 <#
    .SYNOPSIS
		Create a JsonDataSource.cs file.
    .DESCRIPTION
		Create a JsonDataSource.cs file containing a .NET representation of a JSON formatted string in a example *.json file.
    .PARAMETER ProjectName
		The name of a project that generated files will be added to.
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
        -Path "$SLN_DIR\$ProjectName\Mappings\JsonDataSource.cs" | Out-Null
    [string]$jsonDataSourceTemplateClass =
@"
using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;

namespace {0}.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON formatted file ./JsonDataSource/Example.json.
    /// Along with this class, a JsonDataTarget was generated.
    /// 
    /// All data loaded from the JSON file Example.json file:
    /// 
    /// {
    /// 	"ExampleProperty": "ExampleValue",
    /// }
    /// 
    /// will be saved in the class properties whose name corresponds to the key names 
    /// and object types from the specified file.
    /// 
    /// To make the class visible for processing, it has to extend an AJsonDataSource<JsonDataTarget>
    /// class, where JsonDataTarget points the type of an object that represents 
    /// the result JSON structure in a form of a class.
    /// </summary>
    public class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
		[JsonProperty()]
        public string ExampleProperty { get; set; }
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
		Create a JsonDataTarget.cs file.
    .DESCRIPTION
		Create a JsonDataTarget.cs file containing a .NET representation of a result *.json file to be generated.
    .PARAMETER ProjectName
		The name of a project that generated files will be added to.
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
        -Path "$SLN_DIR\$ProjectName\Mappings\JsonDataTarget.cs" | Out-Null
    [string]$jsonDataTargetTemplateClass =
@"
using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;

namespace {0}.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON file to be generated out of this class' instance.
    /// Along with this class, a JsonDataSource was generated.
    /// 
    /// Example string that will be generated ased on this class:
    /// 
    /// {
    /// 	"Example-Property": "ExampleValue-generated",
    /// }
    /// 
    /// Each of the resulted JSON keys has a value 
    /// of the public property of this class enhanced by the proper JSON attribute.
    /// A data source for all the values generated is a JsonDataSource class.
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Example-Property")]
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
		Create an Example.json file.
    .DESCRIPTION
		Create an Example.json file with a simple JSON formatted string.
    .PARAMETER ProjectName
		The name of a project that generated files will be added to.
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
        -Path "$SLN_DIR\$ProjectName\JsonDataSource\Example.json" | Out-Null
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