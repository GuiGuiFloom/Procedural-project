using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Permet d'avoir une autre méthode "RechargeMonde"
using UnityEngine.UI; // permet d'utiliser une référence "Silder"

public enum GenerationState
{
    Idle,
    GenerationRooms,
    GenerationsLumière,

    GenerationSpawn,
    GenerationExit,

    GenerationBarriere
}
public class GenerationManager : MonoBehaviour
{
    [Header("Référence")]
    [SerializeField] Transform WorldGrid; // Référence au WorldGrid qui est le parent des "rooms"
    [SerializeField] List<GameObject> TypesRoom; // Une Liste qui nous permet aux scripts de choisr aléatoirement les prefabs
    [SerializeField] List<GameObject> TypesLumière; // une liste de prefab qui apparait dans nos "rooms" pour donner des lumières
    [SerializeField] int tailleCarte = 16; // Ceci est la taille de la carte, le chiffre choisi doit avoir obligatoirement une racine carré entier
    [SerializeField] Slider TailleCarteSlider, VideSlider, LuminositeSlider;
    [SerializeField] Button GenerateButton;
    [SerializeField] GameObject E_Room; // Le type "room" vide (la salle vide)
    [SerializeField] GameObject B_Room; // Le type "room" Barrière
    [SerializeField] GameObject SpawnRoom, ExitRoom;
    public List<GameObject> GeneratedRooms; // Stock les salles qui sont déja généré

    [SerializeField] GameObject PlayerObject, MainCameraObject;

    [Header("Paramètre")]
    public int carteEmptiness; // La chance pour qu'un E_Room spawn, c'est pour contrôler a quel point la carte est vide ou dense
    public int carteLuminosité; // La chance d'un type de lumière qui apparait 
    private int tailleCarteCarré; // La racine caré de la taille de la carte

    private Vector3 positionAct; // La position actuelle de la "room" ou elle doit être généré
    private int positionActX, positionActZ, posActTracker, roomAct; // Ceci permettra de tracker la position de la "room" ou elle est généré
    public float tailleRoom = 7; // la valeur x et y des cubes
    public GenerationState currentState; // L'état actuel de la génération


    public void Update()
    {
        tailleCarte = ((int)Mathf.Pow(TailleCarteSlider.value, 4)); // Ceci est pour assurer que notre valeur sera toujours au carré

        tailleCarteCarré = ((int)Mathf.Sqrt(tailleCarte)); // cette variable nous permet d'avoir en permanence la racine carré de la taille de la carte en int pour eviter de crash

        carteEmptiness = (int)VideSlider.value;

        carteLuminosité = (int)LuminositeSlider.value;

       

    }

