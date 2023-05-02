namespace DataBaseProject.DBModels
{
    public class Browser
    {
        public int Id { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public string? BrowserName { get; set; }
    }   
}