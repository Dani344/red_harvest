using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStateBoss : MonoBehaviour
{
    private Boss_Patrol _bp;
    private Boss_Combat _bc;
    private void Awake()
    {
        _bp = GetComponent<Boss_Patrol>();
        _bc = GetComponent<Boss_Combat>();
    }
    
    public void Combat()
    {
        Debug.Log("Activar Estado Combate");
    }

    public void Patrol()
    {
        Debug.Log("Acticar Estado Patrol");
    }

    public void GameOverBoss()
    {
        Debug.Log("FIN BOSS");
    }
}
