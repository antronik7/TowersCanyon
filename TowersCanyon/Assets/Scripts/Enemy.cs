using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int healthPoint;
    [SerializeField]
    private TextMeshProUGUI healthUI;

    private NavMeshAgent navMeshAgent;
    private bool isAlive = false;
    private int currentHealthPoints;
    private Vector3 latestPosition;

    //Awake is always called before any Start functions
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
            latestPosition = transform.position;
    }

    public void Spawn(Vector3 spawnPosition, Vector3 targetPosition)
    {
        currentHealthPoints = healthPoint;
        healthUI.text = "" + currentHealthPoints;
        isAlive = true;
        transform.position = spawnPosition;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(targetPosition);
        latestPosition = transform.position;
    }

    public void ReceiveDamage(int amount)
    {
        currentHealthPoints -= amount;
        healthUI.text = "" + currentHealthPoints;

        if (currentHealthPoints < 1)
            Kill();
    }

    public void Kill()
    {
        isAlive = false;
        transform.position = new Vector3(-1000f, -1000f, -1000f);
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        GameManager.instance.RemoveEnemiesFromTotal(1);
    }

    public bool CheckIfAlive()
    {
        return isAlive;
    }

    public Vector3 GetPosition()
    {
        return latestPosition;
    }
}
