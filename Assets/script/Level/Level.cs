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
    [SerializeField]
    protected GameObject passedLevelTips;

    protected bool passedLevel = false;

    protected float levelCost;
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
        levelScene nowLevel = nextLevel;
        nowLevel--;
        if (GameSystemManager.GetSystem<StudentEventManager>())
        {
            GameSystemManager.GetSystem<StudentEventManager>().logStudentEvent("level.start", "{level:'" + nowLevel + "'}");
        }
        levelCost = 0;
        passedLevel = false;
    }
    protected void updateTarget()
    {
        if (!targetSystem.targetStatus.Contains(false))
        {
            passedLevelTips.SetActive(true);
            passedLevel = true;
            passedLevelTips.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = 
                "您是第" + 1 + "位通過此關卡的人\n" + "使用了" + GameObject.Find("DeveloperConsoleObject").GetComponent<Console.DeveloperConsole>().inputLogs.Count
                + "行指令\n" + "並花費" + (int)levelCost + "秒通關";
        }
        else
        {
            passedLevelTips.SetActive(false);
            passedLevel = false;
        }
    }

    protected void levelCostCount()
    {
        if (!passedLevel)
        {
            levelCost += Time.deltaTime;
        }
    }
}
