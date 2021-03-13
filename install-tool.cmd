del /Q .\src\MyCompanyName.MyProjectName\bin
dotnet build -c Release
dotnet pack -c Release
dotnet tool uninstall -g MyCompanyName.MyProjectName
dotnet tool install -g MyCompanyName.MyProjectName --add-source .\src\MyCompanyName.MyProjectName\bin\nupkg
pause