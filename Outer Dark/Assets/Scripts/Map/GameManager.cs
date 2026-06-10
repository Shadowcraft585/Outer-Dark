using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int seed;
    [SerializeField] public float TileSize = 1.0f;
    [SerializeField] private int mapSize = 200;

    private int mapWidth;
    private int mapHeight;

    public SolarSystem CurrentSystem { get; private set; }
    public GameplayLoop GameplayLoop { get; private set; }

    private SolarSystemRenderer solarSystemRenderer;
    private CommanderSpawner commanderSpawner;
    private CommanderController commanderController;

    private void Awake()
    {
        seed = Random.Range(int.MinValue, int.MaxValue);
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
        commanderSpawner = GetComponent<CommanderSpawner>();
        commanderController = GetComponent<CommanderController>();

        if (solarSystemRenderer == null)
        {
            Debug.LogError("SolarSystemRenderer fehlt auf GameManager.");
            return;
        }

        if (commanderSpawner == null)
        {
            Debug.LogError("CommanderSpawner fehlt auf GameManager.");
            return;
        }

        SolarSystemGenerator generator = new SolarSystemGenerator();
        CurrentSystem = generator.GenerateSolarSystem(seed, mapWidth, mapHeight);

        solarSystemRenderer.RenderSolarSystem(CurrentSystem);

        SetupGameplay();
        SpawnTestCommander();
    }

    private void SetupGameplay()
    {
        GameplayLoop = new GameplayLoop();

        Faction playerFaction = new Faction
        {
            Name = "Player"
        };

        playerFaction.Resources[ResourceType.Gold] = 100;
        playerFaction.Resources[ResourceType.Iron] = 50;

        GameplayLoop.Factions.Add(playerFaction);
        GameplayLoop.CurrentFaction = playerFaction;
        GameplayLoop.CurrentTurn = 1;
    }

    private void SpawnTestCommander()
    {
        CommanderUnit commander = new CommanderUnit
        {
            Name = "Commander",
            MaxHealth = 30,
            CurrentHealth = 30,
            Attack = 8,
            Armor = 5,
            MagicResistance = 3,
            Position = new Vector2Int(mapWidth / 2, mapHeight / 2),
            MaxMovementPoints = 4,
            CurrentMovementPoints = 4
        };

        commander.AddUnit(new MeleeUnit
        {
            Name = "Soldier",
            MaxHealth = 10,
            CurrentHealth = 10,
            Attack = 4,
            Armor = 2,
            MagicResistance = 1,
            Category = UnitCategory.Melee
        });

        GameplayLoop.CurrentFaction.Commanders.Add(commander);

        GameObject commanderObject = commanderSpawner.SpawnCommander(commander, 0);

        if (commanderController != null && commanderObject != null)
        {
            CommanderView view = commanderObject.GetComponent<CommanderView>();
            commanderController.SelectCommander(view);
        }
    }
}