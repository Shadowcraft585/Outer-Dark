using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int seed;
    [SerializeField] public float TileSize = 1.0f;
    [SerializeField] private int mapSize = 200; // Standardgröße der Karte, kann im Inspector angepasst werden
    private int mapWidth;
    private int mapHeight;

    public SolarSystem CurrentSystem { get; private set; }

    private SolarSystemRenderer solarSystemRenderer;

    private void Awake()
    {
        seed = Random.Range(int.MinValue, int.MaxValue); // Generiere einen zufälligen Seed für die Weltgenerierung
        mapWidth = mapSize;
        mapHeight = mapSize;
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        solarSystemRenderer = GetComponent<SolarSystemRenderer>();

        SolarSystemGenerator generator = new SolarSystemGenerator();
        CurrentSystem = generator.GenerateSolarSystem(seed, mapWidth, mapHeight);

        solarSystemRenderer.RenderSolarSystem(CurrentSystem);
    }
}