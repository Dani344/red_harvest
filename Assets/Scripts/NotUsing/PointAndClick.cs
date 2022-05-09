using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClick : MonoBehaviour
{
    public NavMeshAgent target;
    private Transform miplayer;
    private Camera _cam;

    private void Awake()
    {
        target = FindObjectOfType<NavMeshAgent>();
        miplayer = target.transform;
        _cam = FindObjectOfType<Camera>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                var newpos = hit.point;
                target.SetDestination(newpos);
            }
        }
    }
}
