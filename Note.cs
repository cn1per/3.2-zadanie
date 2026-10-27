using System;
using System.Globalization;

namespace notatnik;

public sealed class Note
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string DateLabel => CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
}
