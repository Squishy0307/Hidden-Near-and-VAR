using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InractibleItems : MonoBehaviour
{
    [SerializeField] Transform TrashCan;
    [SerializeField] Transform Behive;
    [SerializeField] Transform SinkWater;

    public void TrashCanFall()
    {
        TrashCan.DORotate(new Vector3(5.277f, -90f, 0f), 0.5f);
    }

    public void BeeHiveFall()
    {
        Behive.DORotate(new Vector3(99.55f, -180f, 0f), 0.5f);
        Behive.DOMove(new Vector3(2.75f, 4.118f, 6.259f),0.5f).SetEase(Ease.Linear).OnComplete(EnableBeeParticles);
    }

    public void EnableBeeParticles()
    {
        Behive.GetChild(0).gameObject.SetActive(true);
    }

    public void SinkFilled()
    {
        Debug.Log("HAJKSDH");
        SinkWater.DOLocalMoveY(2.7f, 1.5f).SetEase(Ease.OutSine);
    }
}
