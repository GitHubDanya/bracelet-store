using BraceletStore.Api.lib.Queries;
using BraceletStore.Api.Models;

namespace BraceletStore.Api.lib;

public static class ExtensionFunctions
{
    public static string? GetLocalized(this LocalizedRecord name, Language language) =>
        language switch
        {
            Language.Hebrew => name.He,
            Language.Russian => name.Ru,
            _ => name.En
        };

    
}