using TasksApi.Repositories;
namespace TasksApi.Tests;

public class Tests
{
    private InMemoryTaskRepository r = new();

    [SetUp]
    public void SetUp()
    {        
    }

    [TestCase("")]
    [TestCase(" ")]    
    public void NullReturned(string title)
    {
        var result = r.Add(title);
        
        Assert.Multiple(() => 
        {
            Assert.That(result.Id == 0);
            Assert.That(string.IsNullOrEmpty(result.Title));            
            Assert.That(result.IsDone == false);
        });
    }
}
