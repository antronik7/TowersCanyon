using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float healthPoint;

    private NavMeshAgent navMeshAgent;

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
        
    }

    public void Spawn(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(Vector3.zero);
    }

    public void ReceiveDamage(int amount)
    {
        healthPoint -= amount;
        if (healthPoint < 1)
            Kill();
    }

    public void Kill()
    {
        transform.position = new Vector3(-1000f, -1000f, -1000f);
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
    }
}
