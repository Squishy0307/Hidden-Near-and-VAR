using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public static ObjectTracker Instance;

    public List<GameObject> objectToFind;

    void Start()
    {
        Instance = this;
    }
    
    public void objectFound(GameObject g)
    {
        for (int i = 0; i < objectToFind.Count; i++)
        {
            if (objectToFind[i].name == g.name)
            {
                objectToFind.RemoveAt(i);
            }
        }

        if(objectToFind.Count <= 0)
        {
            Debug.Log("All Object found");
        }
    }
}
