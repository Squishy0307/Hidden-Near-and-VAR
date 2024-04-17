using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeSway : MonoBehaviour
{
    private MeshRenderer m_MeshRenderer;
    private Material m_Material;
    float shake = 0.1f;

    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_Material = m_MeshRenderer.material;

        float xSway = Random.Range(0, 15f);
        float ySway = Random.Range(0, 15f);

        m_Material.SetVector("_Direction", new Vector3(xSway,ySway,0));

    }

    private void Update()
    {
        m_Material.SetFloat("_BendControl", shake);
    }

    public void treeShake()
    {
        Debug.Log("Shake");
        DOTween.To(() => shake, x => shake = x, 1f, 0.3f).SetLoops(2,LoopType.Yoyo);
    }

}
