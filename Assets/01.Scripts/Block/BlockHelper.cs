using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockExtension
{
    private static Direction[] directions =
    {
        Direction.foreward,
        Direction.backwards,
        Direction.left,
        Direction.right,
        Direction.up,
        Direction.down,
    };

    public static MeshData GetMeshData(ChunkData chunk, int x, int y, int z, MeshData meshData)
    {

        foreach (Direction direction in directions)
        {
            var neighbourBlockCoordinates = new Vector3Int(x, y, z) + direction.DirectionToVector();
            Debug.Log(neighbourBlockCoordinates + "  ne " + direction);
            Block neighbourBlock = Chunk.GetBlockFromChunkCoordinates(chunk, neighbourBlockCoordinates);
            if (neighbourBlock != null && true)
            {
                meshData = GetFaceDataIn(neighbourBlock, direction, chunk, x, y, z, meshData);
            }
            // if (neighbourBlockType != BlockType.Nothing && BlockDataManager.blockTextureDataDictionary[neighbourBlockType].isSolid == false)
            // {

            // if (blockType == BlockType.Water)
            // {
            //     if (neighbourBlockType == BlockType.Air)
            //         meshData.waterMesh = GetFaceDataIn(direction, chunk, x, y, z, meshData.waterMesh, blockType);
            // }
            // else
            // {
            // }

        }
        return meshData;
    }

    public static MeshData GetFaceDataIn(this Block block, Direction direction, ChunkData chunk, int x, int y, int z, MeshData meshData)
    {
        bool generatesCollider = block.blockSO.generatesCollider;
        GetFaceVertices(block, direction, x, y, z, meshData);
        meshData.AddQuadTriangles(generatesCollider);
        meshData.uv.AddRange(FaceUVs(block, direction));


        return meshData;
    }

    public static void GetFaceVertices(this Block block, Direction direction, int x, int y, int z, MeshData meshData)
    {
        bool generatesCollider = block.blockSO.generatesCollider;
        //order of vertices matters for the normals and how we render the mesh
        switch (direction)
        {
            case Direction.backwards:
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generatesCollider);
                break;
            case Direction.foreward:
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generatesCollider);
                break;
            case Direction.left:
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generatesCollider);
                break;

            case Direction.right:
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generatesCollider);
                break;
            case Direction.down:
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generatesCollider);
                break;
            case Direction.up:
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generatesCollider);
                break;
            default:
                break;
        }
    }

    public static Vector2[] FaceUVs(this Block block, Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Vector2Int tilePos = TexturePosition(block, direction);

        UVs[0] = new Vector2(block.blockSO.textureSizeX * tilePos.x + block.blockSO.textureSizeX - block.blockSO.textureOffset,
            block.blockSO.textureSizeY * tilePos.y + block.blockSO.textureOffset);

        UVs[1] = new Vector2(block.blockSO.textureSizeX * tilePos.x + block.blockSO.textureSizeX - block.blockSO.textureOffset,
            block.blockSO.textureSizeY * tilePos.y + block.blockSO.textureSizeY - block.blockSO.textureOffset);

        UVs[2] = new Vector2(block.blockSO.textureSizeX * tilePos.x + block.blockSO.textureOffset,
            block.blockSO.textureSizeY * tilePos.y + block.blockSO.textureSizeY - block.blockSO.textureOffset);

        UVs[3] = new Vector2(block.blockSO.textureSizeX * tilePos.x + block.blockSO.textureOffset,
            block.blockSO.textureSizeY * tilePos.y + block.blockSO.textureOffset);

        return UVs;
    }

    public static Vector2Int TexturePosition(Block block, Direction direction)
    {
        return Vector2Int.zero;
        // return direction switch
        // {
        //     Direction.up => block.blockSO.up,
        //     Direction.down => block.blockSO.down,
        //     Direction.left => block.blockSO.left,
        //     Direction.right => block.blockSO.right,
        //     Direction.foreward => block.blockSO.foreward,
        //     Direction.backwards => block.blockSO.backwards,
        //     _ => new Vector2Int(0, 0),
        // };
    }
}