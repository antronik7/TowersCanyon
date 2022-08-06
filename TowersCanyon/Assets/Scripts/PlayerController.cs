using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tower testTower;

    private Tower towerGhost;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTowerGhost();
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
        towerGhost.transform.position = GridManager.instance.ConvertToGridPosition(GetMouseCastPosition());
    }

    private Vector3 GetMouseCastPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
            return hit.point;

        return Vector3.zero;
    }

    private void SpawnTowerGhost()
    {
        towerGhost = Instantiate(testTower);
        MoveTowerGhost();
    }

    private void PlaceTower()
    {
        towerGhost.MakeTransparent(false);
        SpawnTowerGhost();
    }
}
