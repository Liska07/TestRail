namespace TestRail.Utils
{
    public class AppSettings
    {
        public string? BrowserType { get; set; }
        public double? TimeOut { get; set; }
        public string? TestRailBaseURL { get; set; }
        public DbSettings DbSettings { get; set; }
    }
}
