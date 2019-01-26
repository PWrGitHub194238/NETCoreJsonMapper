$projectName = Read-Host -Prompt 'Input your .NET Core Library project name'

dotnet sln .\SitecoreJsonMapper.sln remove  ".\$projectName"
Remove-Item -Path ".\$projectName" -Recurse -Force -ErrorAction SilentlyContinue

dotnet new classlib --output ".\$projectName\" --framework netcoreapp2.1
Remove-Item -Path ".\$projectName\*.cs" -Force

dotnet add '.\SitecoreJsonMapper' reference ".\$projectName"
dotnet add ".\$projectName" reference '.\SitecoreJsonMapper.Common\'
dotnet add ".\$projectName" reference '.\SitecoreJsonMapper.Interface\'
dotnet sln . add ".\$projectName"

mkdir ".\$projectName\Mappings" | Out-Null
New-Item -Type File -Name ".\$projectName\Mappings\JsonDataSource.cs" | Out-Null
New-Item -Type File -Name ".\$projectName\Mappings\JsonDataTarget.cs" | Out-Null


mkdir ".\$projectName\JsonDataSource" | Out-Null
New-Item -Type File -Name ".\$projectName\JsonDataSource\Example.json" | Out-Null

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
$jsonDataSourceTemplateClass = $jsonDataSourceTemplateClass.Replace("{0}", "$projectName")

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
$jsonDataTargetTemplateClass = $jsonDataTargetTemplateClass.Replace("{0}", "$projectName")


[string]$exampleJsonDataSource =
@"
{
	"ExampleProperty": "ExampleValue"
}
"@

Set-Content `
    -Path ".\$projectName\Mappings\JsonDataSource.cs" `
    -Value "$jsonDataSourceTemplateClass"

Set-Content `
    -Path ".\$projectName\Mappings\JsonDataTarget.cs" `
    -Value "$jsonDataTargetTemplateClass"

Set-Content `
    -Path ".\$projectName\JsonDataSource\Example.json" `
    -Value "$exampleJsonDataSource"

dotnet build

