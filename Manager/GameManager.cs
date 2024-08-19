using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private Player _player;
    private DataManager _dataManager;
    private SpriteManager _spriteManager;
    private ObjectPool _objectPool;
    private List<Node> _nodeList;
    private List<GameObject> _createdBlockList;
    public List<MapNode> MapNodeList;

    public Player Player { get { return _player; } set { _player = value; } }
    public ObjectPool ObjectPool { get { return _objectPool; } set { _objectPool = value; } }
    public DataManager DataManager { get { return _dataManager; } }
    public SpriteManager SpriteManager { get { return _spriteManager; } }
    public List<Node> NodeList { get { return _nodeList; } set { _nodeList = value; } }
    public List<GameObject> CreatedBlockList { get { return _createdBlockList; } set { _createdBlockList = value; } }
    public MapManager mapManager;
    public bool isNewGame = true;
    public CardWrapper cardDragged;
    public bool isCardDragged = false;

    private const float targetAspectRatio = 16.0f / 9.0f;  // FHD (16:9) 비율

    private int lastScreenWidth;
    private int lastScreenHeight;

    protected override void Awake()
    {

        base.Awake();
        if (GameManager.Instance != null && GameManager.Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            mapManager = GetComponent<MapManager>();

            _dataManager = new DataManager();

            _spriteManager = new SpriteManager();

            _dataManager.Initialize();
            _spriteManager.Initialize();

            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        _createdBlockList = new List<GameObject>();
        isNewGame = false;

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        UpdateWindowSize();
    }

    void Update()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            UpdateWindowSize();
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
        }
    }

    public void ResetGame()
    {
        isNewGame = false;
        CreatedBlockList = null;
        CreatedBlockList = new List<GameObject>();
        Player.fullhp = 50;
        Player.hp = 50;
        Player.Gold = 500;
        Player.playerDeck.Initialize();
        mapManager = GetComponent<MapManager>();
        System.IO.File.Delete($"{Application.dataPath}/Resources/Map.csv");

    }

    void UpdateWindowSize()
    {
        // 현재 창 크기의 비율을 계산합니다.
        float currentAspectRatio = (float)Screen.width / Screen.height;

        if (currentAspectRatio > targetAspectRatio)
        {
            // 현재 비율이 목표 비율보다 클 경우 (너무 넓을 때)
            int width = Mathf.RoundToInt(Screen.height * targetAspectRatio);
            Screen.SetResolution(width, Screen.height, false);
        }
        else if (currentAspectRatio < targetAspectRatio)
        {
            // 현재 비율이 목표 비율보다 작을 경우 (너무 좁을 때)
            int height = Mathf.RoundToInt(Screen.width / targetAspectRatio);
            Screen.SetResolution(Screen.width, height, false);
        }

        Debug.Log($"Window resized to: {Screen.width}x{Screen.height} (Aspect Ratio: {targetAspectRatio})");
    }


}


