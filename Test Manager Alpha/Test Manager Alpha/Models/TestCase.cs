namespace Test_Manager_Alpha.Models
{
    public class TestCase
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Step>? Steps { get; set; }
    }
}
