namespace MyApi.Models
{
  public class AppSetting
  {
    public const string SectionName = "ConnectionStrings";
    public string DefaultConnection { get; set; }
  }
}