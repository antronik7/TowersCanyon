using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance = null;

    [SerializeField]
    private int gridLength = 0;
    [SerializeField]
    private int gridWidth = 0;
    [SerializeField]
    private float cellSize = 0f;

    private bool[,] grid;
    private Vector3 gridOriginPosition;

    //Awake is always called before any Start functions
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

        InitializeGrid();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeGrid()
    {
        float halfCellSize = cellSize / 2f;
        grid = new bool[gridLength, gridWidth];
        gridOriginPosition = transform.position + (Vector3.left * (gridLength / 2f)) + (Vector3.forward * (gridLength / 2f)) + (Vector3.right * halfCellSize) + (Vector3.back * halfCellSize);
    }

    public Vector3 ConvertToGridPosition(Vector3 position)
    {
        return new Vector3(RoundNumberOnGrid(position.x), 0f, RoundNumberOnGrid(position.z));
    }

    private float RoundNumberOnGrid(float number)
    {
        float gridNumber = Mathf.Abs(number);
        float numberSign = Mathf.Sign(number);

        gridNumber = Mathf.Ceil(gridNumber);
        gridNumber -= cellSize / 2f;
        gridNumber *= numberSign;

        return gridNumber;
    }
}
