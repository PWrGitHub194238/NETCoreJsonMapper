dotnet clean
dotnet restore
dotnet build
dotnet publish "$PSScriptRoot\NETCoreJsonMapper" `
    --configuration Release `
    --output "$PSScriptRoot\publish"