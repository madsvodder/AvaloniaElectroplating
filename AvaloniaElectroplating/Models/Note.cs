namespace AvaloniaElectroplating.Models;

public class Note
{
    public string Content = "";
    public string NoteName { get; } = "123N";

    public Note(string name)
    {
        NoteName = name;
    }
}