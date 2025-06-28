using System.ComponentModel.DataAnnotations;

public class AD_User
{
    [Key]
    [MaxLength(64)]
    public string Login_Name { get; set; } = string.Empty;

    [Range(0, 99)]
    public int Authority_Level { get; set; }
}