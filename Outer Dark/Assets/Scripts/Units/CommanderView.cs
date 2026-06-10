using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CommanderView : MonoBehaviour
{
    public CommanderUnit Data { get; private set; }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(CommanderUnit commander, Sprite sprite)
    {
        Data = commander;
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = 1;
        SyncPosition();
    }

    public void SyncPosition()
    {
        float tileSize = GameManager.Instance != null
            ? GameManager.Instance.TileSize
            : 1f;

        transform.position = new Vector3(
            Data.Position.x * tileSize + 0.5f,
            Data.Position.y * tileSize + 0.5f,
            0f
        );
    }
}