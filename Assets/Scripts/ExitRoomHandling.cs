using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoomHandling : MonoBehaviour
{
   
    // permet d'activer le Trigger l'Exit
    public void OnTriggerEnter(Collider other)
    {
        GenerationManager gm = FindObjectOfType<GenerationManager>();

        gm.WinGame();
    }
}
