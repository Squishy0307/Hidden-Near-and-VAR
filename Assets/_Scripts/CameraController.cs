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

    [SerializeField] float ScrollSpd = 200f; 
    public UnityEvent onRotation;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {

        cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpd * 10 * Time.deltaTime;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 20, 60);

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
        if (other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
        {
            other.GetComponent<Wall>().Disapper();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
        {
            other.GetComponent<Wall>().Apper();
        }
    }

}
