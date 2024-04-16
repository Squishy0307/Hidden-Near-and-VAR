using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ObjectTracker : MonoBehaviour
{
    public static ObjectTracker Instance;
    public GameObject itemsText;
    public RectTransform LevelCompleteUI;
    public Transform itemsTextHolderCanvas;
    public List<GameObject> objectToFind;

    void Start()
    {
        Instance = this;
        Vector3 pos = new Vector3(0, 425, 0); 

        for (int i = 0; i < objectToFind.Count; i++)
        {
            TextMeshProUGUI t = Instantiate(itemsText, pos, Quaternion.identity, itemsTextHolderCanvas).GetComponent<TextMeshProUGUI>();
            t.rectTransform.localPosition = pos;
            t.name = objectToFind[i].name;
            t.text = objectToFind[i].name;

            pos.y -= 55;
        }
    }
    
    public void objectFound(GameObject g)
    {
        for (int i = 0; i < objectToFind.Count; i++)
        {
            if (objectToFind[i].name == g.name)
            {               
                objectToFind.RemoveAt(i);
            }
        }

        for (int i = 0; i < itemsTextHolderCanvas.childCount; i++)
        {
            if (itemsTextHolderCanvas.GetChild(i).name == g.name)
            {
                Image FillImage = itemsTextHolderCanvas.GetChild(i).GetChild(0).GetComponent<Image>();

                Sequence s = DOTween.Sequence();
                s.Append(itemsTextHolderCanvas.GetChild(i).transform.DOScale(1.4f, 0.20f).SetEase(Ease.OutSine).SetLoops(2, LoopType.Yoyo));
                s.Append(DOTween.To(() => FillImage.fillAmount, x => FillImage.fillAmount = x, 1f, 0.35f));
            }
        }

        if (objectToFind.Count <= 0)
        {
            Debug.Log("All Object found");
            LevelCompleteUI.DOLocalMoveY(0, 0.6f).SetEase(Ease.OutSine);
            itemsTextHolderCanvas.gameObject.SetActive(false);
            audio_manager.Instance.Play("LevelComplete");
        }
    }
}
