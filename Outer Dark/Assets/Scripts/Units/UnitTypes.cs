
using UnityEngine;
using System.Collections.Generic;

public abstract class Unit { 
    public string Name;
    public int Health;
    public int Attack;
    public Vector2Int Position;
    public int Armor;
    public int MagicResistance;
    public int MovementRange;
    public void Move(Vector2Int newPosition)
    {
        Position = newPosition;
    }
}

public class Spellcaster : Unit { 
    public List<Spells> Spells;
}

public class Ranged : Unit { 
    public int Range;
}

public class Melee : Unit { }

public class Commander : Unit { 
    public List<Unit> Subordinates;
    //public List<Ability> Abilities;
    public void MoveSubordinates(Vector2Int newPosition)
    {
        foreach (Unit subordinate in Subordinates)
        {
            subordinate.Move(newPosition);
        }
    }
}