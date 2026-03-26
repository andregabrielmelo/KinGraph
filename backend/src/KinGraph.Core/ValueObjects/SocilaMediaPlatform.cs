using Ardalis.SmartEnum;

namespace KinGraph.Core.ValueObjects;

public sealed class SocialMediaPlatform : SmartEnum<SocialMediaPlatform>
{
    public string BaseUrl { get; }

    private SocialMediaPlatform(string name, int value, string baseUrl)
        : base(name, value)
    {
        BaseUrl = baseUrl;
    }

    public static readonly SocialMediaPlatform Instagram = new(
        nameof(Instagram),
        1,
        "https://instagram.com/"
    );

    public static readonly SocialMediaPlatform Twitter = new(
        nameof(Twitter),
        2,
        "https://twitter.com/"
    );

    public static readonly SocialMediaPlatform LinkedIn = new(
        nameof(LinkedIn),
        3,
        "https://linkedin.com/in/"
    );

    public static readonly SocialMediaPlatform Facebook = new(
        nameof(Facebook),
        4,
        "https://facebook.com/"
    );

    public string BuildProfileUrl(string username) => $"{BaseUrl}{username}";

    public override string ToString() => Name;
}
