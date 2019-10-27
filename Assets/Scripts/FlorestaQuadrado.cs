using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class FlorestaQuadrado : MonoBehaviour
{

    LineRenderer lineRenderer;

    public Vector2 size = new Vector2(1, 1);

    public float[] cornerVelocities;
    public float[] cornerAngles = new float[4];

    public float cornerDistortionFactor = 0.01f;

    public float randRot;

    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {

        size = new  Vector2(Random.Range(1f, 2f), Random.Range(1f, 2f));

        lineRenderer = GetComponent<LineRenderer>();

        cornerVelocities = new float[]{
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        };

        randRot = Random.Range(-20f, 20f);

        GetComponent<MeshRenderer>().material.SetColor("_MeshColor", Random.ColorHSV());
        
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {

        //Quaternion quat = transform.rotation;
        //quat.eulerAngles = new Vector3(quat.eulerAngles.x, quat.eulerAngles.y, quat.eulerAngles.z + randRot * Time.deltaTime);
        //transform.rotation = quat;

        for(int i = 0; i < 4; i ++){
            cornerAngles[i] += cornerVelocities[i] * Time.deltaTime;
        }

        lineRenderer = GetComponent<LineRenderer>();

        Vector3[] points = new Vector3[] {
            new Vector3(-size.x, -size.y) + new Vector3(Mathf.Cos(cornerAngles[0]), Mathf.Sin(cornerAngles[0]))*cornerDistortionFactor,
            new Vector3(-size.x, size.y) + new Vector3(Mathf.Cos(cornerAngles[1]), Mathf.Sin(cornerAngles[1]))*cornerDistortionFactor,
            new Vector3(size.x, size.y) + new Vector3(Mathf.Cos(cornerAngles[2]), Mathf.Sin(cornerAngles[2]))*cornerDistortionFactor,
            new Vector3(size.x, -size.y) + new Vector3(Mathf.Cos(cornerAngles[3]), Mathf.Sin(cornerAngles[3]))*cornerDistortionFactor
        };
        
        for(int i = 0; i < points.Length; i ++){
            points[i].z = 0;
        }

        mesh.vertices = points;
        mesh.triangles = new int[] {0, 1, 2, 2, 3, 0};

        lineRenderer.SetPositions(points.Select( (Vector3 v)  => transform.rotation * v + transform.position ).ToArray());

    }

    public void CreateFilledGraphShape (Vector3[] linePoints) {
        Vector3[] filledGraphPoints = new Vector3[linePoints.Length * 2]; // one point below each line point
        for(int j = 0; j < linePoints.Length; j ++){
            filledGraphPoints[2 * j] = new Vector3(linePoints[j].x, 0, 0);
            filledGraphPoints[2 * j + 1] = linePoints[j];
        }
 
        int numTriangles = (linePoints.Length -1) * 2;
        int[] triangles = new int[numTriangles * 3]; 
    
        int i = 0;
        for (int t = 0; t < numTriangles; t += 2) {
            // lower left triangle
            triangles[i++] = 2 * t;
            triangles[i++] = 2 * t + 1;
            triangles[i++] = 2 * t +2;
            // upper right triangle - you might need to experiment what are the correct indices
            triangles[i++] = 2 * t + 1;
            triangles[i++] = 2 * t + 2;
            triangles[i++] = 2 * t + 3;
        }
    
        // create mesh
        Mesh filledGraphMesh = GetComponent<MeshFilter>().mesh;
        filledGraphMesh.vertices = filledGraphPoints;
        filledGraphMesh.triangles = triangles;
        // you might need to assign texture coordinates as well
    
        // create game object and add renderer and mesh to it
        
    }
}
