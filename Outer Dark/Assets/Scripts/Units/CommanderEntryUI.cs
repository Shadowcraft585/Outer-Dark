using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommanderEntryUI : MonoBehaviour
{
    [SerializeField] private Image commanderImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text movementText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text armyText;

    private Button button;
    private TMP_Text runtimeInfoText;

    private void Awake()
    {
        button = GetComponent<Button>();

        LayoutElement layoutElement = GetComponent<LayoutElement>();
        if (layoutElement == null)
        {
            layoutElement = gameObject.AddComponent<LayoutElement>();
        }

        layoutElement.minHeight = 140f;
        layoutElement.preferredHeight = 140f;
        layoutElement.flexibleHeight = 0f;

        if (commanderImage == null)
        {
            commanderImage = transform.Find("CommanderSprite")?.GetComponent<Image>();
        }

        if (nameText == null)
        {
            nameText = transform.Find("CommanderInfo/NameText")?.GetComponent<TMP_Text>();
        }

        if (movementText == null)
        {
            movementText = transform.Find("CommanderInfo/MovementText")?.GetComponent<TMP_Text>();
        }

        if (healthText == null)
        {
            healthText = transform.Find("CommanderInfo/HealthText")?.GetComponent<TMP_Text>();
        }

        if (armyText == null)
        {
            armyText = transform.Find("CommanderInfo/ArmyText")?.GetComponent<TMP_Text>();
        }

        if (nameText == null) nameText = GetComponentInChildren<TMP_Text>(true);
        if (movementText == null) movementText = nameText;
        if (healthText == null) healthText = nameText;
        if (armyText == null) armyText = nameText;

        ConfigureEntryLayout();
        HideDefaultButtonText();
        CreateRuntimeInfoText();
    }

    private void CreateRuntimeInfoText()
    {
        GameObject infoObject = new GameObject("RuntimeCommanderInfo",
            typeof(RectTransform), typeof(TextMeshProUGUI));
        infoObject.transform.SetParent(transform, false);

        RectTransform infoRect = infoObject.GetComponent<RectTransform>();
        infoRect.anchorMin = new Vector2(0.34f, 0.06f);
        infoRect.anchorMax = new Vector2(0.98f, 0.94f);
        infoRect.offsetMin = new Vector2(8f, 0f);
        infoRect.offsetMax = new Vector2(-8f, 0f);

        runtimeInfoText = infoObject.GetComponent<TextMeshProUGUI>();
        runtimeInfoText.color = new Color32(35, 35, 35, 255);
        runtimeInfoText.fontSize = 22f;
        runtimeInfoText.alignment = TextAlignmentOptions.MidlineLeft;
        runtimeInfoText.enableWordWrapping = false;
        runtimeInfoText.raycastTarget = false;

        TMP_Text[] oldTexts = GetComponentsInChildren<TMP_Text>(true);
        foreach (TMP_Text oldText in oldTexts)
        {
            if (oldText != runtimeInfoText)
            {
                oldText.gameObject.SetActive(false);
            }
        }
    }

    private void HideDefaultButtonText()
    {
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>(true);
        foreach (TMP_Text text in texts)
        {
            if (text == nameText || text == movementText ||
                text == healthText || text == armyText) continue;

            if (text.text == "Button")
            {
                text.gameObject.SetActive(false);
            }
        }
    }

    private void ConfigureEntryLayout()
    {
        RectTransform entryRect = GetComponent<RectTransform>();
        entryRect.sizeDelta = new Vector2(360f, 180f);

        RectTransform spriteRect = transform.Find("CommanderSprite") as RectTransform;
        if (spriteRect != null)
        {
            spriteRect.anchorMin = new Vector2(0.03f, 0.12f);
            spriteRect.anchorMax = new Vector2(0.3f, 0.88f);
            spriteRect.offsetMin = Vector2.zero;
            spriteRect.offsetMax = Vector2.zero;
        }

        RectTransform infoRect = transform.Find("CommanderInfo") as RectTransform;
        if (infoRect != null)
        {
            infoRect.anchorMin = new Vector2(0.34f, 0.05f);
            infoRect.anchorMax = new Vector2(0.98f, 0.95f);
            infoRect.offsetMin = Vector2.zero;
            infoRect.offsetMax = Vector2.zero;
        }

        SetTextLayout(nameText, 0.75f, 0.95f);
        SetTextLayout(movementText, 0.5f, 0.75f);
        SetTextLayout(healthText, 0.25f, 0.5f);
        SetTextLayout(armyText, 0f, 0.25f);
    }

    private void SetTextLayout(TMP_Text text, float yMin, float yMax)
    {
        if (text == null) return;

        RectTransform textRect = text.rectTransform;
        textRect.anchorMin = new Vector2(0f, yMin);
        textRect.anchorMax = new Vector2(1f, yMax);
        textRect.offsetMin = new Vector2(8f, 0f);
        textRect.offsetMax = new Vector2(-4f, 0f);

        text.color = new Color32(35, 35, 35, 255);
        text.fontSize = 22f;
        text.alignment = TextAlignmentOptions.MidlineLeft;
        text.enableWordWrapping = false;
        text.raycastTarget = false;
    }

    public void Setup(CommanderView commanderView, UnityAction onClicked)
    {
        if (commanderView == null || commanderView.Data == null) return;

        commanderImage.sprite = commanderView.Sprite;
        runtimeInfoText.text = $"{commanderView.Data.Name}\n"
            + $"Ausdauer: {commanderView.Data.CurrentMovementPoints} / "
            + $"{commanderView.Data.MaxMovementPoints}\n"
            + $"Leben: {commanderView.Data.CurrentHealth} / "
            + $"{commanderView.Data.MaxHealth}\n"
            + $"Truppen: {commanderView.Data.Army.Count}";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onClicked);
    }
}