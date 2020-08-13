using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deformController : MonoBehaviour
{
    [Header("Vertices")]
    public Vector3 leftTopFront;
    public Vector3 rightTopFront;
    public Vector3 leftBotFront;
    public Vector3 rightBotFront;
    public Vector3 leftTopBack;
    public Vector3 rightTopBack;
    public Vector3 leftBotBack;
    public Vector3 rightBotBack;

    MeshFilter mf;
    Mesh mesh;
    MeshCollider meshCollider;
    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mf.mesh = mesh;
        meshCollider = GetComponent<MeshCollider>();
        btnDeform();
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void btnDeform(){
        Vector3[] vertices = new Vector3[]{
            // front face //
            leftTopFront,   //0
            rightTopFront,  //1
            leftBotFront,   //2
            rightBotFront,  //3

            // back face //
            rightTopBack,   //4
            leftTopBack,    //5
            rightBotBack,   //6
            leftBotBack,    //7   

            // left face //
            leftTopBack,    //8
            leftTopFront,   //9
            leftBotBack,    //10
            leftBotFront,   //11

            // right face //
            rightTopFront,  //12
            rightTopBack,   //13
            rightBotFront,  //14
            rightBotBack,   //15

            // top face //
            leftTopBack,    //16
            rightTopBack,   //17
            leftTopFront,   //18
            rightTopFront,  //19

            // bot face //
            leftBotFront,   //20
            rightBotFront,  //21
            leftBotBack,    //22
            rightBotBack    //23
                        
        };
        /* clockwise determine which side is visible */
        int[] triangles = new int[]{
            // front face //
            0,2,3,
            3,1,0,

            // back face //
            4,6,7,
            7,5,4,

            // left face //
            8,10,11,
            11,9,8,

            // right face //
            12,14,15,
            15,13,12,

            // top face //
            16,18,19,
            19,17,16,
            
            // bottom face //
            20,22,23,
            23,21,20
            
        };

        Vector2[] uvs = new Vector2[]{
            // front face //
            new Vector2(0,0.333f),
            new Vector2(0,0),
            new Vector2(0.333f,0.333f),
            new Vector2(0.333f,0),

            // back face //
            new Vector2(0.667f,0.333f),
            new Vector2(0.667f,0),
            new Vector2(1,0.333f),
            new Vector2(1,0),

            // left face //
            new Vector2(0.334f,0.666f),
            new Vector2(0.334f,0.334f),
            new Vector2(0.666f,0.666f),
            new Vector2(0.666f,0.334f),

            // right face //
            new Vector2(0.667f,0.666f),
            new Vector2(0.667f,0.334f),
            new Vector2(1,0.666f),
            new Vector2(1,0.334f),

            // top face //
            new Vector2(0.334f,0.333f),
            new Vector2(0.334f,0),
            new Vector2(0.666f,0.333f),
            new Vector2(0.666f,0),

            // bottom face //
            new Vector2(0,0.666f),
            new Vector2(0,0.334f),
            new Vector2(0.333f,0.666f),
            new Vector2(0.333f,0.334f)
        };
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
