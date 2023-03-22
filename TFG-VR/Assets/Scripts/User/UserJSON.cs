using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class UserJSON 
{
    public string name;
    public string password;
    public int currentDay;
    public DataLevel dataLevel;
    public AchievementsJSON achievements;
    public bool tutorialChefCompleted;
    public bool tutorialWaiterCompleted;
    public string volume;
    public string dataCollection;

}
