// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

public class Plane : SceneEntity
{
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 normal;
    
    public override RaycastHit? Intersect(Ray ray)
    {
        // Use a custom written intersection function.
        // Uncomment below once this is done (e.g. from your project):
        return MyIntersect(ray);

        // Use Unity engine for collisions.
        //return UnityIntersect(ray);
    }

    private RaycastHit? MyIntersect(Ray ray)
    {
        // Check whether ray intersects with plane
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
    
    public Vector3 Center => this.center;
    public Vector3 Normal => this.normal;
}
