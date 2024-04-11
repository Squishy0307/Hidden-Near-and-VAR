using UnityEngine;
using DG.Tweening;

public class Wall : MonoBehaviour
{
    private MeshRenderer mesh;
    private Material mat;
    private float dittherAmount = 2;

    private Tween AppearTween;
    private Tween DisapperTween;

    [SerializeField] bool StartDisapper = false;

    void Start()
    {
        mesh = transform.GetComponent<MeshRenderer>();
        mat = mesh.material;

        if (StartDisapper)
        {
            dittherAmount = 2;
        }
        else
        {
            dittherAmount = 0;
        }
    }

    private void Update()
    {
        mat.SetFloat("_Ditther_Amount", dittherAmount);
    }

    public void Apper()
    {
        DisapperTween.Kill();
        enableMesh();
        AppearTween = DOTween.To(() => dittherAmount, x => dittherAmount = x, 0f, 0.35f);
    }

    public void Disapper()
    {
        AppearTween.Kill();
        DisapperTween = DOTween.To(() => dittherAmount, x => dittherAmount = x, 2f, 0.35f).OnComplete(disableMesh);
    }

    void enableMesh()
    {
        mesh.enabled = true;
    }
    void disableMesh()
    {
        mesh.enabled = false;
    }

}
