using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        GameSystemManager.GetSystem<SceneStateManager>().LoadSceneState( new LoadSceneState("MainSceneState", "Level1Scene") ,true);
    }
}
