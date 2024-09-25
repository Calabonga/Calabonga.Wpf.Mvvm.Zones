using System.Text.Json.Serialization;

namespace Calabonga.Wpf.MvvmMdi.Identity;

/// <summary>
/// This is a Token for deserialization from 
/// </summary>
[Serializable]
public class SecurityToken
{

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}

/// <summary>
/// Server authentication error
/// </summary>
public class SecurityError
{
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("error_description")]
    public string? Description { get; set; }
}