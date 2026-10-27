using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace notatnik;

public partial class MainWindow : Window
{
    private readonly NotesRepository _notesRepository = new();

    public ObservableCollection<Note> Notes { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        LoadNotes();
    }

    private void SaveNote_Click(object? sender, RoutedEventArgs e)
    {
        if (!NoteValidator.TryCreateNote(TitleInput.Text, ContentInput.Text, out var note, out var error))
        {
            SetStatus(error, isError: true);
            return;
        }

        var updatedNotes = new List<Note> { note };
        updatedNotes.AddRange(Notes);

        try
        {
            _notesRepository.Save(updatedNotes);
        }
        catch (Exception exception) when (IsNotesFileException(exception))
        {
            SetStatus($"Nie udało się zapisać notatki: {exception.Message}", isError: true);
            return;
        }

        Notes.Insert(0, note);
        TitleInput.Clear();
        ContentInput.Clear();
        UpdateNoteCount();
        SetStatus("Notatka została zapisana.", isError: false);
    }

    private void NotesList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (NotesList.SelectedItem is Note note)
        {
            SelectedNoteContent.Text = note.Content;
            SelectedNoteViewer.IsVisible = true;
            SelectNoteHint.IsVisible = false;
        }
        else
        {
            SelectedNoteContent.Text = string.Empty;
            SelectedNoteViewer.IsVisible = false;
            SelectNoteHint.IsVisible = true;
        }
    }

    private void LoadNotes()
    {
        try
        {
            var savedNotes = _notesRepository.Load();
            foreach (var note in savedNotes)
            {
                Notes.Add(note);
            }

            UpdateNoteCount();
        }
        catch (Exception exception) when (IsNotesFileException(exception))
        {
            UpdateNoteCount();
            SetStatus($"Nie udało się wczytać notatek: {exception.Message}", isError: true);
        }
    }

    private static bool IsNotesFileException(Exception exception)
    {
        return exception is IOException
            or UnauthorizedAccessException
            or System.Security.SecurityException
            or System.Text.Json.JsonException
            or InvalidDataException;
    }

    private void UpdateNoteCount()
    {
        NoteCount.Text = Notes.Count.ToString();
        EmptyNotes.IsVisible = Notes.Count == 0;
    }

    private void SetStatus(string message, bool isError)
    {
        StatusText.Text = message;
        StatusText.Foreground = Avalonia.Media.Brush.Parse(isError ? "#B42318" : "#21805A");
    }
}
