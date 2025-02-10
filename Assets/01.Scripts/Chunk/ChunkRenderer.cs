using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ChunkRenderer : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    Mesh mesh;

#if UNITY_EDITOR
    public bool showGizmos = false;
#endif
    public ChunkData ChunkData { get; private set; }
    public bool Modified { get => ChunkData.Modified; set => ChunkData.Modified = value; }
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        mesh = meshFilter.mesh;
    }

    private void RenderMesh(MeshData meshData)
    {
        mesh.Clear();

        mesh.subMeshCount = 2;
        mesh.vertices = meshData.vertices.Concat(meshData.waterMesh.vertices).ToArray();

        mesh.SetTriangles(meshData.triangles.ToArray(), 0);
        mesh.SetTriangles(meshData.triangles.Select(value => value + meshData.vertices.Count).ToArray(), 1);

        mesh.uv = meshData.uv.Concat(meshData.waterMesh.uv).ToArray();
        mesh.RecalculateNormals();

        Mesh collisionMesh = new Mesh();
        collisionMesh.Clear();
        collisionMesh.vertices = meshData.colliderVertices.ToArray();
        collisionMesh.triangles = meshData.colliderTriangles.ToArray();
        meshCollider.sharedMesh = collisionMesh;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            if (!Application.isPlaying || ChunkData == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(ChunkData.ChunkSize, ChunkData.ChunkHeight, ChunkData.ChunkSize));
        }
    }

    internal void InitializeChunk(ChunkData data)
    {
        this.ChunkData = data;
    }

    internal void UpdateChunk(MeshData meshData)
    {
        RenderMesh(meshData);
    }
#endif
}
