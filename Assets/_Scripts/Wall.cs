using UnityEngine;
using DG.Tweening;

public class Wall : MonoBehaviour
{
    private Material mat;
    private float dittherAmount = 2;

    private Tween AppearTween;
    private Tween DisapperTween;

    [SerializeField] bool StartDisapper = false;

    void Start()
    {
        mat = transform.GetComponent<MeshRenderer>().material;

        if (StartDisapper)
        {
            dittherAmount = 0;
        }
        else
        {
            dittherAmount = 2;
        }
    }

    private void Update()
    {
        mat.SetFloat("_Ditther_Amount", dittherAmount);
    }

    public void Apper()
    {
        DisapperTween.Kill();
        AppearTween = DOTween.To(() => dittherAmount, x => dittherAmount = x, 2f, 0.35f);
    }

    public void Disapper()
    {
        AppearTween.Kill();
        DisapperTween = DOTween.To(() => dittherAmount, x => dittherAmount = x, 0f, 0.35f);
    }

}
