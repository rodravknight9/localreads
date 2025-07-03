namespace LocalReads.Shared.Domain;

public class ProfilePicture
{
    public int Id { get; set; }
    public byte[] Data { get; set; }
    public string FileExtension { get; set; }
    public decimal Size { get; set; }
}
