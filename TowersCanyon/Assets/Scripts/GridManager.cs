using UnityEngine;
using Unity.AI.Navigation;

public class GridManager : MonoBehaviour
{
    public enum CellContent
    {
        Empty,
        Wall,
        Tower
    }

    public static GridManager instance = null;

    [SerializeField]
    private int gridRow = 0;
    [SerializeField]
    private int gridColumn = 0;
    [SerializeField]
    private float cellSize = 0f;
    [SerializeField]
    private GameObject wall;

    private NavMeshSurface navMeshSurface;

    private CellContent[,] grid;
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

        navMeshSurface = GetComponent<NavMeshSurface>();

        InitializeGrid();
        GenerateLevel();
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
        grid = new CellContent[gridRow, gridColumn];
        gridOriginPosition = transform.position + (Vector3.left * (gridRow / 2f)) + (Vector3.forward * (gridRow / 2f)) + (Vector3.right * halfCellSize) + (Vector3.back * halfCellSize);
    }

    private void GenerateLevel()
    {
        grid[0, 0] = CellContent.Wall;
        grid[0, 1] = CellContent.Wall;
        grid[0, 2] = CellContent.Wall;
        grid[1, 0] = CellContent.Wall;
        grid[1, 1] = CellContent.Wall;

        grid[0, 7] = CellContent.Wall;
        grid[0, 8] = CellContent.Wall;
        grid[0, 9] = CellContent.Wall;
        grid[1, 8] = CellContent.Wall;
        grid[1, 9] = CellContent.Wall;

        grid[8, 0] = CellContent.Wall;
        grid[8, 1] = CellContent.Wall;
        grid[9, 0] = CellContent.Wall;
        grid[9, 1] = CellContent.Wall;
        grid[9, 2] = CellContent.Wall;

        grid[8, 8] = CellContent.Wall;
        grid[8, 9] = CellContent.Wall;
        grid[9, 7] = CellContent.Wall;
        grid[9, 8] = CellContent.Wall;
        grid[9, 9] = CellContent.Wall;

        grid[2, 3] = CellContent.Wall;
        grid[2, 4] = CellContent.Wall;
        grid[2, 5] = CellContent.Wall;
        grid[2, 6] = CellContent.Wall;

        for (int i = 0; i < gridRow; ++i)
        {
            for (int j = 0; j < gridColumn; ++j)
            {
                if (grid[i, j] == CellContent.Wall)
                    Instantiate(wall, GetCellPosition(i, j), Quaternion.identity);
            }
        }

        navMeshSurface.BuildNavMesh();
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

    public Vector3 GetCellPosition(int row, int column)
    {
        return gridOriginPosition + (Vector3.back * row) + (Vector3.right * column);
    }

    public Vector2 ConvertPositionToGrid(Vector3 positon)
    {
        return new Vector2(((positon.z * -1f) - (cellSize / 2f)) + gridRow / 2, (positon.x - (cellSize / 2f)) + gridColumn / 2);
    }

    public Vector2 GetClosestTowerPosition(Vector3 position)
    {
        int closestRow = 0;
        int closestColumn = 0;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < gridRow; ++i)
        {
            for (int j = 0; j < gridColumn; ++j)
            {
                if (grid[i, j] == CellContent.Tower)
                {
                    float cellDistance = Vector3.Distance(position, GetCellPosition(i, j));

                    if (cellDistance < closestDistance)
                    {
                        closestDistance = cellDistance;
                        closestRow = i;
                        closestColumn = j;
                    }
                }
            }
        }

        return new Vector2(closestRow, closestColumn);
    }

    public void AddTower(Vector2 gridPosition)
    {
        grid[(int)gridPosition.x, (int)gridPosition.y] = CellContent.Tower;//Int casting may cause some errors...
    }
}
