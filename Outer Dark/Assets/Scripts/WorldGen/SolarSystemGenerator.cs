using UnityEngine;

public class SolarSystemGenerator
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Perlin Noise könnte hier verwendet werden, um die Verteilung von Planeten und Asteroiden zu steuern
    //generiert ein einfaches Sonnensystem mit einem Stern in der Mitte und zufällig verteilten Planeten und Asteroiden
    public SolarSystem GenerateSolarSystem(int seed, int width, int height) {


        int StarDiameter = width / 4; // Beispielgröße für den Stern, könnte auch variieren

        // Zufällige Verteilung basierend auf Seed
        Random.InitState(seed);

        int maxNumPlanets = Random.Range(5, 10); // maximale Anzahl von Planeten
        int numPlanets = 0;
        int maxNumAsteroids = Random.Range(20, 30); // maximale Anzahl von Asteroiden
        int numAsteroids = 0;


        SolarSystem system = new SolarSystem();
        system.Width = width;
        system.Height = height;
        system.Tiles = new SystemTile[width, height];

        // Alle Tiles initialisieren als Space
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                system.Tiles[x, y] = new SystemTile {
                    Position = new Vector2Int(x, y),
                    Type = TileType.Space
                };
            }
        }

        // Star in der Mitte platzieren
        system.StarPosition = new Vector2Int(width / 2, height / 2);
        SystemTile Star = system.Tiles[system.StarPosition.x, system.StarPosition.y] = new SystemTile {
            Position = system.StarPosition,
            Type = TileType.StarCore,
            Diameter = StarDiameter, // Beispielgröße für den Stern
            PlanetId = "Sun"
        };
        BuildPlanetAroundCore(system, Star); // Baut den Stern um den Core herum auf

        // Planeten und Asteroiden Mitte zufällig generieren
        for (int i = 0; i < width * height; i++) {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            // Erstelle Planeten/Asteroiden nur außerhalb eines zentralen Bereichs um den Stern
            // if trifft nur ein, wenn x entweder größer als (die Mitte + Diameter) des Sterns oder kleiner als (die Mitte - Diameter des Sterns) ist, und das gleiche gilt für y
            // Somit können Planeten/Asteroiden minimal (Diameter des Sterns) Einheiten vom Stern entfernt sein, aber nicht näher
            // z.B. bei einem Stern mit Diameter 20 können Planeten/Asteroiden nicht näher als 20 Einheiten zum Stern platziert werden
            if (system.Tiles[x, y].Type == TileType.Space && 
            (x < ((width / 2) - StarDiameter) || 
            x > ((width / 2) + StarDiameter)) || 
            y < ((height / 2) - StarDiameter) || 
            y > ((height / 2) + StarDiameter)) {
                TileType type = (Random.value < 0.2f) ? TileType.PlanetCore : TileType.AsteroidCore;
                int diameter = Random.Range(width / 10, width / 6);

                if (type == TileType.PlanetCore) {
                    if (numPlanets >= maxNumPlanets) {
                        continue; // Überspringe, wenn die maximale Anzahl von Planeten erreicht ist
                    }
                    diameter = Random.Range(width / 10, width / 6); // Beispielgröße für Planeten
                } else {
                    if (numAsteroids >= maxNumAsteroids) {
                        continue; // Überspringe, wenn die maximale Anzahl von Asteroiden erreicht ist
                    }
                    diameter = Random.Range(width / 50, width / 25); // Beispielgröße für Asteroiden
                }

                SystemTile candidate = new SystemTile {
                    Position = new Vector2Int(x, y),
                    Type = type,
                    Diameter = diameter,
                    PlanetId = (type == TileType.PlanetCore)
                        ? "PlanetCore_" + x + "_" + y
                        : "AsteroidCore_" + x + "_" + y
                };

                if (!CheckIfCoreHasEnoughSpace(system, candidate))
                    continue;

                system.Tiles[x, y] = candidate;
                BuildPlanetAroundCore(system, system.Tiles[x, y]);

                if (type == TileType.PlanetCore) numPlanets++;
                else numAsteroids++;
            }
        }

        return system;
    }

    // Baut den Planeten oder Asteroiden um den Core herum auf, basierend auf dem Durchmesser
    void BuildPlanetAroundCore(SolarSystem system, SystemTile core) {
        int radius = core.Diameter / 2;
        int cx = core.Position.x;
        int cy = core.Position.y;

        TileType surfaceType = core.Type == TileType.StarCore
            ? TileType.Star
            : core.Type == TileType.PlanetCore
                ? TileType.Planet
                : TileType.Asteroid;

        for (int x = cx - radius; x <= cx + radius; x++) {
            for (int y = cy - radius; y <= cy + radius; y++) {
                // Bounds-Check
                if (x < 0 || x >= system.Width || y < 0 || y >= system.Height)
                    continue;

                // Berechne die Entfernung zum Core, um eine runde Form zu gewährleisten
                float dist = Vector2.Distance(new Vector2(cx, cy), new Vector2(x, y));

                // Überspringe den Core selbst
                if (x == cx && y == cy)
                    continue;

                // Setze den Tile-Typ basierend auf der Entfernung zum Core, um eine runde Form zu gewährleisten
                if (dist <= radius) {
                    system.Tiles[x, y] = new SystemTile {
                        Position = new Vector2Int(x, y),
                        Type = surfaceType,
                        PlanetId = core.PlanetId
                    };
                }
            }
        }
    }

    // Überprüft, ob der Planet/Asteroid genügend Platz hat, um vollständig platziert zu werden, ohne andere Objekte zu überlappen
    bool CheckIfCoreHasEnoughSpace(SolarSystem system, SystemTile core) 
    {
        int radius = core.Diameter / 2;
        int cx = core.Position.x;
        int cy = core.Position.y;

        // Prüfe ob der Planet komplett innerhalb der Grid-Grenzen liegt
        if (cx - radius < 0 || cx + radius >= system.Width ||
            cy - radius < 0 || cy + radius >= system.Height) {
            return false;
        }

        // Prüfe ob alle Tiles im Bereich des Planeten noch Space sind
        for (int x = cx - radius; x <= cx + radius; x++) {
            for (int y = cy - radius; y <= cy + radius; y++) {
                float dist = Vector2.Distance(new Vector2(cx, cy), new Vector2(x, y));
                if (dist <= radius && system.Tiles[x, y].Type != TileType.Space) {
                    return false;
                }
            }
        }

        return true;
    }
}
