using UnityEngine;

public class CommanderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject commanderPrefab;
    [SerializeField] private Sprite[] commanderSprites;

    public GameObject SpawnCommander(CommanderUnit commander, int spriteIndex)
    {
        if (commanderPrefab == null)
        {
            Debug.LogError("Commander prefab fehlt.");
            return null;
        }

        if (commanderSprites == null || commanderSprites.Length == 0)
        {
            Debug.LogError("Commander sprites fehlen.");
            return null;
        }

        if (spriteIndex < 0 || spriteIndex >= commanderSprites.Length)
        {
            Debug.LogError($"Ungültiger Sprite Index: {spriteIndex}");
            return null;
        }

        GameObject obj = Instantiate(commanderPrefab);
        CommanderView view = obj.GetComponent<CommanderView>();

        if (view == null)
        {
            Debug.LogError("CommanderPrefab hat kein CommanderView.");
            return null;
        }

        view.Initialize(commander, commanderSprites[spriteIndex]);
        return obj;
    }
}