using System;
using System.Diagnostics.CodeAnalysis;

namespace notatnik;

public static class NoteValidator
{
    public static bool TryCreateNote(
        string? title,
        string? content,
        [NotNullWhen(true)] out Note? note,
        out string error)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
        {
            note = null;
            error = "Uzupełnij tytuł i treść notatki.";
            return false;
        }

        note = new Note
        {
            Title = title.Trim(),
            Content = content.Trim(),
            CreatedAt = DateTime.Now
        };
        error = string.Empty;
        return true;
    }
}
