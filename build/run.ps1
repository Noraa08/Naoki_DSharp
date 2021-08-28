function Run-Bot
{
    dotnet.exe run --project ..\src\TemplateBot\TemplateBot.csproj `
                   --configuration Release;
}

Run-Bot;