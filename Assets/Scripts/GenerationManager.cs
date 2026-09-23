using UnityEngine;
using UnityEngine.SceneManagement; // Permet d'avoir une autre méthode "RechargeMonde"

public class GenerationManager : MonoBehaviour
{
    [SerializeField] Transform WorldGrid; // Référence au WorldGrid qui est le parent des "rooms"
    [SerializeField] GameObject RoomPrefab;
    public int tailleCarte = 16; // Ceci est la taille de la carte, le chiffre choisi doit avoir obligatoirement une racine carré entier
    private int tailleCarteCarré; // La racine caré de la taille de la carte

    private Vector3 positionAct; // La position actuelle de la "room" ou elle doit être généré
    private int positionActX, positionActZ; // Ceci permettra de tracker la position de la "room" ou elle est généré
    private int posTracker; // Garde la position de notre générateur

    public void Update()
    {
        tailleCarteCarré = ((int)Mathf.Sqrt(tailleCarte)); // cette variable nous permet d'avoir en permanence la racine carré de la taille de la carte en int pour eviter de crash
    }

    public void RechargeMonde() // Recharge le monde pour qu'on puisse en faire un nouveau
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GenerationMonde() // Génére des mondes quand ceci est clliqué
    {
        for (int i = 0; i < tailleCarte; i++)
        {

            Instantiate(RoomPrefab, positionAct, Quaternion.identity, WorldGrid); // il va créer des rooms par rapport à la valeur dans "tailleCarte"
        }
    }
}
