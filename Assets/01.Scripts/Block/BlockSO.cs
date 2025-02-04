using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSO : BaseBlockSO
{
    public string blockName;
    public int blockID;

    public Block GenerateBlock()
    {
        Block block = new Block(this);
        return block;
    }
}
