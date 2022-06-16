using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Events : MonoBehaviour
{

    //COmo se llama en cada fotograma no necesitamos este evento
    //public Action<float> _changeHealthPlayer;
    //public Action<float> _changeCooldownImage;

    public Action<int> _changeTotalCoins;
    public Action<float> _changeTotalProgress;
    public Action<int> _monoliteActivated;
    
    public Action _canUpgradeAbility;

}
