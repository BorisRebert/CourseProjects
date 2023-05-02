namespace DataBaseProject.DBModels
{
    public class User
    {
        public int Id { get; set; }
        public List<Browser> Browsers { get; set; } = new List<Browser>();
        public List<TestData> Tests { get; set; } = new List<TestData>();
        public string? Role { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}