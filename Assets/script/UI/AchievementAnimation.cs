using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementAnimation : MonoBehaviour
{

    public bool testSwitch;

    public bool popuping { get; private set; } = false;
    public bool hiding { get; private set; } = false;
    [SerializeField]
    private int speed;
    [SerializeField]
    private float stopTime;
    [SerializeField]
    Image icon;
    [SerializeField]
    Text description;

    // Update is called once per frame
    void Update()
    {
        if (testSwitch)
        {
            popup();
            testSwitch = false;
        }
        if (popuping)
        {
            if (transform.localPosition.y >= -407)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -407, transform.localPosition.z);
                popuping = false;
                GameSystemManager.GetSystem<TimerManager>().Add(new Timer(stopTime, hide));
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * speed, transform.localPosition.z);
            }
        }
        if (hiding)
        {
            if (transform.localPosition.y <= -495)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -495, transform.localPosition.z);
                hiding = false;
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * -speed, transform.localPosition.z);
            }
        }
    }


    public void popup(Sprite icon, string description)
    {
        this.icon.sprite = icon;
        this.description.text = description;
        popup();
    }
    private void popup()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, -495 , transform.localPosition.z);
        popuping = true;
    }

    void hide(object obj)
    {
        hiding = true;
    }
}
