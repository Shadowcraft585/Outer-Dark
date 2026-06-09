using UnityEngine;
using System.Collections.Generic;

public class GameplayLoop { 
    public int CurrentTurn;
    public List<Faction> Factions;
    public Faction CurrentFaction;
    public void EndTurn()
    {
        CurrentTurn++;

        if (Factions == null || Factions.Count == 0) return;

        int currentIndex = Factions.IndexOf(CurrentFaction);
        CurrentFaction = Factions[(currentIndex + 1) % Factions.Count];
    }
}

public class Faction { 
    public string Name;
    public Dictionary<ResourceType, int> Resources;
    public List<Unit> Units;
    public HashSet<Vector2Int> OwnedTiles;
}

public enum ResourceType { 
    Gold,
    Iron
}