using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSO : BaseBlockSO
{
    public string blockName;
    public int blockID;
    public bool generatesCollider;
    public int textureOffset = 1;
    public int textureSizeX { get; set; } = 1;
    public int textureSizeY { get; set; } = 1;
    public Block GenerateBlock()
    {
        Block block = new Block(this);
        return block;
    }
}
