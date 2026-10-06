public class TaskItem
{
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }

    public TaskItem()
    {
    }

    public TaskItem(string title)
    {
        Title = title;
        IsCompleted = false;
    }
}