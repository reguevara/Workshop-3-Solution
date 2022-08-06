// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

public class Triangle : SceneEntity
{
    [SerializeField] private Vector3 v1, v2, v3;

    public override RaycastHit? Intersect(Ray ray)
    {
        // Use a custom written intersection function.
        // Uncomment below once this is done (e.g. from your project):
        //return MyIntersect(ray);

        // Use Unity engine for collisions.
        return UnityIntersect(ray);
    }

    private RaycastHit? MyIntersect(Ray ray)
    {
        // Check whether ray intersects with sphere.
        // Left as an exercise, but see the plane for an example.

        return null;
    }
    
    public Vector3[] Vertices()
    {
        return new[] { this.v1, this.v2, this.v3 };
    }
}
