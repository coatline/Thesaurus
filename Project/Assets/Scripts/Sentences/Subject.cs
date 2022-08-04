using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Subject", fileName = "New Subject")]

public class Subject : ScriptableObject
{
    public bool properNoun;
    public List<Subject> connections;
    public SentenceFragment[] descriptors;

    public void OnValidate()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            if (!connections[i].connections.Contains(this))
            {
                connections[i].connections.Add(this);
            }
        }
    }

    public string GetName
    {
        get
        {
            string n = name;

            if (properNoun)
            {
                n = "The " + name;
            }
            else
            {
                n.ToLower();
            }

            return n;
        }
    }

    public Subject GetRandomConnection()
    {
        if (connections.Count == 0) { return null; }
        return connections[Random.Range(0, connections.Count)];
    }

    public SentenceFragment GetRandomDescriptors()
    {
        return descriptors[Random.Range(0, descriptors.Length)];
    }
}