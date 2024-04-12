using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class IntractableObject : MonoBehaviour
{
    private bool isFound = false;
    private Vector3 scale;
    [SerializeField] float pickUpScale = 0.65f;
    public UnityEvent onIntract;

    private void Start()
    {
        scale = new Vector3(transform.localScale.x + pickUpScale, transform.localScale.y + pickUpScale, transform.localScale.z + pickUpScale);
    }

    public void ItemIntracted()
    {
        onIntract.Invoke();

        if(transform.gameObject.layer == LayerMask.NameToLayer("Picker"))
        {
            ItemFound();
        }
    }

    public void ItemFound()
    {
        if (!isFound)
        {
            isFound = true;
            transform.DOScale(scale, 0.15f).SetEase(Ease.OutSine).SetLoops(2,LoopType.Yoyo).OnComplete(destoryMe);
        }

    }

    void destoryMe()
    {
        Destroy(gameObject);
    }
}
