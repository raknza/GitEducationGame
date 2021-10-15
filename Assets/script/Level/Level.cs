using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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
    protected int  costLines;
    public enum levelScene
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
        Level0,
    };
    [SerializeField]
    protected levelScene nextLevel;
    public levelScene nowLevel { get; private set; }

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
            costLines = GameObject.Find("DeveloperConsoleObject").GetComponent<Console.DeveloperConsole>().inputLogs.Count;
            passedLevelTips.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = 
                "您使用了" + costLines + "行指令\n" + "並花費" + (int)levelCost + "秒通關";

            GameSystemManager.GetSystem<StudentEventManager>().logStudentEvent("level_passed", "{level:'" + nowLevel + "'" +
                ", line_cost:'" + costLines + "', time_cost:'" + (int)levelCost + "' }");
            StartCoroutine(achievementSet());
            
        }
        else if(!passedLevel)
        {
            passedLevelTips.SetActive(false);
            passedLevel = false;
        }
    }

    IEnumerator achievementSet()
    {
        // achieve 1
        if (nowLevel == levelScene.Level1)
        {
            StartCoroutine(GameSystemManager.GetSystem<AchievementManager>().logAchievement(1));
        }
        // achieve 2
        if (nowLevel == levelScene.Level5)
        {
            StartCoroutine(GameSystemManager.GetSystem<AchievementManager>().logAchievement(2));
        }
        // achieve 3
        if (nowLevel == levelScene.Level9)
        {
            StartCoroutine(GameSystemManager.GetSystem<AchievementManager>().logAchievement(3));
        }
        // achieve 7
        if (nowLevel != levelScene.Level0 && levelCost < 30)
        {
            StartCoroutine(GameSystemManager.GetSystem<AchievementManager>().logAchievement(7));
        }

        // log level record
        yield return StartCoroutine(GameSystemManager.GetSystem<LeaderBoard>().logLevelRecord((int)levelCost, costLines, (int)nowLevel + 1));

        // achieve 4 
        using (UnityWebRequest www = UnityWebRequest.Get(GameSystemManager.GetSystem<LeaderBoard>().getLeaderBoardApi() + "?level=" + ((int)nowLevel + 1)  ))
        {
            yield return www.SendWebRequest();
            string jsonString = JsonHelper.fixJson(www.downloadHandler.text);
            Debug.Log(GameSystemManager.GetSystem<LeaderBoard>().getLeaderBoardApi() + "?level=" + ((int)nowLevel + 1));
            LeaderBoard.LevelRecord[] leaderboardRecords = JsonHelper.FromJson<LeaderBoard.LevelRecord>(jsonString);
            if (leaderboardRecords[0].username == GameSystemManager.GetSystem<StudentEventManager>().username)
            {
                StartCoroutine(GameSystemManager.GetSystem<AchievementManager>().logAchievement(4));
            }
        }
        
    }

    protected void levelCostCount()
    {
        if (!passedLevel)
        {
            levelCost += Time.deltaTime;
        }
    }



    public void showLevelLeaderboard()
    {
        GameSystemManager.GetSystem<LeaderBoard>().getLevelLeaderboard();
    }

}
