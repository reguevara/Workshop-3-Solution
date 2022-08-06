// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class SceneEntity : MonoBehaviour
{
    public abstract RaycastHit? Intersect(Ray ray);

    public Color Color()
    {
        return GetComponent<MeshRenderer>().material.color;
    }
}
