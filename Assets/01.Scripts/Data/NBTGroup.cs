using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBTGroup : NBT
{
    public NBTType GroupType { get; set; }
    public Dictionary<string, NBT> Group { get; set; }
    public NBT this[string key]
    {
        get
        {
            return Group[key];
        }
        set
        {
            Group[key] = value;
        }
    }
    public void Add(string key, NBT value)
    {
        value.Name = key;
        Group.Add(key, value);
    }
    public void Add(NBT value)
    {
        Group.Add(value.Name, value);
    }
    public NBT Get(string key)
    {
        return Group.ContainsKey(key) ? Group[key] : null;
    }
    public NBTGroup(string name, NBTType type) : base(name, NBTType.Group, null)
    {
        GroupType = type;
        Group = new Dictionary<string, NBT>();
    }
}
