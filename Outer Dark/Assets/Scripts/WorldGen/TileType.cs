using UnityEngine;

public enum TileType { Space, Planet, Asteroid, Star, Station, StarCore, PlanetCore, AsteroidCore }

public class SystemTile {
    public Vector2Int Position;
    public TileType Type;
    public int Diameter;          // für Planeten/Asteroiden
    public string PlanetId;   // Referenz auf detaillierte Planet-Daten
}

public class SolarSystem {
    public int Width, Height;
    public SystemTile[,] Tiles;
    public Vector2Int StarPosition;
}