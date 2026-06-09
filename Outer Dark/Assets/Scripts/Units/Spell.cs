using UnityEngine;

public enum Spells { 
    Fireball,
    IceSpike,
    LightningBolt,
    Heal,
    Shield
}

public class Spell
{
    public string Name;
    public SpellType Type;
    public int Damage;
    public int Range;
    public int AreaOfEffect;

    public void Cast(Unit caster, Unit target)
    {
        // Logik hier
        target.Health -= Damage;
    }
}