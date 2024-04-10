using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectTracker : MonoBehaviour
{
    public static ObjectTracker Instance;
    public GameObject itemsText;
    public Transform itemsTextHolderCanvas;
    public List<GameObject> objectToFind;

    void Start()
    {
        Instance = this;
        Vector3 pos = new Vector3(-823f, 560, 0); 

        for (int i = 0; i < objectToFind.Count; i++)
        {
            pos.y -= 55;
            TextMeshProUGUI t = Instantiate(itemsText, pos, Quaternion.identity, itemsTextHolderCanvas).GetComponent<TextMeshProUGUI>();
            t.rectTransform.localPosition = pos;
            t.name = objectToFind[i].name;
            t.text = objectToFind[i].name;
        }
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

        for (int i = 0; i < itemsTextHolderCanvas.childCount; i++)
        {
            if (itemsTextHolderCanvas.GetChild(i).name == g.name)
            {
                itemsTextHolderCanvas.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (objectToFind.Count <= 0)
        {
            Debug.Log("All Object found");
        }
    }
}
