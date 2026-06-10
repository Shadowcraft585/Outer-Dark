using UnityEngine;

public enum SpellTType { 
    Fireball,
    IceSpike,
    LightningBolt,
    Heal,
    Shield
}

public class Spell
{
    public string Name;
    public int Damage;
    public int Range;
    public int AreaOfEffect;
}