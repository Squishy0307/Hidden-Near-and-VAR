using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

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

                audio_manager.Instance.Play("ItemFound");

                return;
            }

            if (Physics.Raycast(ray, out hit2, 500))
            {
                GameObject d = Instantiate(dust, hit2.point, Quaternion.identity);
                d.transform.LookAt(cam.transform);
                Destroy(d, 2f);

                IntractableObject obj;
                if (hit2.transform.TryGetComponent<IntractableObject>(out obj))
                {
                    obj.ItemIntracted();
                }

                audio_manager.Instance.Play("Bloop");

            }
        }
    }

    public void FoundAnObject(GameObject g)
    {
        IntractableObject o = g.GetComponent<IntractableObject>();
        //o.ItemFound();
        o.ItemIntracted();

        ObjectTracker.Instance.objectFound(g);
    }
}
