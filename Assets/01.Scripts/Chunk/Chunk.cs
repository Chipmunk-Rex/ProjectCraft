using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public static void LoopThroughTheBlocks(ChunkData chunkData, Action<Vector3Int, Block> action)
    {
        for (int index = 0; index < chunkData.blocks.Length; index++)
        {
            Vector3Int localPosition = GetPositionFormIndex(chunkData, index);
            Block block = chunkData.blocks[index];
            action(localPosition, block);
        }
    }
    public static Vector3Int GetPositionFormIndex(ChunkData chunkData, int index)
    {
        int x = index % chunkData.ChunkSize;
        int y = (index / chunkData.ChunkSize) % chunkData.ChunkHeight;
        int z = index / (chunkData.ChunkSize * chunkData.ChunkHeight);
        return new Vector3Int(x, y, z);
    }
    public static bool InRange(ChunkData chunkData, Vector3Int localPosition)
    {
        Debug.Log("Local position: " + localPosition);
        if (localPosition.x < 0 || localPosition.x >= chunkData.ChunkSize)
            return false;
        if (localPosition.y < 0 || localPosition.y >= chunkData.ChunkHeight)
            return false;
        if (localPosition.z < 0 || localPosition.z >= chunkData.ChunkSize)
            return false;
        return true;
    }
    private static bool InRange(ChunkData chunkData, int axisCoordinate)
    {
        if (axisCoordinate < 0 || axisCoordinate >= chunkData.ChunkSize)
            return false;

        return true;
    }
    public static void SetBlock(ChunkData chunkData, Vector3Int localPosition, Block block)
    {
        if (InRange(chunkData, localPosition))
        {
            int index = GetIndexFromPosition(chunkData, localPosition);
            chunkData.blocks[index] = block;
        }
        else
        {
            Debug.LogError("Block out of range");
        }
    }
    public static Block GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int localPosition)
    {
        if (InRange(chunkData, localPosition))
        {
            int index = GetIndexFromPosition(chunkData, localPosition);
            Debug.Log("Index: " + index);
            return chunkData.blocks[index];
        }
        else
        {
            Debug.LogError("Block out of range");
            return chunkData.world.GetBlockFromChunkCoordinates(chunkData, localPosition);
        }
    }
    public static int GetIndexFromPosition(ChunkData chunkData, Vector3Int localPosition)
    {
        return localPosition.x + localPosition.y * chunkData.ChunkSize + localPosition.z * chunkData.ChunkSize * chunkData.ChunkHeight;
    }
    public static Vector3Int GetBlockInChunkCoordinates(ChunkData chunkData, Vector3Int localPosition)
    {
        return new Vector3Int(localPosition.x % chunkData.ChunkSize, localPosition.y % chunkData.ChunkHeight, localPosition.z % chunkData.ChunkSize);
    }
    public static Vector3Int GetChunkPositionFromBlockCoords(int chunkSize, int chunkHeight, Vector3Int cordinate)
    {
        Vector3Int pos = new Vector3Int
        {
            x = Mathf.FloorToInt(cordinate.x / (float)chunkSize) * chunkSize,
            y = Mathf.FloorToInt(cordinate.y / (float)chunkHeight) * chunkHeight,
            z = Mathf.FloorToInt(cordinate.z / (float)chunkSize) * chunkSize
        };
        return pos;
    }
    public static MeshData GetChunkMeshData(ChunkData chunkData)
    {
        MeshData meshData = new MeshData(true);

        LoopThroughTheBlocks(chunkData, (localPosition, block) =>
        {
            meshData = block.GetBlockMeshData(chunkData, localPosition, meshData);
        });
        return meshData;
    }
}




