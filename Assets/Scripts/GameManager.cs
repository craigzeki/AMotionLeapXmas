using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        INITIALIZING = 0,
        NOT_STARTED,
        IN_PROGRESS,
        FINISHED,
        NUM_OF_STATES
    }

    [SerializeField] private TextMeshProUGUI _pointsTMPro;
    [SerializeField] private TextMeshProUGUI _timeTMPro;
    [SerializeField] private GameObject[] presentPrefabs = new GameObject[(int)PresentColour.NUM_OF_COLORS];
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject startButton;

    [SerializeField] private float startTime = 120.0f;
    [SerializeField] private Vector3 maxBounds;
    [SerializeField] private Vector3 minBounds;

    private uint _points = 0;
    private float remainingTime = 0;
    private GameState gameState = GameState.INITIALIZING;

    private GameObject present;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Start()
    {
        //Init();
        //gameState = GameState.NOT_STARTED;
        //StartGame();
        //SpawnPresent();

    }
    private void Init()
    {
        _points = 0;
        UpdatePoints(_points);
        UpdateTime(startTime);
        remainingTime = startTime;
        
    }

    public bool IsInPlayArea(Vector3 position)
    {
        bool result = false;

        if ((position.x >= minBounds.x) && (position.x <= maxBounds.x)
            && (position.y >= minBounds.y) && (position.y <= maxBounds.y)
                && (position.z >= minBounds.z) && (position.z <= maxBounds.z)) result = true;

        return result;
    }

    void UpdatePoints(uint points)
    {
        if (_pointsTMPro == null) return;
        _pointsTMPro.text = "Points: " + points.ToString();
    }

    void UpdateTime(float time)
    {
        if (_timeTMPro == null) return;
        _timeTMPro.text = $"Time: {(uint)Mathf.CeilToInt(remainingTime)}";
    }

    public void AddPoints(uint points)
    {
        _points += points;
        UpdatePoints(_points);
    }

    public void SpawnPresent()
    {
        switch (gameState)
        {
            case GameState.INITIALIZING:
                
                break;
            case GameState.NOT_STARTED:
                break;
            case GameState.IN_PROGRESS:
                present = Instantiate(presentPrefabs[Random.Range((int)0, (int)PresentColour.NUM_OF_COLORS)], spawnPoint.position, spawnPoint.rotation);
                break;
            case GameState.FINISHED:
                break;
            case GameState.NUM_OF_STATES:
            default:
                break;
        }
        
    }

    public void StartGame()
    {
        switch (gameState)
        {
            case GameState.INITIALIZING:
                break;
            case GameState.NOT_STARTED:
                gameState = GameState.IN_PROGRESS;
                startButton.SetActive(false);
                Init();
                SpawnPresent();
                break;
            case GameState.IN_PROGRESS:
                break;
            case GameState.FINISHED:
                gameState = GameState.IN_PROGRESS;
                startButton.SetActive(false);
                Init();
                SpawnPresent();
                break;
            case GameState.NUM_OF_STATES:
            default:
                break;
        }
    }

    public IEnumerator WindUp()
    {
        yield return new WaitForSeconds(3f);
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.INITIALIZING:
                Init();
                gameState = GameState.NOT_STARTED;
                break;
            case GameState.NOT_STARTED:
                break;
            case GameState.IN_PROGRESS:
                remainingTime -= Time.deltaTime;
                if(remainingTime <= 0)
                {
                    remainingTime = 0;
                    startButton.SetActive(true);
                    gameState = GameState.FINISHED;
                }
                UpdateTime(remainingTime);

                break;
            case GameState.FINISHED:
                if (present != null) Destroy(present);
                break;
            case GameState.NUM_OF_STATES:
            default:
                break;
        }
        
    }
}
