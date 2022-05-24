using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventitoSsus : MonoBehaviour
{
    private Eventitos _compo;

    private void Awake()
    {
        _compo = FindObjectOfType<Eventitos>();
    }

    private void Start()
    {
        _compo._primerEvento += HazPrimerEvento;
        _compo._segundoEvento += HazSegundoEvento;

        _compo._tercerEvento += HazTercerEvento;


        _compo.isReady += HazCuartoEvento;
        _compo._EmpiezaBatalla += Otro;
    }


    private void Update()
    {
    }

    void HazPrimerEvento()
    {
        print("Primer Evento!");
    }


    private void HazCuartoEvento(bool empieza)
    {
        print("Está listo! 4º Evento!");
    }

    private void HazSegundoEvento(Eventitos target)
    {
        var currentEventitos = target;
        print("Segundo Evento");
    }

    private void HazTercerEvento(int cant)
    {
        print("Tecer Evento: " + cant);
    }

    private void Otro(bool empieza, int vidas)
    {
        print("Empieza la batalla: " + empieza);
        print("vidas: " + vidas);
    }
    
    

    private void OnDestroy()
    {
        _compo._primerEvento -= HazPrimerEvento;
    }
}
