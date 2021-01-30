using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtilities : MonoBehaviour
{
    public static Vector3 GetRandomLocationOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int triangleIndex = Random.Range(0, navMeshData.indices.Length - 3);

        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[triangleIndex]], navMeshData.vertices[navMeshData.indices[triangleIndex + 1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[triangleIndex + 2]], Random.value);

        return point;
    }
}
