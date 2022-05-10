using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCentral : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Activamos la cura OP del player.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Quitamos la cura/mana reg OP del player
        }
    }
}
