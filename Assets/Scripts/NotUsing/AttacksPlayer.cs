using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (_ballPrefab)
            {
                var temp = Instantiate(_ballPrefab, transform.position, Quaternion.identity);
                temp.GetComponent<Ball>().SetDirection(transform.forward);

            }
        }
    }
}   
