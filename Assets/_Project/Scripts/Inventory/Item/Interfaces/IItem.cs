public interface IItem : IToolTip
{
    string Id { get; }

    void Use();

    int MaxStackSize { get; }
}