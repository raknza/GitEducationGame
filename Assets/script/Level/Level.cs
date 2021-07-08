using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected TargetSystem targetSystem;
    [SerializeField]
    protected FileSystem fileSystem;
    [SerializeField]
    protected GitSystem gitSystem;

    [SerializeField]
    protected Button nextLevelButton;
    protected enum levelScene
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,

    };
    [SerializeField]
    protected levelScene nextLevel;

    protected void setUp()
    {
        nextLevelButton.onClick.AddListener(delegate
        {
            GameSystemManager.GetSystem<SceneStateManager>().LoadSceneState(new LoadSceneState("MainSceneState", nextLevel + "Scene"), true);
        });
    }
    protected void updateTarget()
    {
        if (!targetSystem.targetStatus.Contains(false))
        {
            nextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }
}
