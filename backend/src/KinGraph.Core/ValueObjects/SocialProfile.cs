namespace KinGraph.Core.ValueObjects;

public class SocialProfile(SocialMediaPlatform platform, string username) : ValueObject
{
    public SocialMediaPlatform Platform { get; private set; } = platform;
    public string Username { get; private set; } = username;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Platform;
        yield return Username;
    }

    public override string ToString()
    {
        return $"{Platform}: {Username}";
    }
}
