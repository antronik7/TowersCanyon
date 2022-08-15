using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private EnemySpawner[] enemySpawners;

    private int currentRound = 1;
    private bool inPlacingPhase = true;
    private int currentActiveSpawner = 0;

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
            spawner.Activate();
        }

        player.Deactivate();

        currentActiveSpawner = enemySpawners.Length;
        inPlacingPhase = false;
    }

    private void EndRound()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.ResetSpawner();
        }

        player.Activate();

        ++currentRound;
        inPlacingPhase = true;
    }

    public void EnemySpawnerDone()
    {
        --currentActiveSpawner;

        if (currentActiveSpawner < 1)
            EndRound();
    }
}
