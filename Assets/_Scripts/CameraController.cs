using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    float ZoomAmount = 0;
    public float MaxToClamp = 1;
    public float ROTSpeed = 6.5f;

    float xPos;
    float currentYrot;
    private bool canRotate = true;
    private Camera cam;

    [SerializeField] float dragThreshold = 20f;
    [SerializeField] float wallDetectionRadius = 10f;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] AnimationCurve rotationEase;

    public UnityEvent onRotation;

    private void Start()
    {
         cam = GetComponent<Camera>();
    }

    void Update()
    {
        ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
        float translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));
        gameObject.transform.Translate(0, 0, translate * ROTSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));


        if (Input.GetMouseButtonDown(1))
        {
            xPos = Input.mousePosition.x;
        }

        if(Input.GetMouseButtonUp(1))
        {

            if(xPos - Input.mousePosition.x >= dragThreshold)
            {
                if (canRotate)
                {
                    canRotate = false;
                    currentYrot -= 90f;

                    transform.parent.DORotate(new Vector3(0,currentYrot,0), 0.5f).SetEase(rotationEase).OnComplete(CanRotaateCam);

                    onRotation.Invoke();
                }
            }
            else if(xPos - Input.mousePosition.x <= -dragThreshold)
            {
                if (canRotate)
                {
                    canRotate = false;
                    currentYrot += 90f;

                    transform.parent.DORotate(new Vector3(0,currentYrot,0), 0.5f).SetEase(rotationEase).OnComplete(CanRotaateCam);

                    onRotation.Invoke();
                }
            }
        }

    }

    public void CanRotaateCam()
    {
        canRotate = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            other.GetComponent<Wall>().Disapper();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            other.GetComponent<Wall>().Apper();
        }
    }

}
