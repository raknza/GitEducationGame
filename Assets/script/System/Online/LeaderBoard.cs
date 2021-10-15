using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{

    [SerializeField]
    string leaderBoardApi;
    [SerializeField]
    string logLevelRecordApi;
    [SerializeField]
    Text content;
    [SerializeField]
    Text head;
    [SerializeField]
    Text index;
    [SerializeField]
    Text username;
    [SerializeField]
    Text timeCost;
    [SerializeField]
    Text lineCost;
    [SerializeField]
    Text time;


    public void getLevelLeaderboard()
    {
        gameObject.SetActive(true);
        Level.levelScene nowLevel = GameObject.Find("MainScreenObject").GetComponent<Level>().nowLevel;
        int levelIndex = Array.IndexOf(Level.levelScene.GetValues(nowLevel.GetType()), nowLevel);
        index.text = "";
        username.text = "";
        timeCost.text = "";
        lineCost.text = "";
        time.text = "";
        StartCoroutine(getLevelLeaderboard(levelIndex + 1));
    }

    IEnumerator getLevelLeaderboard(int level)
    {
        head.text = "排行榜 - Level " + level;

        using (UnityWebRequest www = UnityWebRequest.Get(leaderBoardApi + "?level=" + level))
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            string jsonString = JsonHelper.fixJson(www.downloadHandler.text);
            LevelRecord[] leaderboardRecords = JsonHelper.FromJson<LevelRecord>(jsonString);
            for(int i = 0; i< leaderboardRecords.Length && (i < 10) ; i++)
            {
                index.text = index.text + (i + 1) + "." + "\n";
                username.text = username.text + leaderboardRecords[i].username + "\n";
                timeCost.text = timeCost.text + leaderboardRecords[i].time_cost + "\n";
                lineCost.text = lineCost.text + leaderboardRecords[i].line_cost + "\n";
                time.text = time.text + leaderboardRecords[i].time + "\n";
            }
        }

    }


    public IEnumerator logLevelRecord(int timeCost , int lineCost , int level)
    {

        WWWForm form = new WWWForm();

        form.AddField("username", GameSystemManager.GetSystem<StudentEventManager>().username);
        form.AddField("timeCost", timeCost);
        form.AddField("lineCost", lineCost);
        form.AddField("level", level);


        using (UnityWebRequest www = UnityWebRequest.Post(logLevelRecordApi, form))
        {
            yield return www.SendWebRequest();
        }

    }

    public string getLeaderBoardApi() { return leaderBoardApi; }


    [System.Serializable]
    public class LevelRecord
    {
        public string username;
        public string time;
        public string time_cost;
        public int line_cost;
    }

}