using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolygonTool;

public class T : MonoBehaviour
{
    //用物体的坐标来代替点
    public List<Transform> tList;
    //计算得到的三角形序列下标
    private List<int> resultList = new List<int>();

    private Triangulation triangulation;

    private void Start()
    {
        List<Vector3> posList = new List<Vector3>();
        for (int i = 0; i < tList.Count; i++)
        {
            posList.Add(tList[i].position);
        }

        triangulation = new Triangulation(posList);

        triangulation.SetCompareAxle(CompareAxle.Y);

        int[] a = triangulation.GetTriangles();

        if (a != null)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Debug.Log("===:" + a[i]);
                resultList.Add(a[i]);
            }
        }


        GameObject go = new GameObject();

        MeshFilter mf = go.AddComponent<MeshFilter>();

        go.AddComponent<MeshRenderer>();

        Mesh m = new Mesh();

        Vector3[] vertexs = new Vector3[a.Length];

        for (int i = 0; i < vertexs.Length; i++)
        {
            Vector3 v = tList[a[i]].position;
            vertexs[i] = v;
        }

        m.vertices = vertexs;
        
        int[] tri = new int[a.Length];

        for (int i = 0; i < tri.Length; i += 3)
        {
            tri[i] = i;
            tri[i + 1] = i + 2;
            tri[i + 2] = i + 1;
        }

        m.triangles = tri;

        mf.mesh = m;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < tList.Count; i++)
        {
            if (i < tList.Count - 1)
            {
                Gizmos.DrawLine(tList[i].position, tList[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(tList[i].position, tList[0].position);
            }

        }

        Gizmos.color = Color.black;

        for (int i = 0; i < resultList.Count; i+=3)
        {
            int startIndex = resultList[i];
            int endIndex = resultList[i + 2];
            Gizmos.DrawLine(tList[startIndex].position, tList[endIndex].position);
        }
    }

}
