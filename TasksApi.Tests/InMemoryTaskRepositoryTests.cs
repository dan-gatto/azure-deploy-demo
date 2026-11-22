using TasksApi.Repositories;
namespace TasksApi.Tests;

public class InMemoryTaskRepositoryTests
{
    private InMemoryTaskRepository r = null;

    [SetUp]
    public void SetUp()
    {
        r = new InMemoryTaskRepository();
    }

    [TestCase("")]
    [TestCase(" ")]    
    public void BlankTitleReturnsEmptyTask(string title)
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
