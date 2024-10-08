namespace Explorator.Models;

public class FileInformation
{
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Size { get; set; }
    public string? Type { get; set; }
    public DateTime LastModified { get; set; }
    public string? HashMD5 { get; set; }
    public string? HashSHA1 { get; set; }
    public string? HashSHA256 { get; set; }
}