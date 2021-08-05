using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsSystem : MonoBehaviour
{
    [SerializeField]
    Button leftButton;
    [SerializeField]
    Button rightButton;
    [SerializeField]
    Text pageText;

    [SerializeField]
    int count;

    int nowPage = 0;

    [SerializeField]
    List<string> dialouge;

    [SerializeField]
    Text dialougeText;
    [SerializeField]
    List<Sprite> sprites;
    [SerializeField]
    float dialougeWaitSeconds;

    [SerializeField]
    Image image;

    string dialougeString;
    int dialougeStringIndex;
    bool waitEnd;

    // Start is called before the first frame update
    void Start()
    {

        leftButton.onClick.AddListener( delegate
        {
            PrePage();
         }
        );
        rightButton.onClick.AddListener(delegate
        {
            NextPage();
        }
        );
        pageText.text = (nowPage+1) + "/" +  count;
        dialougeString = dialouge[nowPage];
        if (sprites[nowPage])
        {
            image.sprite = sprites[nowPage];
        }

        waitEnd = true;
        GameObject tipsObject = gameObject;
        tipsObject.SetActive(false);
        GameSystemManager.GetSystem<TimerManager>().Add(new Timer(4, TipsStart, tipsObject));
    }

    public void TipsStart(object obj)
    {
        GameObject gobj = (GameObject)obj;
        gobj.SetActive(true);
    }



    private void Update()
    {
        dialougeText.text = dialougeString.Substring(0,dialougeStringIndex);
        if ( dialougeStringIndex < dialougeString.Length && waitEnd == true)
        {
            StartCoroutine(WaitForDialouge());
        }
        if (sprites[nowPage])
        {
            image.color = new Color(1, 1, 1, Mathf.Lerp(image.color.a, 1, 0.04f));
        }
        else
        {
            image.color = new Color(0,0,0,0);
        }
    }

    void PrePage()
    {
        if ( nowPage !=0 )
        {
            nowPage--;
            pageText.text = (nowPage + 1) + "/" + count;
            image.color = new Color(1, 1, 1, 0);
            dialougeString = dialouge[nowPage];
            if (sprites[nowPage])
            {
                image.sprite = sprites[nowPage];
            }
            dialougeStringIndex = 0;
            waitEnd = true;
        }

    }

    void NextPage()
    {
        if (nowPage < count - 1 )
        {
            nowPage++;
            pageText.text = (nowPage + 1) + "/" + count;
            image.color = new Color(1, 1, 1, 0);
            dialougeString = dialouge[nowPage];
            if (sprites[nowPage])
            {
                image.sprite = sprites[nowPage];
            }
            dialougeStringIndex = 0;
            waitEnd = true;
        }
    }

    IEnumerator WaitForDialouge()
    {
        waitEnd = false;
        dialougeStringIndex++;
        yield return new WaitForSeconds(dialougeWaitSeconds);
        waitEnd = true;
    }
}
