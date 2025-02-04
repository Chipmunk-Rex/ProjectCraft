using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData
{
    public Block[] blocks;
    public int ChunkSize = 16;
    public int ChunkHeight = 256;
    public Vector3Int position;
    private World world;

    public ChunkData(int chunkSize, int chunkHeight, World world, Vector3Int position)
    {
        ChunkSize = chunkSize;
        ChunkHeight = chunkHeight;
        this.world = world;
        this.position = position;
    }

    public bool Modified { get; set; }
}
