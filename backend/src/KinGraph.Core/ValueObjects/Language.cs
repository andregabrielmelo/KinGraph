using Ardalis.SmartEnum;

namespace KinGraph.Core.ValueObjects;

public sealed class Language : SmartEnum<Language>
{
    public string Code { get; }

    private Language(string name, int value, string code)
        : base(name, value)
    {
        Code = code.ToLowerInvariant();
    }

    // Common languages (expand as needed)
    public static readonly Language English = new(nameof(English), 1, "en");

    public static readonly Language Portuguese = new(nameof(Portuguese), 2, "pt");

    public static readonly Language PortugueseBrazil = new(nameof(PortugueseBrazil), 3, "pt-BR");

    public static readonly Language Spanish = new(nameof(Spanish), 4, "es");

    public static readonly Language French = new(nameof(French), 5, "fr");

    public static readonly Language German = new(nameof(German), 6, "de");

    // Lookup by ISO code
    public static Language FromCode(string code)
    {
        var normalized = code.Trim().ToLowerInvariant();

        return List.FirstOrDefault(l => l.Code == normalized)
            ?? throw new ArgumentException($"Invalid language code: {code}");
    }

    public override string ToString() => $"{Name} ({Code})";
}
