using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Gold,
    Iron
}

public enum UnitCategory
{
    Melee,
    Ranged,
    Spellcaster
}

public class GameplayLoop
{
    public int CurrentTurn;
    public List<Faction> Factions = new();
    public Faction CurrentFaction;

    public void EndTurn()
    {
        if (Factions == null || Factions.Count == 0) return;

        int currentIndex = Factions.IndexOf(CurrentFaction);
        CurrentFaction = Factions[(currentIndex + 1) % Factions.Count];

        if (currentIndex == Factions.Count - 1)
        {
            CurrentTurn++;
        }
    }
}

public class Faction
{
    public string Name;
    public Dictionary<ResourceType, int> Resources = new();
    public List<CommanderUnit> Commanders = new();
    public HashSet<Vector2Int> OwnedTiles = new();
}

public abstract class StackUnit
{
    public string Name;
    public int MaxHealth;
    public int CurrentHealth;
    public int Attack;
    public int Armor;
    public int MagicResistance;
    public UnitCategory Category;
    public Vector2Int Position;
    public Faction belongsTo;
}

public class MeleeUnit : StackUnit
{
}

public class RangedUnit : StackUnit
{
    public int Range;
}

public class SpellcasterUnit : StackUnit
{
    public List<string> Spells = new();
}

public class CommanderUnit
{
    public string Name;
    public int MaxHealth;
    public int CurrentHealth;
    public int Attack;
    public int Armor;
    public int MagicResistance;

    public Vector2Int Position;

    public int MaxMovementPoints;
    public int CurrentMovementPoints;

    public List<StackUnit> Army = new();

    public void Move(Vector2Int newPosition, int movementCost)
    {
        if (movementCost > CurrentMovementPoints) return;

        Position = newPosition;
        CurrentMovementPoints -= movementCost;
    }

    public void AddUnit(StackUnit unit)
    {
        Army.Add(unit);
    }

    public void RemoveUnit(StackUnit unit)
    {
        Army.Remove(unit);
    }

    public void StartNewTurn()
    {
        CurrentMovementPoints = MaxMovementPoints;
    }
}