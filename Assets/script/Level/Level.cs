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
    protected Button restartLevelButton;
    [SerializeField]
    protected Button returnTitleButton;
    [SerializeField]
    protected GameObject passedLevelTips;

    protected bool passedLevel = false;
    public bool levelStarted { private set; get; } = false;

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
    levelScene nowLevel;

    protected void setUp()
    {
        nowLevel = nextLevel;
        nowLevel--;
        nextLevelButton.onClick.AddListener(delegate
        {
            GameSystemManager.GetSystem<SceneStateManager>().LoadSceneState(new LoadSceneState("MainSceneState", nextLevel + "Scene"), true);
        });
        restartLevelButton.onClick.AddListener(delegate
        {
            GameSystemManager.GetSystem<SceneStateManager>().LoadSceneState(new LoadSceneState("MainSceneState", nowLevel + "Scene"), true);
            GameSystemManager.GetSystem<StudentEventManager>().logStudentEvent("level_restart", "{level:'" + nowLevel + "'}");
        });
        returnTitleButton.onClick.AddListener(delegate
        {
            GameSystemManager.GetSystem<SceneStateManager>().LoadSceneState(new LoadSceneState("MainSceneState", "TitleScene"), true);
            GameSystemManager.GetSystem<StudentEventManager>().logStudentEvent("level_quit", "{level:'" + nowLevel + "'}");
        });
        if (GameSystemManager.GetSystem<StudentEventManager>())
        {
            GameSystemManager.GetSystem<StudentEventManager>().logStudentEvent("level_start", "{level:'" + nowLevel + "'}");
        }

        levelCost = 0;
        passedLevel = false;
        levelStarted = true;
    }
    protected void updateTarget()
    {
        if (!targetSystem.targetStatus.Contains(false) && !passedLevel)
        {
            passedLevelTips.SetActive(true);
            passedLevel = true;
            int costLines = GameObject.Find("DeveloperConsoleObject").GetComponent<Console.DeveloperConsole>().inputLogs.Count;
            passedLevelTips.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = 
                "您使用了" + costLines + "行指令\n" + "並花費" + (int)levelCost + "秒通關";

            GameSystemManager.GetSystem<StudentEventManager>().logStudentEvent("level_passed", "{level:'" + nowLevel + "'" +
                ", lines:'" + costLines +  "', time:'" + (int)levelCost + "' }");
        }
        else if(!passedLevel)
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
