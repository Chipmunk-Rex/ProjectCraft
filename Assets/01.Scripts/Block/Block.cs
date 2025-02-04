using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    MeshData meshData;
    BlockSO blockSO;

    public Block(BlockSO blockSO)
    {
        this.blockSO = blockSO;
    }

    internal MeshData GetBlockMeshData(ChunkData chunkData, Vector3Int localPosition, MeshData meshData)
    {
        
    }

    internal class FaceDirection
    {
    }
}
