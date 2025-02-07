using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public MeshData meshData { get; set; }
    public BlockSO blockSO { get; set; }
    public Sprite sprite { get; set; }
    public Block(BlockSO blockSO)
    {
        this.blockSO = blockSO;
    }

    public MeshData GetBlockMeshData(ChunkData chunkData, Vector3Int localPosition, MeshData meshData)
    {
        meshData = BlockExtension.GetMeshData(chunkData, localPosition.x, localPosition.y, localPosition.z, meshData);
        return meshData;
    }

    internal class FaceDirection
    {
    }
}
