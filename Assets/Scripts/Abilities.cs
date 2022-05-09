using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [SerializeField] private Image _abilityQ;
    [SerializeField] private float _cooldownQ = 5f;
    private bool _isOnCooldown = false;
    private void Start()
    {
        _abilityQ.fillAmount = 1f;
    }

    // Update is called once per frame
    private void Update()
    {
        AbilityQ();
    }

    private void AbilityQ(){
        if (Input.GetKeyDown(KeyCode.Q) && _isOnCooldown == false){
            _isOnCooldown = true;
            _abilityQ.fillAmount = 1f;
        }

        if (_isOnCooldown){
            _abilityQ.fillAmount -= 1 / _cooldownQ * Time.deltaTime;

            if (_abilityQ.fillAmount <= 0){
                _abilityQ.fillAmount = 0f;
                _isOnCooldown = false;
            }
        }
    }
}
