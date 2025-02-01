using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NBT
{
    public string Name { get; set; }
    public NBTType Type { get; set; }
    public object Value { get; set; }

    public NBT(string name, NBTType type, object value)
    {
        Name = name;
        Type = type;
        Value = value;
    }
    public override string ToString()
    {
        return $"Name: {Name}, Type: {Type}, Value: {Value}";
    }
    public void Serialize(BinaryWriter writer)
    {
        writer.Write((byte)Type); // 태그 타입 쓰기
        writer.Write(Name); // 태그 이름 쓰기

        switch (Type)
        {
            case NBTType.Byte:
                writer.Write((byte)Value);
                break;
            case NBTType.Short:
                writer.Write((short)Value);
                break;
            case NBTType.Int:
                writer.Write((int)Value);
                break;
            case NBTType.Long:
                writer.Write((long)Value);
                break;
            case NBTType.Float:
                writer.Write((float)Value);
                break;
            case NBTType.Double:
                writer.Write((double)Value);
                break;
            case NBTType.String:
                writer.Write((string)Value);
                break;
            case NBTType.Byte_Array:
                byte[] byteArray = (byte[])Value;
                writer.Write(byteArray.Length);
                writer.Write(byteArray);
                break;
            case NBTType.Int_Array:
                int[] intArray = (int[])Value;
                writer.Write(intArray.Length);
                foreach (int i in intArray) writer.Write(i);
                break;
            case NBTType.Long_Array:
                long[] longArray = (long[])Value;
                writer.Write(longArray.Length);
                foreach (long l in longArray) writer.Write(l);
                break;
            case NBTType.List:
                ((NBTList)this).Serialize(writer);
                break;
            case NBTType.Group:
                ((NBTGroup)this).Serialize(writer);
                break;
            default:
                throw new NotSupportedException($"Unsupported tag type: {Type}");
        }
    }
}
