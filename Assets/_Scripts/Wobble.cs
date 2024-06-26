using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wobble : MonoBehaviour
{
    [SerializeField] float wobble = 1;

    Renderer rend;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 angularVelocity;
    public float MaxWobble = 0.03f;
    public float WobbleSpeed = 1f;
    public float Recovery = 1f;
    float wobbleAmountX;
    float wobbleAmountZ;
    float wobbleAmountToAddX;
    float wobbleAmountToAddZ;
    float pulse;
    float time = 0.5f;
    bool isWobbling = false;

    bool isEmpty = false;
    private float fillAmount;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();

        fillAmount = rend.material.GetFloat("_Liquid_Amount");
    }
    private void Update()
    {
        time += Time.deltaTime;
        // decrease wobble over time
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (Recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * (Recovery));

        // make a sine wave of the decreasing wobble
        pulse = 2 * Mathf.PI * WobbleSpeed;
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);

        // send it to the shader
        rend.material.SetFloat("_WobbleX", wobbleAmountX);
        rend.material.SetFloat("_WobbleZ", wobbleAmountZ);

        // velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;


        // add clamped velocity to wobble
        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        // keep last position
        lastPos = transform.position * wobble;
        lastRot = transform.rotation.eulerAngles * wobble;

        //set fill amount
        rend.material.SetFloat("_Liquid_Amount", fillAmount);
    }

    public void CamRotationWobble()
    {
        if (!isWobbling)
        {
            isWobbling = true;
            DOTween.To(() => wobble, x => wobble = x, 1.02f, 0.05f).SetLoops(2, LoopType.Yoyo).OnComplete(wobbleComplete);
        }
    }
    void wobbleComplete()
    {
        isWobbling = false;
    }

    public void EmptyContainer()
    {
        if (!isEmpty)
        {
            isEmpty = true;

            DOTween.To(() => fillAmount, x => fillAmount = x, 0.62f, 3f);
        }
    }
}
