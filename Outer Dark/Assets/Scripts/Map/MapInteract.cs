using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MapInteract : MonoBehaviour
{
    [SerializeField] private TMP_Text tileInfoText;
    [SerializeField] private GameObject tileInfoPanel;

    [SerializeField] private Sprite highlightedSunSprite;
    [SerializeField] private Sprite highlightedPlanetSprite;
    [SerializeField] private Sprite highlightedAsteroidSprite;
    [SerializeField] private Sprite highlightedSpaceSprite;

    private GameObject selectedTileObject;
    private Sprite originalSprite;
    private SolarSystemRenderer solarSystemRenderer;
    private CommanderController commanderController;
    [SerializeField] private CommanderSidebar commanderSidebar;

    private void Start()
    {
        tileInfoPanel.SetActive(false);
        solarSystemRenderer = GameManager.Instance
            .GetComponent<SolarSystemRenderer>();
        commanderController = GameManager.Instance
            .GetComponent<CommanderController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RestoreSelectedTile();
            tileInfoPanel.SetActive(false);
            commanderController?.ClearSelection();
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        float tileSize = GameManager.Instance.TileSize;

        Vector3 screenPos = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Mathf.Abs(Camera.main.transform.position.z)
        );
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        int x = Mathf.RoundToInt(worldPos.x / tileSize - 0.5f);
        int y = Mathf.RoundToInt(worldPos.y / tileSize - 0.5f);

        SolarSystem system = GameManager.Instance.CurrentSystem;

        if (x < 0 || x >= system.Width || y < 0 || y >= system.Height) return;

        SystemTile tile = system.Tiles[x, y];
        GameObject tileObj = solarSystemRenderer.GetTileObject(x, y);

        if (tileObj == null) return;

        RestoreSelectedTile();

        HighlightTile(tileObj, tile.Type);

        CommanderView selectedCommander = commanderController != null
            ? commanderController.SelectedCommanderView
            : null;

        if (selectedCommander != null && selectedCommander.Data.Position != tile.Position)
        {
            commanderController.MoveSelectedCommanderToward(tile.Position, TerrainType.Plains);
        }

        ShowTileInfo(tile);
    }

    private void HighlightTile(GameObject tileObj, TileType type)
    {
        SpriteRenderer sr = tileObj.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        originalSprite = sr.sprite;
        selectedTileObject = tileObj;

        sr.sprite = type switch
        {
            TileType.Star or TileType.StarCore => highlightedSunSprite,
            TileType.Planet or TileType.PlanetCore => highlightedPlanetSprite,
            TileType.Asteroid or TileType.AsteroidCore => 
                highlightedAsteroidSprite,
            _ => highlightedSpaceSprite
        };
    }

    private void RestoreSelectedTile()
    {
        if (selectedTileObject == null) return;

        SpriteRenderer sr = selectedTileObject.GetComponent<SpriteRenderer>();
        if (sr != null) sr.sprite = originalSprite;

        selectedTileObject = null;
        originalSprite = null;
    }


    private void ShowTileInfo(SystemTile tile)
    {
        tileInfoText.text =
            $"Tile at: ({tile.Position.x}, {tile.Position.y})\n"
            + $"Type: {tile.Type}\n"
            + $"PlanetId: {tile.PlanetId}\n"
            + $"Diameter: {tile.Diameter}";

        commanderSidebar?.ShowCommanders(tile.Position);
        tileInfoPanel.SetActive(true);
    }
}