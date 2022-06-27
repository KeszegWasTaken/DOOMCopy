public class Quest
{
    private int total;
    private int progress;
    private bool isComplete;
    private string source;
    private string target;

    public int Total
    {
        get => total;
        set => total = value;
    }

    public int Progress
    {
        get => progress;
        set => progress = value;
    }

    public bool IsComplete
    {
        get => isComplete;
        set => isComplete = value;
    }

    public string Source
    {
        get => source;
        set => source = value;
    }

    public string Target
    {
        get => target;
        set => target = value;
    }

    public Quest(int total, bool isComplete, string source, string target)
    {
        this.total = total;
        this.progress = 0;
        this.isComplete = isComplete;
        this.source = source;
        this.target = target;
    }
}
