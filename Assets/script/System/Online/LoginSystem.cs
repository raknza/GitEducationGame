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
    Button loginButton;

    [SerializeField]
    InputField username;
    [SerializeField]
    InputField password;
    [SerializeField]
    Text loginStatus;
    [SerializeField]
    Text token;

    private void Start()
    {
        loginButton.onClick.AddListener(loginDialogueShow);
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
            loginStatus.text = "�n�J���A�G�K�X���~�αb�����s�b";
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
                    loginStatus.text = "�n�J���A�G�n�J���\";
                    loginButton.GetComponentInChildren<Text>().text = "�n�X�b��";
                    startButton.interactable = true;
                    chapterButton.interactable = true;
                    loginDialogue.SetActive(false);
                    GameSystemManager.GetSystem<StudentEventManager>().setJwtToken(loginResult.token);
                    GameSystemManager.GetSystem<StudentEventManager>().setUsername(username.text);
                    loginButton.onClick.RemoveAllListeners();
                    loginButton.onClick.AddListener(logout);
                }
                else
                {
                    loginStatus.text = "�n�J���A�G�K�X���~�αb�����s�b";
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

    public void logout()
    {
        GameSystemManager.GetSystem<StudentEventManager>().logout();
        loginStatus.text = "�n�J���A�G�w�n�X";
        startButton.interactable = false;
        chapterButton.interactable = false;
        loginButton.GetComponentInChildren<Text>().text = "�n�J�b��";
        loginButton.onClick.RemoveAllListeners();
        loginButton.onClick.AddListener(loginDialogueShow);
        username.text = "";
        password.text = "";
    }
}
