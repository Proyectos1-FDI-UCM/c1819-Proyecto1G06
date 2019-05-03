using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class TextMeshChangeLayer : MonoBehaviour {

    [SerializeField]
    public string sortingLayer;
    [SerializeField]
    public int orderInLayer;

    private void Awake()
    {
        ApplyLayer();
    }

    public void OnValidate()
    {
        ApplyLayer();
    }

    void ApplyLayer()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.sortingLayerName = sortingLayer;
        mesh.sortingOrder = orderInLayer;
    }
}
