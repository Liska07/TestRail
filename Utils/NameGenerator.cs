namespace TestRail.Utils
{
    public static class NameGenerator
    {
        public static string CreateProjectName()
        {
            return "EAntonova " + Guid.NewGuid();
        }

        public static string CreateMilestoneName()
        {
            return "Release " + Guid.NewGuid();
        }
    }
}
