using UnityEngine;

public class SolarSystemRenderer : MonoBehaviour
{

    [SerializeField] private Material spaceMaterial;
    [SerializeField] private Material sunMaterial;
    [SerializeField] private Material planetMaterial;
    [SerializeField] private Material asteroidMaterial;

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileSize = 1.0f;

    private GameObject[,] gridArray;

    public void RenderSolarSystem(SolarSystem system) {
        SystemTile[,] SolarSystemTiles = system.Tiles; // Zugriff auf die Tiles des Sonnensystems, um sie zu rendern
        
        int gridWidth = system.Width;
        int gridHeight = system.Height;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                SystemTile tile = SolarSystemTiles[x, y];
                if (tile == null) continue; // Überspringe leere Tiles

                Vector2 position = new Vector2(x * tileSize, y * tileSize);
                GameObject tileGO = Instantiate(tilePrefab, position, Quaternion.identity);
                tileGO.name = $"{tile.Type}_{x}_{y}";

                Renderer renderer = tileGO.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = tile.Type switch
                    {
                        TileType.Space => spaceMaterial,
                        TileType.Star or TileType.StarCore => sunMaterial,
                        TileType.Planet or TileType.PlanetCore => planetMaterial,
                        TileType.Asteroid or TileType.AsteroidCore => asteroidMaterial,
                        _ => spaceMaterial
                    };
                }
            }
        }
    }

    // Access a specific cell
    public SystemTile GetTileAt(int x, int y, SolarSystem system)
    {
        if (x >= 0 && x < system.Width && y >= 0 && y < system.Height)
        {
            return system.Tiles[x, y];
        }
        return null;
    }

    public void BuildTileMap()
    {
        SolarSystemGenerator generator = new SolarSystemGenerator();
        SolarSystem newSystem = generator.GenerateSolarSystem(2000, 400, 400);

        RenderSolarSystem(newSystem);
    }
}
