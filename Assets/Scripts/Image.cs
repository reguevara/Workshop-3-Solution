// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Image : MonoBehaviour
{
    [SerializeField] private int imageWidth;
    [SerializeField] private int imageHeight;
    [SerializeField] private bool drawPixelGrid = true;
    [SerializeField] private MeshRenderer output;
    private bool _dirty;

    private Color?[,] _image;
    private Texture2D _imageTexture;

    public int Width => this.imageWidth;
    public int Height => this.imageHeight;

    private void Awake()
    {
        this._image = new Color?[this.imageWidth, this.imageHeight];
        this._imageTexture = new Texture2D(this.imageWidth, this.imageHeight)
        {
            filterMode = FilterMode.Point
        };

        if (this.drawPixelGrid)
            GetComponent<MeshFilter>().mesh = CreatePixelGridMesh();

        GeneratePixels();
    }

    private void Update()
    {
        if (this._dirty) this._imageTexture.Apply();
    }

    public void SetPixel(int x, int y, Color color)
    {
        this._imageTexture.SetPixel(x, y, color);
        this._dirty = true;
    }

    private void GeneratePixels()
    {
        for (var y = 0; y < this.imageHeight; y++)
        for (var x = 0; x < this.imageWidth; x++)
            this._imageTexture.SetPixel(x, y, this._image[x, y] ?? new Color(0f, 0f, 0f, 0f));
        this._imageTexture.Apply();

        this.output.material.mainTexture = this._imageTexture;
        this.output.enabled = true;
    }

    private Mesh CreatePixelGridMesh()
    {
        var mesh = new Mesh
        {
            name = "PixelGridMesh"
        };

        var vertices = new List<Vector3>();
        var colors = new List<Color>();
        var lines = new List<int>();

        var addLine = new Action<Vector3, Vector3, Color?>((a, b, color) =>
        {
            vertices.AddRange(new[] { a, b });
            colors.AddRange(Enumerable.Repeat(color ?? Color.white, 2));
            lines.AddRange(Enumerable.Range(vertices.Count - 2, 2));
        });

        var origin = new Vector3(-0.5f, -0.5f);

        // Vertical pixel grid lines
        for (var x = 0; x < this.imageWidth + 1; x++)
        {
            var xt = (float)x / this.imageWidth;
            var a = origin + Vector3.right * xt;
            var b = origin + Vector3.right * xt + Vector3.up;
            addLine(a, b, Color.red);
        }

        // Horizontal pixel grid lines
        for (var y = 0; y < this.imageHeight + 1; y++)
        {
            var yt = (float)y / this.imageHeight;
            var a = origin + Vector3.up * yt;
            var b = origin + Vector3.up * yt + Vector3.right;
            addLine(a, b, Color.red);
        }

        mesh.SetVertices(vertices);
        mesh.SetColors(colors);
        mesh.SetIndices(lines, MeshTopology.Lines, 0);

        return mesh;
    }
}
