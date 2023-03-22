using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    List<Observer> observers = new List<Observer>();

    public void Notify(string name)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnNotify(name);
        }
    }

    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(Observer observer)
    {

    }
}

