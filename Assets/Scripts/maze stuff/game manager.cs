using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public mazetest mazePrefab;

    public mazetest mazeInstance;


    private void BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as mazetest;
        StartCoroutine(mazeInstance.Generate());
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }

    



}
