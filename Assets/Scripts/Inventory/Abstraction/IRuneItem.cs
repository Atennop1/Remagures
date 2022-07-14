using System.Collections.Generic;

public enum RuneType
{
    Fire,
    Mana,
    Shield
}

public interface IRuneItem : IBaseItemComponent
{
    public ClassStat ClassStat { get; }
    public RuneType RuneType { get; }
}
