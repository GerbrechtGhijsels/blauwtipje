namespace BlauwtipjeApp.Core.Models.Update
{
    public class StartupCheckList
    {
        public bool HasInternet { get; set; }
        public bool HasDeterminationXmlInstalled { get; set; }
        public bool IsOutdated { get; set; }
    }
}
