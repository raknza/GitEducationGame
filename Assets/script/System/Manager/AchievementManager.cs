using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Achievement
{
    public string name;
    public string description;
    public Sprite icon;

}

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    AchievementAnimation achievementObject;

    [SerializeField]
    List<Achievement> achievements;

    public string testInput;
    public bool testSwitch;

    public void achieve(string name)
    {
        Achievement achieve = achievements.Find(achievement => achievement.name == name);
        if (achieve != null)
        {
            achievementObject.popup(achieve.icon, achieve.description);
        }
    }

    private void Update()
    {
        if (testSwitch)
        {
            achieve(testInput);
            testSwitch = false;
        }
    }
}
