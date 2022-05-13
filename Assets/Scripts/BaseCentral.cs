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
            var player = other.GetComponent<PlayerMovement>();
            if (!player.isPlayerSafeZone())
            {
                player.SetPlayerSafe(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Quitamos la cura/mana reg OP del player
            var player = other.GetComponent<PlayerMovement>();
            if (player.isPlayerSafeZone())
            {
                player.SetPlayerSafe(false);
            }
            
        }
    }
}
