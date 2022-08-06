// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sphere : SceneEntity
{
    public override RaycastHit? Intersect(Ray ray)
    {
        //return TestMyIntersect(ray);

        // Using Unity engine for ray-sphere collisions instead:
        var sphere = GetComponent<SphereCollider>();
        var isHit = sphere.Raycast(ray, out var hit, float.PositiveInfinity);
        return isHit ? hit : null;
    }

    private RaycastHit? TestMyIntersect(Ray ray)
    {
        // Check whether ray intersects with sphere.
        // Left as an exercise, but see the plane for an example.

        return null;
    }
}