    public void RechargeMonde() // Recharge le monde pour qu'on puisse en faire un nouveau
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // recharge la scène actuellement chargés 
    }

    public void GenerationMonde() // Génére des mondes quand ceci est clliqué
    {
        for (int i = 0; i < carteEmptiness; i++)
        {
            TypesRoom.Add(E_Room); // ajoute des "room" vide aux TypesRoom array (a la liste des types de salles)
        }

        GenerateButton.interactable = false; // Sert à empêcher de généré infiniment le monde


        for (int state = 0; state < 6; state++)
        {
            for (int i = 0; i < tailleCarte; i++)
            {
                if (posActTracker == tailleCarteCarré) // Permet de ramener la position sur le grid au début pour qu'il puisse aller au dessus
                {
                    if (currentState == GenerationState.GenerationBarriere) GenerationBarriere(); // fait apparaitre des barrière a la droite de la carte
                    
                    positionActX = 0;
                    posActTracker = 0;

                    positionActZ += (int)tailleRoom;

                    if (currentState == GenerationState.GenerationBarriere) GenerationBarriere(); // fait apparaitre des barrière a la gauche de la carte


                }

                positionAct = new(positionActX, 0, positionActZ);



                switch (currentState) // il va générer soit les "rooms", soit les lumières qui carie en fonction de l'état de la génération
                {
                    case GenerationState.GenerationRooms:
                        GeneratedRooms.Add(Instantiate(TypesRoom[Random.Range(0, TypesRoom.Count)], positionAct, Quaternion.identity, WorldGrid)); // Instantie les type de room à positionAct. (position actuelle)
                        break;

                    case GenerationState.GenerationsLumière:
                        int lumièreSpawn = Random.Range(-1, carteLuminosité);

                        if (lumièreSpawn == 0)
                            Instantiate(TypesLumière[Random.Range(0, TypesLumière.Count)], positionAct, Quaternion.identity, WorldGrid); // Instantie les type de room à positionAct. (position actuelle)

                        break;


                    case GenerationState.GenerationBarriere:

                        if (roomAct <= tailleCarteCarré && roomAct >= 0)

                        {
                            GenerationBarriere(); // dessous de la carte
                        }

                        if (roomAct <= tailleCarteCarré && roomAct >= tailleCarte - tailleCarteCarré)

                        {
                            GenerationBarriere(); // dessus de la carte
                        }

                        break;
                }



                roomAct++;
                posActTracker++; // traque la position X sans utiliser les tailles des room
                positionActX += (int)tailleRoom; // Ajoute plus de position à postionActX. (position actuelle sur l'axe X), déplaçent la position sur la droite

            }

            NextState();
            // gère le spawn room et l'exitroom, qui ne dépend pas du world grid, mais de la position de salle qui va remplacer.

            switch (currentState)
            {
                case GenerationState.GenerationExit: // va choisir une "room" alétoire généré puis la détruire pour la remplacer par une exit room

                    int roomToReplace = Random.Range(0, GeneratedRooms.Count);

                    GameObject exitRoom = Instantiate(ExitRoom, GeneratedRooms[roomToReplace].transform.position, Quaternion.identity, WorldGrid);

                    Destroy(GeneratedRooms[roomToReplace]);

                    GeneratedRooms[roomToReplace] = exitRoom;

                    break;

                case GenerationState.GenerationSpawn: // va choisir une "room" alétoire généré puis la détruire pour la remplacer par une spawn room

                    int _roomToReplace = Random.Range(0, GeneratedRooms.Count);

                    spawnRoom = Instantiate(SpawnRoom, GeneratedRooms[_roomToReplace].transform.position, Quaternion.identity, WorldGrid);

                    Destroy(GeneratedRooms[_roomToReplace]);

                    GeneratedRooms[_roomToReplace] = spawnRoom;

                    break;


            }






        }



    }


    public GameObject spawnRoom;

    public void SpawnPlayer() // Le GameObject Joueur est désactiver puis réactivé par le bouton "spawn"
    {
        //PlayerObject.SetActive(false);


        PlayerObject.transform.position = new Vector3(spawnRoom.transform.position.x, 1.8f, spawnRoom.transform.position.z);

        PlayerObject.SetActive(true);
        MainCameraObject.SetActive(false);

    }


    public void NextState()
    {
        currentState++; // va à l'état suivant


        // Reset nos variables
        roomAct = 0;

        positionActX = 0;
        positionActZ = 0;
        positionAct = Vector3.zero;
        posActTracker = 0;
    }


    public void WinGame() // Quand le joueur touche l'exit il gagne
    {
        MainCameraObject.SetActive(true); // Réactive la caméra
        PlayerObject.SetActive(false); // désactive le joueur

        Cursor.lockState = CursorLockMode.None;
        // fait réapparaitre le curseur
        Cursor.visible = true;

        Debug.Log("Le Joueur est sorti et à réussi");
    }


    public void GenerationBarriere() // genère la "room" barrière à la position actuelle
    {
        positionAct = new(positionActX, 0, positionActZ);

        Instantiate(B_Room, positionAct, Quaternion.identity, WorldGrid);
    }
}
