namespace DataBaseProject.DBModels
{
    public class TestData
    { 
        public int Id { get; set; }
        public string? TestName { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }   
}