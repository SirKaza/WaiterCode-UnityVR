using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public abstract class Observer
{
    public abstract void OnNotify(string name);
}

public class AchievementsController : Observer
{
    public AchievementsController()
    {


    }

    public override void OnNotify(string name)
    {

    }

   
}
