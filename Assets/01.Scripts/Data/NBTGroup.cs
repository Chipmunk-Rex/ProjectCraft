using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public new void Serialize(BinaryWriter writer)
    {
        foreach (var tag in Group.Values)
        {
            tag.Serialize(writer);
        }
        writer.Write((byte)NBTType.End);
    }

    // 역직렬화
    public static NBTGroup Deserialize(BinaryReader reader, string name)
    {
        NBTGroup compound = new NBTGroup(name);

        while (true)
        {
            NBTType type = (NBTType)reader.ReadByte(); // 태그 타입 읽기
            if (type == NBTType.End) break; // 종료 태그 확인

            NBT nbtData = NBT.Deserialize(reader); // 태그 역직렬화
            compound.Add(nbtData);
        }

        return compound;
    }
    public NBTGroup(string name) : base(name, NBTType.Group, null)
    {
        Group = new Dictionary<string, NBT>();
    }
}
