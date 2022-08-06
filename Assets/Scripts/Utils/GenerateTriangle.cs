// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class GenerateTriangle : MonoBehaviour
{
    [SerializeField] private Vector3 v1, v2, v3;

    private void Start()
    {
        var mesh = CreateMesh();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private Mesh CreateMesh()
    {
        var mesh = new Mesh
        {
            name = "Triangle"
        };

        mesh.SetVertices(new[] { this.v1, this.v2, this.v3 });
        mesh.SetIndices(new[] { 0, 1, 2 }, MeshTopology.Triangles, 0);

        return mesh;
    }
}
