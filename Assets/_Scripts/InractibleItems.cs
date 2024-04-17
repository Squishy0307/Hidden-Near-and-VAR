using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InractibleItems : MonoBehaviour
{
    [SerializeField] Transform TrashCan;
    [SerializeField] Transform Behive;
    [SerializeField] Transform SinkWater;
    [SerializeField] Transform bonfireParticles;
    [SerializeField] Transform coconut;
    [SerializeField] Transform treasureChest;
    [SerializeField] Transform treasureChest2;
    private bool beesFound;
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
        SinkWater.DOLocalMoveY(2.7f, 1.5f).SetEase(Ease.OutSine);
    }

    public void shakeBehive()
    {
        if (!beesFound)
        {
            beesFound = true;
            Sequence b = DOTween.Sequence();

            b.Append(Behive.DORotate(new Vector3(0, -180, 45), 0.3f).SetEase(Ease.OutSine));
            b.Append(Behive.DORotate(new Vector3(0, -180, -25), 0.4f).SetEase(Ease.OutSine));
            b.Append(Behive.DORotate(new Vector3(0, -180, 0), 0.2f).SetEase(Ease.OutSine));
        }
    }

    public void BonfireShake()
    {
        Sequence b = DOTween.Sequence();

        b.Append(bonfireParticles.DORotate(new Vector3(-140f, 0, 0), 0.3f).SetEase(Ease.OutSine));
        b.Append(bonfireParticles.DORotate(new Vector3(-40, 0, 0), 0.4f).SetEase(Ease.OutSine));
        b.Append(bonfireParticles.DORotate(new Vector3(-90, 0, 0), 0.2f).SetEase(Ease.OutSine));
    }

    public void CoconutFall()
    {
        coconut.DOLocalMoveY(-2.436f, 1f).SetEase(Ease.Linear);
    }

    public void OpenChest()
    {
        Debug.Log("Open seseme");
        treasureChest.DOLocalRotate(Vector3.zero, 0.5f);
    }
    public void OpenChest2()
    {
        treasureChest2.DOLocalRotate(Vector3.zero, 0.5f);
    }
}
