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
    public static NBT Deserialize(BinaryReader reader)
    {
        NBTType type = (NBTType)reader.ReadByte(); 
        string name = reader.ReadString(); 

        switch (type)
        {
            case NBTType.Byte:
                return new NBT(name, type, reader.ReadByte());
            case NBTType.Short:
                return new NBT(name, type, reader.ReadInt16());
            case NBTType.Int:
                return new NBT(name, type, reader.ReadInt32());
            case NBTType.Long:
                return new NBT(name, type, reader.ReadInt64());
            case NBTType.Float:
                return new NBT(name, type, reader.ReadSingle());
            case NBTType.Double:
                return new NBT(name, type, reader.ReadDouble());
            case NBTType.String:
                return new NBT(name, type, reader.ReadString());
            case NBTType.Byte_Array:
                int byteArrayLength = reader.ReadInt32();
                byte[] byteArray = reader.ReadBytes(byteArrayLength);
                return new NBT(name, type, byteArray);
            case NBTType.Int_Array:
                int intArrayLength = reader.ReadInt32();
                int[] intArray = new int[intArrayLength];
                for (int i = 0; i < intArrayLength; i++) intArray[i] = reader.ReadInt32();
                return new NBT(name, type, intArray);
            case NBTType.Long_Array:
                int longArrayLength = reader.ReadInt32();
                long[] longArray = new long[longArrayLength];
                for (int i = 0; i < longArrayLength; i++) longArray[i] = reader.ReadInt64();
                return new NBT(name, type, longArray);
            case NBTType.List:
                return NBTList.Deserialize(reader, name);
            case NBTType.Group:
                return NBTGroup.Deserialize(reader, name);
            default:
                throw new NotSupportedException($"Unsupported tag type: {type}");
        }
    }
}
