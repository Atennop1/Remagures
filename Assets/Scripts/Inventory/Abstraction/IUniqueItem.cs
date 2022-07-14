public enum UniqueClass
{
    Helmet,
    Chestplate,
    Leggins,
    Weapon
}

public interface IUniqueItem : IBaseItemComponent
{
    public UniqueClass UniqueClass { get; }
}
