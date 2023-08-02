using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]
    private TextMeshProUGUI textGold;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int currentGold;
    [SerializeField]
    private int goldAfterRound;
    [SerializeField]
    private EnemySpawner[] enemySpawners;

    private int currentRound = 1;
    private int currentNbrEnemies = 0;
    private bool inPlacingPhase = true;
    private List<Tower> towers = new List<Tower>();

    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        textGold.text = "Gold: " + currentGold;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlacingPhase && Input.GetKeyDown("space"))
            StartRound();
    }

    private void StartRound()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            currentNbrEnemies += spawner.GetNbrEnemiesInSpawner();
            spawner.Activate();
        }

        player.Deactivate();

        inPlacingPhase = false;
    }

    private void EndRound()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.ResetSpawner();
        }

        player.Activate();

        AddGold(goldAfterRound);
        ++currentRound;
        inPlacingPhase = true;
    }

    public void RemoveEnemiesFromTotal(int quantity)
    {
        currentNbrEnemies -= quantity;

        if (currentNbrEnemies < 1)
            EndRound();
    }

    public void AddGold(int goldValue)
    {
        currentGold += goldValue;

        textGold.text = "Gold: " + currentGold;
    }

    public bool SpendGold(int goldValue)
    {
        if (goldValue > currentGold)
            return false;

        currentGold -= goldValue;

        textGold.text = "Gold: " + currentGold;

        return true;
    }

    public void AddTower(Tower tower)
    {
        towers.Add(tower);
    }

    public void RemoveTower(Tower tower)
    {
        towers.Remove(tower);
    }
}
