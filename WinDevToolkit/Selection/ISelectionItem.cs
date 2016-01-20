namespace WinDevToolkit.Selection
{
    public interface ISelectionItem<out T> : ISelectionItem
    {
        T Value { get; }
    }

    public interface ISelectionItem
    {
        string Key { get; }

        bool IsSelected { get; set; }
    }
}
