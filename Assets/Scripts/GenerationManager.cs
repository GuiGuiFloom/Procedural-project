using UnityEngine;
using UnityEngine.SceneManagement; // Permet d'avoir une autre méthode "RechargeMonde"
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // permet d'utiliser une référence "Silder"


public class GenerationManager : MonoBehaviour
{
    [SerializeField] Transform WorldGrid; // Référence au WorldGrid qui est le parent des "rooms"
    [SerializeField] List<GameObject> TypesRoom; // Une Liste qui nous permet aux scripts de choisr aléatoirement les prefabs
    [SerializeField] int tailleCarte = 16; // Ceci est la taille de la carte, le chiffre choisi doit avoir obligatoirement une racine carré entier
    [SerializeField] Slider TailleCarteSlider, VideSlider;
    [SerializeField] Button GenerateButton;
    [SerializeField] GameObject E_Room; // Le type "room" vide (la salle vide)
    public int carteEmptiness; // La chance pour qu'un E_Room spawn, c'est pour contrôler a quel point la carte est vide ou dense
    private int tailleCarteCarré; // La racine caré de la taille de la carte

    private Vector3 positionAct; // La position actuelle de la "room" ou elle doit être généré
    private int positionActX, positionActZ, posActTracker; // Ceci permettra de tracker la position de la "room" ou elle est généré
    public float tailleRoom = 7; // la valeur x et y des cubes


    public void Update()
    {
        tailleCarte = ((int)Mathf.Pow(TailleCarteSlider.value, 4)); // Ceci est pour assurer que notre valeur sera toujours au carré
        
        tailleCarteCarré = ((int)Mathf.Sqrt(tailleCarte)); // cette variable nous permet d'avoir en permanence la racine carré de la taille de la carte en int pour eviter de crash

        carteEmptiness = (int)VideSlider.value;
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

        for (int i = 0; i < tailleCarte; i++)
        {
            if (posActTracker == tailleCarteCarré) // Permet de ramener la position sur le grid au début pour qu'il puisse aller au dessus
            {
                positionActX = 0;
                posActTracker = 0;

                positionActZ += (int)tailleRoom;

                
            }

            positionAct = new(positionActX, 0, positionActZ);

            Instantiate(TypesRoom[Random.Range(0, TypesRoom.Count)], positionAct, Quaternion.identity, WorldGrid); // Instantie les type de room à positionAct. (position actuelle)

            posActTracker ++; // traque la position X sans utiliser les tailles des room
            positionActX += (int)tailleRoom; // Ajoute plus de position à postionActX. (position actuelle sur l'axe X), déplaçent la position sur la droite


        }
    }
}
