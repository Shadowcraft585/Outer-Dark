using UnityEngine;

public class SolarSystemRenderer : MonoBehaviour
{

    [SerializeField] private Sprite spaceSprite;
    [SerializeField] private Sprite sunSprite;
    [SerializeField] private Sprite planetSprite;
    [SerializeField] private Sprite asteroidSprite;
    
    [SerializeField] private GameObject tilePrefab;
    private float tileSize;

    private GameObject[,] gridArray;

    public void RenderSolarSystem(SolarSystem system) {
        tileSize = GameManager.Instance.TileSize;
        SystemTile[,] SolarSystemTiles = system.Tiles; // Zugriff auf die Tiles des Sonnensystems, um sie zu rendern
        
        int gridWidth = system.Width;
        int gridHeight = system.Height;

        gridArray = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                SystemTile tile = SolarSystemTiles[x, y];
                if (tile == null) continue; // Überspringe leere Tiles

                Vector2 position = new Vector2(x * tileSize + 0.5f, y * tileSize + 0.5f);
                GameObject tileGO = Instantiate(tilePrefab, position, Quaternion.identity);
                tileGO.name = $"{tile.Type}_{x}_{y}";

                gridArray[x, y] = tileGO;

                SpriteRenderer sr = tileGO.GetComponent<SpriteRenderer>();
                sr.sprite = tile.Type switch
                {
                    TileType.Space => spaceSprite,
                    TileType.Star or TileType.StarCore => sunSprite,
                    TileType.Planet or TileType.PlanetCore => planetSprite,
                    TileType.Asteroid or TileType.AsteroidCore => asteroidSprite,
                    _ => spaceSprite
                };
            }
        }
    }

    // Access a specific cell
    public GameObject GetTileObject(int x, int y)
    {
        if (x < 0 || x >= gridArray.GetLength(0) || 
            y < 0 || y >= gridArray.GetLength(1)) return null;
        return gridArray[x, y];
    }
}
