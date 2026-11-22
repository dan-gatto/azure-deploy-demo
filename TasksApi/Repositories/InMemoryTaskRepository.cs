using System.Collections.Concurrent;
using TasksApi.Models;

namespace TasksApi.Repositories;

public interface ITaskRepository
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Add(string title);
}

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly ConcurrentDictionary<int, TaskItem> tasks = new();
    private int nextId;

    public IEnumerable<TaskItem> GetAll() => tasks.Values.OrderBy(t => t.Id);

    public TaskItem? GetById(int id) => tasks.TryGetValue(id, out var task) ? task : null;

    public TaskItem Add(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return new TaskItem();
        }

        var task = new TaskItem { Id = Interlocked.Increment(ref nextId), Title = title };
        
        tasks[task.Id] = task;
        return task;
    }
}
