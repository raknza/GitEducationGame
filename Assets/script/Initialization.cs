using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour {
    [SerializeField]
    List<GameObject> _prefab;

    // Use this for initialization
    void Start () {

        if(!GameSystemManager.GetSystem<ScreenEffect>())
            GameSystemManager.AddSystem<ScreenEffect>(Instantiate(_prefab[0]));
    }



}
