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
        StartCoroutine(getLevelLeaderboard(levelIndex + 1));
        index.text = "";
        username.text = "";
        timeCost.text = "";
        lineCost.text = "";
        time.text = "";
    }

    IEnumerator getLevelLeaderboard(int level)
    {
        head.text = "±Æ¦æº] - Level " + level;
        WWWForm form = new WWWForm();


        using (UnityWebRequest www = UnityWebRequest.Get(leaderBoardApi + "?level=" + level))
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            string jsonString = JsonHelper.fixJson(www.downloadHandler.text);
            LevelRecord[] leaderboardRecords = JsonHelper.FromJson<LevelRecord>(jsonString);
            for(int i = 0; i< leaderboardRecords.Length; i++)
            {
                index.text = index.text + (i + 1) + "." + "\n";
                username.text = username.text + leaderboardRecords[i].username + "\n";
                timeCost.text = timeCost.text + leaderboardRecords[i].time_cost + "¬í\n";
                lineCost.text = lineCost.text + leaderboardRecords[i].line_cost + "¦æ\n";
                time.text = time.text + leaderboardRecords[i].time + "\n";
            }
        }

    }

    [System.Serializable]
    public class LevelRecord
    {
        public string username;
        public string time;
        public string time_cost;
        public int line_cost;
    }

}