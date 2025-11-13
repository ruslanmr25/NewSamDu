using System;

namespace NewSamDU.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expires { get; set; }

    public string CreatedByIp { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
