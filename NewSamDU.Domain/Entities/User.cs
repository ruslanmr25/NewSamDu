using System;
using System.Text.Json.Serialization;
using NewSamDU.Domain.Enums;

namespace NewSamDU.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    [JsonIgnore]
    public string Password { get; set; } = string.Empty;

    public Role Role { get; set; } = Role.User;

    public List<RefreshToken> RefreshTokens { get; set; }
}
