using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeshData : ScriptableObject
{
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector3> colliderVertices = new List<Vector3>();
    public List<int> colliderTriangles = new List<int>();
    public List<Vector2> uv = new List<Vector2>();
    public MeshData waterMesh;
    private bool isMainMesh => waterMesh != null;
    public void AddTriangle(Vector3[] newVertices, Vector2[] newUVs)
    {
        int index = vertices.Count;
        vertices.AddRange(newVertices);
        uv.AddRange(newUVs);
        triangles.Add(index);
        triangles.Add(index + 1);
        triangles.Add(index + 2);
    }
    public void AddQuad(Vector3[] newVertices, Vector2[] newUVs)
    {
        int index = vertices.Count;
        vertices.AddRange(newVertices);
        uv.AddRange(newUVs);
        triangles.Add(index);
        triangles.Add(index + 1);
        triangles.Add(index + 2);
        triangles.Add(index + 2);
        triangles.Add(index + 3);
        triangles.Add(index);
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();
        return mesh;
    }

    public void AddVertex(Vector3 vertex, bool vertexGeneratesCollider)
    {
        vertices.Add(vertex);
        if (vertexGeneratesCollider)
        {
            colliderVertices.Add(vertex);
        }

    }

    internal void AddQuadTriangles(object generatesCollider)
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);

        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        if (quadGeneratesCollider)
        {
            colliderTriangles.Add(colliderVertices.Count - 4);
            colliderTriangles.Add(colliderVertices.Count - 3);
            colliderTriangles.Add(colliderVertices.Count - 2);
            colliderTriangles.Add(colliderVertices.Count - 4);
            colliderTriangles.Add(colliderVertices.Count - 2);
            colliderTriangles.Add(colliderVertices.Count - 1);
        }
    }

    public MeshData(bool isMainMesh)
    {
        if (isMainMesh)
        {
            waterMesh = new MeshData(false);
        }
    }
}
