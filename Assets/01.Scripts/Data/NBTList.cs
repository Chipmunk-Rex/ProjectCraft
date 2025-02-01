using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NBTList : NBT
{
    public NBTType ListType { get; set; }
    public List<NBT> List { get; set; }
    public NBT this[int index]
    {
        get
        {
            return List[index];
        }
        set
        {
            List[index] = value;
        }
    }
    public NBTList(string name, NBTType type) : base(name, NBTType.List, null)
    {
        ListType = type;
        List = new List<NBT>();
    }
}
