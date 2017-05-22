using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//refactor into single class?
public class MeshGen {

    public static MeshData GenerateTerrain(float[,] heightMap, float heightMultiplier)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[x, y] * heightMultiplier, topLeftZ - y); //adds a world point relative to the noise map
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height); //texture wrapping point

                if (x < width-1 && y < height - 1)
                {
                    //terrain is a sheet, series of squares made up of two triangles of point A B C and A D C or some varaint
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width); 
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        meshData.CreateMesh();
        return meshData;
    }

}
//hold meshData, not necessary currently but easier to scale with rather than directly adding data to the attached mesh
public class MeshData
{
    public Vector3[] vertices; //worldpoints of mesh to be connected
    public int[] triangles; //triangles wound through the vertices drawing the mesh
    public Vector2[] uvs; //points for texture wrapping

    int triangleIndex;

    public Mesh mesh;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1)*6];
        uvs = new Vector2[meshWidth * meshHeight];
        mesh = new Mesh();
    }
   
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public void CreateMesh()
    {
        //Mesh meshNew = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}