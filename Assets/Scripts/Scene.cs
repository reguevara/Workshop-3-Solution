// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System;
using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private FOVAxisType fovAxis = FOVAxisType.Vertical;
    [SerializeField] [Range(1, 179)] private float fov = 60f;

    private float _imagePlaneHeight;
    private float _imagePlaneWidth;

    private void Start()
    {
        ComputeWorldImageBounds();
        EmbedImageInWorld();

        // Perform ray tracing to render the image.
        // This is meant to mimic the Render() method in project 1.
        Render();
    }

    private void Update()
    {
        // Here you may use Unity "debug rays" to visualise rays in the scene.

        // Frustum corner rays first.
        const float dist = 10.0f; // How 'far' the rays are drawn.
        Debug.DrawRay(Vector3.zero, NormalizedImageToWorldCoord(0f, 0f) * dist, Color.blue);
        Debug.DrawRay(Vector3.zero, NormalizedImageToWorldCoord(0f, 1f) * dist, Color.blue);
        Debug.DrawRay(Vector3.zero, NormalizedImageToWorldCoord(1f, 0f) * dist, Color.blue);
        Debug.DrawRay(Vector3.zero, NormalizedImageToWorldCoord(1f, 1f) * dist, Color.blue);
        
        // Add rays here...
    }

    private void Render()
    {
        for (var y = 0; y < this.image.Height; y++)
        for (var x = 0; x < this.image.Width; x++)
        {
            var rayOrigin = Vector3.zero;
            var rayDirection = GetPixelCenterWorldCoord(x, y);
            var ray = new Ray(rayOrigin, rayDirection);

            this.image.SetPixel(x, y, Color.black);
            RaycastHit? nearestHit = null;
            foreach (var sceneEntity in FindObjectsOfType<SceneEntity>())
            {
                var hit = sceneEntity.Intersect(ray);
                if (hit != null && (hit?.distance < nearestHit?.distance || nearestHit == null))
                {
                    this.image.SetPixel(x, y, sceneEntity.Color());
                    nearestHit = hit;
                }
            }
        }
    }

    private Vector3 GetPixelCenterWorldCoord(int x, int y)
    {
        var pixelX = (x + 0.5f) / this.image.Width;
        var pixelY = (y + 0.5f) / this.image.Height;

        return NormalizedImageToWorldCoord(pixelX, pixelY);
    }

    private Vector3 NormalizedImageToWorldCoord(float x, float y)
    {
        return new Vector3(
            this._imagePlaneWidth * (x - 0.5f),
            this._imagePlaneHeight * (0.5f - y),
            1.0f); // Image plane is 1 unit from camera.
    }

    private void ComputeWorldImageBounds()
    {
        var aspectRatio = (float)this.image.Width / this.image.Height;
        var fovLength = Mathf.Tan(this.fov / 2f * Mathf.Deg2Rad) * 2f;

        switch (this.fovAxis)
        {
            case FOVAxisType.Vertical:
                this._imagePlaneHeight = fovLength;
                this._imagePlaneWidth = this._imagePlaneHeight * aspectRatio;
                break;
            case FOVAxisType.Horizontal:
                this._imagePlaneWidth = fovLength;
                this._imagePlaneHeight = this._imagePlaneWidth / aspectRatio;
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void EmbedImageInWorld()
    {
        this.image.transform.position = Vector3.forward;
        this.image.transform.localScale = new Vector3(this._imagePlaneWidth, this._imagePlaneHeight, 0f);
    }

    private enum FOVAxisType
    {
        Horizontal,
        Vertical
    }
}
