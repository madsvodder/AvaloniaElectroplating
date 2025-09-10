using System.Collections.ObjectModel;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class NotesPageViewModel : PageViewModel
{
    public NotesPageViewModel()
    {
        PageName = ApplicationPageNames.Notes;
    }

    public ObservableCollection<Note> NotesCollection { get; } = new();

    [RelayCommand]
    private void CreateNewNote()
    {
        NotesCollection.Add(new Note("Blank note name"));
    }
}