using UnityEngine;
using UnityEngine.UI;

public class CommanderSidebar : MonoBehaviour
{
    [SerializeField] private Transform commanderList;
    [SerializeField] private CommanderEntryUI commanderEntryPrefab;

    private CommanderSpawner commanderSpawner;
    private CommanderController commanderController;
    private CommanderEntryUI sceneTemplate;

    private void Awake()
    {
        ConfigureLayout();
    }

    private void Start()
    {
        ResolveReferences();
    }

    private void ResolveReferences()
    {
        if (commanderSpawner != null && commanderController != null) return;

        GameManager gameManager = GameManager.Instance;
        if (gameManager == null) return;

        commanderSpawner = gameManager.GetComponent<CommanderSpawner>();
        commanderController = gameManager.GetComponent<CommanderController>();

        ConfigureCommanderList();

        if (commanderEntryPrefab == null && commanderList != null)
        {
            sceneTemplate = commanderList.GetComponentInChildren<CommanderEntryUI>(true);
            commanderEntryPrefab = sceneTemplate;
        }

        if (sceneTemplate != null)
        {
            sceneTemplate.gameObject.SetActive(false);
        }
    }

    private void ConfigureCommanderList()
    {
        if (commanderList == null) return;

        RectTransform listRect = commanderList as RectTransform;
        if (listRect == null) return;

        listRect.anchorMin = new Vector2(0.2f, 0.02f);
        listRect.anchorMax = new Vector2(0.92f, 0.28f);
        listRect.offsetMin = Vector2.zero;
        listRect.offsetMax = Vector2.zero;

        VerticalLayoutGroup layout = commanderList.GetComponent<VerticalLayoutGroup>();
        if (layout == null)
        {
            layout = commanderList.gameObject.AddComponent<VerticalLayoutGroup>();
        }

        layout.spacing = 8f;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
    }

    private void ConfigureLayout()
    {
        RectTransform sidebarRect = GetComponent<RectTransform>();
        if (sidebarRect == null) return;

        sidebarRect.anchorMin = new Vector2(0.65f, 0f);
        sidebarRect.anchorMax = new Vector2(1f, 1f);
        sidebarRect.offsetMin = Vector2.zero;
        sidebarRect.offsetMax = Vector2.zero;
        sidebarRect.localScale = Vector3.one;

        Image sidebarImage = GetComponent<Image>();
        if (sidebarImage != null)
        {
            sidebarImage.color = new Color(1f, 1f, 1f, 0f);
            sidebarImage.raycastTarget = false;
        }
    }

    public void ShowCommanders(Vector2Int tilePosition)
    {
        ResolveReferences();
        ClearList();

        if (commanderSpawner == null || commanderController == null ||
            commanderEntryPrefab == null) return;

        foreach (CommanderView commanderView in commanderSpawner.SpawnedCommanders)
        {
            if (commanderView == null || commanderView.Data == null ||
                commanderView.Data.Position != tilePosition) continue;

            CommanderView selectedCommander = commanderView;
            CommanderEntryUI entry = Instantiate(commanderEntryPrefab, commanderList);
            entry.gameObject.SetActive(true);
            entry.Setup(selectedCommander, () =>
                commanderController.SelectCommander(selectedCommander));
        }
    }

    public void ClearList()
    {
        if (commanderList == null) return;

        for (int i = commanderList.childCount - 1; i >= 0; i--)
        {
            GameObject child = commanderList.GetChild(i).gameObject;
            if (sceneTemplate != null && child == sceneTemplate.gameObject) continue;

            Destroy(child);
        }
    }
}