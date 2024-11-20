using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    [SerializeField] private bool _changeClothesColor;
    [SerializeField] private SkinnedMeshRenderer[] _clothesMeshRenderer;

    private List<Material> _materials;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.OnChangePlayerColor += SetColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColor(Color color)
    {
        //MaterialPropertyBlock materialPropertyBlock not good with URP
        foreach (Material mat in _skinnedMeshRenderer.materials)
        {
            mat.SetColor("_BaseColor", color);
        }

        if (!_changeClothesColor)
            return;

        foreach (SkinnedMeshRenderer mesh in _clothesMeshRenderer)
        {
            foreach (Material mat in mesh.materials)
            {
                mat.SetColor("_BaseColor", color);
            }
        }
    }
}
