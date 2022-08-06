// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Triangle : SceneEntity
{
    public override RaycastHit? Intersect(Ray ray)
    {
        //return TestMyIntersect(ray);

        // Using Unity engine for ray-plane(/mesh) collisions instead:
        var triangle = GetComponent<MeshCollider>();
        var isHit = triangle.Raycast(ray, out var hit, float.PositiveInfinity);
        return isHit ? hit : null;
    }

    private RaycastHit? TestMyIntersect(Ray ray)
    {
        // Check whether ray intersects with sphere.
        // Left as an exercise, but see the plane for an example.

        return null;
    }
}
