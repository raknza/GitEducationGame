
using UnityEngine;
using System.Collections;

using UnityEngine.Networking;

public class StudentEventManager : MonoBehaviour
{
    [SerializeField]
    string logEventApi;

    string jwtToken;
    string username;

    public void logStudentEvent(string eventName, string event_content)
    {
        StartCoroutine(logEvent(eventName,event_content));
    }

    IEnumerator logEvent(string eventName, string event_content)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", username);

        form.AddField("eventName", eventName);

        form.AddField("event", event_content);


        using (UnityWebRequest www = UnityWebRequest.Post(logEventApi, form))
        {
            yield return www.SendWebRequest();

            Debug.Log("event passed : " + eventName + "\n" + event_content);
        }

    }

    public void SetJwtToken(string jwtToken)
    {
        this.jwtToken = jwtToken;
    }

    public void setUsername(string username)
    {
        this.username = username;
    }



}
