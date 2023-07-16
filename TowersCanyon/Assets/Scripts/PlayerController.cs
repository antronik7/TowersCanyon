using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tower testTower;
    [SerializeField]
    private LayerMask layerMask;

    private Tower towerGhost;

    // Start is called before the first frame update
    void Start()
    {
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowerGhost();
        ManageInputs();
    }

    private void ManageInputs()
    {
        if (Input.GetMouseButtonDown(0))
            PlaceTower();
    }

    private void MoveTowerGhost()
    {
        if (towerGhost == null)//temp for testing...
            return;

        towerGhost.transform.position = GridManager.instance.ConvertToGridPosition(GetMouseCastPosition());
    }

    private Vector3 GetMouseCastPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                return hit.point;
        }

        return new Vector3(-1000f, -1000f, -1000f);
    }

    private void SpawnTowerGhost()
    {
        towerGhost = Instantiate(testTower);
        MoveTowerGhost();
    }

    private void PlaceTower()
    {
        Vector2 gridPosition = GridManager.instance.ConvertPositionToGrid(towerGhost.transform.position);

        if (GridManager.instance.TestPositionOnGrid(gridPosition) == false)//Can only check if cell is empty...
            return;

        if (GridManager.instance.CheckIfCellEmpty(gridPosition) == false)
            return;

        if (GameManager.instance.SpendGold(testTower.GetPrice()) == false)
            return;

        GridManager.instance.AddTower(gridPosition);
        GameManager.instance.AddTower(towerGhost);
        towerGhost.MakeTransparent(false);
        SpawnTowerGhost();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        SpawnTowerGhost();
    }

    public void Deactivate()
    {
        Destroy(towerGhost.gameObject);
        gameObject.SetActive(false);
    }
}
