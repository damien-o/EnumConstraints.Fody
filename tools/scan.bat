dotnet 

@REM dotnet tool install -g dotnet-coverage 
dotnet-coverage collect dotnet test --verbosity normal ../tests/EnumConstraints.Fody.Tests/EnumConstraints.Fody.Tests.csproj


@REM dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet-coverage merge -f cobertura ./output.coverage
reportgenerator -reports:./output.cobertura.xml -targetdir:./CodeCoverage -reporttypes:"Cobertura;MarkdownSummaryGithub" -verbosity Off -assemblyfilters:"+EnumConstraints.Fody;+EnumConstraints"

