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
    [SerializeField] GameObject dust;
    [SerializeField] GameObject starsVFX;

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

                GameObject s = Instantiate(starsVFX,hit.point, Quaternion.identity);
                s.transform.LookAt(cam.transform);
                Destroy(s,2f);

                //dust.transform.eulerAngles = hit.normal;
                //starsVFX.SendEvent("Fire1");
                //starsVFX.SetVector3("Pos", hit.point);
                //starsVFX.SetVector3("Rotation", hit.normal);
                return;
            }

            if (Physics.Raycast(ray, out hit2, 500))
            {
                GameObject d = Instantiate(dust, hit2.point, Quaternion.identity);
                d.transform.LookAt(cam.transform);
                Destroy(d, 2f);

                //dust.transform.eulerAngles = hit2.normal;
                //dust.SendEvent("Fire");
                //dust.SetVector3("Pos", hit2.point);
                //dust.SetVector3("Rotation", hit2.normal);

            }
        }
    }

    public void FoundAnObject(GameObject g)
    {
        IntractableObject o = g.GetComponent<IntractableObject>();
        o.ItemFound();

        ObjectTracker.Instance.objectFound(g);
    }
}
