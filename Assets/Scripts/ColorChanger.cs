using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

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
    }
}
