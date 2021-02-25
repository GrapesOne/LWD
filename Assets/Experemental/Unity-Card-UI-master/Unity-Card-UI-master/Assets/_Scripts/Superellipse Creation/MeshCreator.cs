using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Credit where credit is due
///https://wiki.unity3d.com/index.php?title=Triangulator  
[ExecuteInEditMode]
public class MeshCreator : MonoBehaviour
{
    public static Mesh CreateMesh(List<Vector2> points)
    {
        // Create Vector2 vertices
        var vertices2D = points.ToArray();

        // Use the triangulator to get indices for creating triangles
        var tr = new Triangulator(vertices2D);
        var indices = tr.Triangulate();

        // Create the Vector3 vertices
        var vertices = new Vector3[vertices2D.Length];
        for (var i = 0; i < vertices.Length; i++)
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        foreach (var i in indices)
        {
            Debug.Log(i);
        }
        // Create the mesh
        var msh = new Mesh {vertices = vertices, triangles = indices};
        msh.RecalculateNormals();
        msh.RecalculateBounds();
        return msh;
    }
    public void CreateMeshAndSet(List<Vector2> points)
    {
        // Set up game object with mesh;
        GetComponent<MeshFilter>().mesh = CreateMesh(points);
    }
    
}