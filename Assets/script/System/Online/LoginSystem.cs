using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{

    [SerializeField]
    string loginApi;
    [SerializeField]
    GameObject loginDialogue;
    [SerializeField]
    Button startButton;
    [SerializeField]
    Button chapterButton;

    [SerializeField]
    InputField username;
    [SerializeField]
    InputField password;
    [SerializeField]
    Text loginStatus;
    [SerializeField]
    Text token;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loginDialogueShow()
    {
        loginDialogue.SetActive(true);
    }

    public void login()
    {
        StartCoroutine(loginAuth());
    }

    IEnumerator loginAuth()
    {
        WWWForm form = new WWWForm();
        if (username.text.Equals("") || password.text.Equals(""))
        {
            loginStatus.text = "登入狀態：密碼錯誤或帳號不存在";
            yield return null;
        }
        else
        {
            form.AddField("username", username.text);
            form.AddField("password", password.text);

            using (UnityWebRequest www = UnityWebRequest.Post(loginApi, form))
            {
                yield return www.SendWebRequest();

                //Debug.Log(www.result.ToString());
                loginJson loginResult = JsonUtility.FromJson<loginJson>(www.downloadHandler.text);
                if (loginResult.status)
                {
                    loginStatus.text = "登入狀態：登入成功";
                    startButton.interactable = true;
                    chapterButton.interactable = true;
                    loginDialogue.SetActive(false);
                    GameSystemManager.GetSystem<StudentEventManager>().SetJwtToken(loginResult.token);
                    GameSystemManager.GetSystem<StudentEventManager>().setUsername(username.text);
                }
                else
                {
                    loginStatus.text = "登入狀態：密碼錯誤或帳號不存在";
                    startButton.interactable = false;
                    chapterButton.interactable = false;
                }
                //token.text = www.downloadHandler.text;
                //Debug.Log(token.text);
            }
        }
    }

    class loginJson
    {
        public bool status;
        public string token;
    }
}
