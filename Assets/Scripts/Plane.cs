// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Plane : SceneEntity
{
    public override RaycastHit? Intersect(Ray ray)
    {
        // Using a custom written intersection function (e.g. from your project):
        //return TestMyIntersect(ray);

        // Using Unity engine for ray-plane(/mesh) collisions instead:
        var plane = GetComponent<MeshCollider>();
        var isHit = plane.Raycast(ray, out var hit, float.PositiveInfinity);
        return isHit ? hit : null;
    }

    private RaycastHit? TestMyIntersect(Ray ray)
    {
        // Check whether ray intersects with plane
        var normal = new Vector3(0, 1, 0); // Note this is hardcoded!
        var center = transform.position;

        var denom = Vector3.Dot(ray.direction, normal);
        if (Mathf.Abs(denom) > float.Epsilon)
        {
            var t = Vector3.Dot(center - ray.origin, normal) / denom;
            if (t > float.Epsilon)
            {
                var hitPosition = ray.GetPoint(t);
                return new RaycastHit
                {
                    distance = (hitPosition - center).magnitude
                };
            }
        }

        return null;
    }
}
