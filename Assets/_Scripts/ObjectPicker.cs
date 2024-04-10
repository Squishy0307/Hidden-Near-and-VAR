using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class ObjectPicker : MonoBehaviour
{
    private Camera cam;
    [SerializeField] LayerMask intractableLayer;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, intractableLayer))
            {
                FoundAnObject(hit.transform.gameObject);
            }
        }
    }

    public void FoundAnObject(GameObject g)
    {
        g.SetActive(false);
        ObjectTracker.Instance.objectFound(g);
    }
}
