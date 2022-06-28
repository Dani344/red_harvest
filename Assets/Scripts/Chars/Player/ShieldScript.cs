using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PaperConstants.TAG_PROJECTIL))
        {
            var proj = other.GetComponent<Projectil>();
            var isProjectilPlayer = proj.isPlayerProjectil();
            if (isProjectilPlayer) return;
            
            Destroy(other.gameObject);
        }
    }
}
