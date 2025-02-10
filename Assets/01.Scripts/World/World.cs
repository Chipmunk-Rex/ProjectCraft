using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int chunkSize = 16;
    public int chunkHeight = 256;
    public GameObject chunkPrefab;

    Dictionary<Vector3Int, ChunkData> chunks = new Dictionary<Vector3Int, ChunkData>();
    Dictionary<Vector3Int, ChunkRenderer> chunkRenderers = new Dictionary<Vector3Int, ChunkRenderer>();

    [SerializeField] BlockSO grassBlockSO;

    [SerializeField] int worldSize = 100;
    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        foreach (ChunkRenderer chunkRenderer in chunkRenderers.Values)
        {
            Destroy(chunkRenderer);
        }
        chunkRenderers.Clear();
        chunks.Clear();
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                ChunkData data = new ChunkData(chunkSize, chunkHeight, this, new Vector3Int(x * chunkSize, 0, y * chunkSize));
                Debug.Log(Chunk.GetPositionFormIndex(data, 0));
                GenerateVoxels(data);
                chunks.Add(data.position, data);
            }
        }

        foreach (ChunkData data in chunks.Values)
        {
            MeshData meshData = Chunk.GetChunkMeshData(data);
            GameObject chunkObject = Instantiate(chunkPrefab, data.position, Quaternion.identity);
            ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();
            chunkRenderers.Add(data.position, chunkRenderer);
            chunkRenderer.InitializeChunk(data);
            chunkRenderer.UpdateChunk(meshData);

        }
    }
    int tryCount = 0;
    internal Block GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int localPosition)
    {
        Vector3Int pos = chunkData.position + localPosition;
        Vector3Int chunkPos = Chunk.GetChunkPositionFromBlockCoords(chunkSize, chunkHeight, pos);
        if (chunks.TryGetValue(chunkPos, out ChunkData data))
        {
            Debug.Log(localPosition);
            Debug.Log(pos);
            if (tryCount++ > 50)
            {
                Debug.LogError("Infinite loop");
                return null;
            }
            return Chunk.GetBlockFromChunkCoordinates(data, localPosition);
        }
        return null;
    }

    private void GenerateVoxels(ChunkData data)
    {
        data.blocks = new Block[chunkSize * chunkHeight * chunkSize];
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                float noiseValue = Mathf.PerlinNoise((x + data.position.x) / 10f, (z + data.position.z) / 10f) * 10;
                int groundHeight = Mathf.FloorToInt(noiseValue * chunkHeight);
                for (int y = 0; y < chunkHeight; y++)
                {
                    if (y < groundHeight)
                    {
                        Block block = grassBlockSO.GenerateBlock();
                        Chunk.SetBlock(data, new Vector3Int(x, y, z), block);
                    }
                }
            }
        }
    }
}
