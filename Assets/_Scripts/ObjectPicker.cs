using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class ObjectPicker : MonoBehaviour
{
    private Camera cam;
    [SerializeField] LayerMask intractableLayer;
    [SerializeField] VisualEffect dust;

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
            RaycastHit hit2;

            if (Physics.Raycast(ray, out hit, 500,intractableLayer))
            {
                FoundAnObject(hit.transform.gameObject);
            }

            if(Physics.Raycast(ray, out hit2, 500))
            {
                dust.SendEvent("Fire");
                dust.SetVector3("Pos", hit2.point);
            }
        }
    }

    public void FoundAnObject(GameObject g)
    {
        g.SetActive(false);
        ObjectTracker.Instance.objectFound(g);
    }
}
