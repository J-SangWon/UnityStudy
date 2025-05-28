using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshHighlighter : MonoBehaviour
{
    [SerializeField] List<SkinnedMeshRenderer> meshesToHighlight;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material defaultMaterial;


    public void HighlightMesh(bool highlight)
    {
        foreach (var mesh in meshesToHighlight)
        {
                mesh.material = highlight ? highlightMaterial : defaultMaterial;
        }
    }
}
