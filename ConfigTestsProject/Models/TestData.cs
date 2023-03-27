namespace ConfigTestsProject.Models
{
    public class TestData
    {
        public string Test { get; set; }

        public TestData(){}
        
        public override string ToString()
        {
            if (Test != null)
            {
                return $", tests = {Test}";
            }

            return null;
        } 
    }
}