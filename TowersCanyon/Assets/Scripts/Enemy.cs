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
        navMeshAgent.SetDestination(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(int amount)
    {
        healthPoint -= amount;
        if (healthPoint < 1)
            Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
