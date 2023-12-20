using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ActionName
{
}
public class EventManager : Singleton<EventManager>
{
    private Dictionary<ActionName, Action<Dictionary<string, object>>> m_Events = new Dictionary<ActionName, Action<Dictionary<string, object>>>();
    private void Awake()
    {
        base.Awake();
    }
    
    public void StartListening(ActionName name, Action<Dictionary<string, object>> action)
    {
        if (m_Events.ContainsKey(name))
        {
            m_Events[name] += action;
        }
        else
        {
            m_Events.Add(name, action);
        }
    }
    public void StopListening(ActionName name, Action<Dictionary<string, object>> action)
    {
        if (m_Events.ContainsKey(name))
        {
            m_Events[name] -= action;
        }
    }
    public void TriggerAction(ActionName name, Dictionary<string, object> action)
    {
        if (m_Events.ContainsKey(name))
        {
            m_Events[name]?.Invoke(action);
        }
    }
}