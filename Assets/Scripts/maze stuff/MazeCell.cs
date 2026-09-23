using UnityEngine;

public class MazeCell : MonoBehaviour
{

    public int sizeX, sizeZ;

    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    GameObject bigboss;
    mazetest mazeInstance;
    private void BeginGame()
    {
        
    }

    private void RestartGame()
    {
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }


    public void Start()
    {
        bigboss = GameObject.Find("bigboss");
        mazeInstance = bigboss.GetComponent<GameManager>().mazeInstance;

    }
    public IntVector2 coordinates;


}
