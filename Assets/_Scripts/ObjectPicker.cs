using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log(hit.transform.name);
                ObjectTracker.Instance.objectFound(hit.transform.gameObject);
            }
        }
    }
}
