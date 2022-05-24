using System;
using UnityEngine;

public class Eventitos : MonoBehaviour
{
    public Action _primerEvento;
    public Action<Eventitos> _segundoEvento;
    public Action<int> _tercerEvento;
    public Action<bool> isReady;

    public Action<bool, int> _EmpiezaBatalla;

    private void Start()
    {
        Invoke(nameof(NewMethod),4f);
    }

    private void NewMethod()
    {
        isReady?.Invoke(true);
        _EmpiezaBatalla?.Invoke(false, 3);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _primerEvento?.Invoke();
            _segundoEvento?.Invoke(this);
            _tercerEvento?.Invoke(5);
        }
    }
}
