using System.Collections;
using System.Collections.Generic;
using System.IO;
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


    public new void Serialize(BinaryWriter writer)
    {
        writer.Write((byte)ListType); // 리스트 타입 쓰기
        writer.Write(List.Count); // 리스트 크기 쓰기

        foreach (var tag in List)
        {
            tag.Serialize(writer); // 각 태그 직렬화
        }
    }
    public static NBTList Deserialize(BinaryReader reader, string name)
    {
        NBTType listType = (NBTType)reader.ReadByte(); // 리스트 타입 읽기
        int count = reader.ReadInt32(); // 리스트 크기 읽기

        NBTList list = new NBTList(name, listType);
        for (int i = 0; i < count; i++)
        {
            list.List.Add(NBT.Deserialize(reader)); // 각 태그 역직렬화
        }

        return list;
    }


    public NBTList(string name, NBTType type) : base(name, NBTType.List, null)
    {
        ListType = type;
        List = new List<NBT>();
    }
}
