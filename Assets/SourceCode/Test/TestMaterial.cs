using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections;

public class TestMaterial : MonoBehaviour {

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private int[] triangles;

    private List<Vector3> list;

    private int count;

    public Material material;
 

    void Awake()
    {
        //mesh = new Mesh();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        mesh = meshFilter.mesh;
        meshRenderer.material.color = Color.green;
        meshRenderer.material.shader = Shader.Find("Transparent/Diffuse");
        mesh.Clear();

        list = new List<Vector3>();
        MeshUtils.InitStaticValues();

    }

	// Use this for initialization
	void Start () {
	
	}

    static private List<Vector3> vertices = new List<Vector3>();
    static private List<Vector2> uvs = new List<Vector2>();
    static private List<Vector2> uvs2 = new List<Vector2>();
    static private List<Vector3> normals = new List<Vector3>();
    static private List<Color> colors = new List<Color>();

    static private List<int> trianglesNormal = new List<int>();
    static private List<int> trianglesTransparent = new List<int>();
    static private List<int> trianglesTranslucid = new List<int>();
    static private List<int> trianglesDamage = new List<int>();
	
	// Update is called once per frame
	void Update () {
        mesh.Clear();

        colors.Clear();
        vertices.Clear();
        uvs.Clear();
        uvs2.Clear();
        normals.Clear();
        trianglesNormal.Clear();
        trianglesTransparent.Clear();
        trianglesTranslucid.Clear();
        trianglesDamage.Clear();

        int index = 0;
        float uvdelta = 1.0f / GraphicsUnity.TILE_PER_MATERIAL_ROW;

        List<int> triangles;
        Vector3[] faceVectors;

        triangles = trianglesNormal;
        //TODO: Implemented liquid
        //if (drawMode == TileDefinition.DrawMode.LIQUID)
        //    faceVectors = MeshUtils.faceVectorsLiquid[0];
        //else
        faceVectors = MeshUtils.faceVectorsNormal;

        for (int face = 0; face < 6; face++)
        {
            int materialInt = 1;
            if (materialInt < 0)
                continue;

            Color faceColor = new Color(1.0f * MeshUtils.faceBright[face],
                                         1.0f * MeshUtils.faceBright[face],
                                         1.0f * MeshUtils.faceBright[face]);

            Vector3 faceNormal = MeshUtils.faceNormals[face];

            for (int i = 0; i < 4; i++)
            {
                vertices.Add(faceVectors[(face << 2) + i]);
                normals.Add(faceNormal);
                colors.Add(faceColor);
            }

            triangles.Add(index + 0);
            triangles.Add(index + 1);
            triangles.Add(index + 2);

            triangles.Add(index + 2);
            triangles.Add(index + 3);
            triangles.Add(index + 0);

            float uvx = uvdelta * (materialInt % GraphicsUnity.TILE_PER_MATERIAL_ROW);
            float uvy = 1.0f - uvdelta * (materialInt / GraphicsUnity.TILE_PER_MATERIAL_ROW);

            uvs.Add(new Vector2(uvx, uvy - uvdelta));
            uvs.Add(new Vector2(uvx, uvy));
            uvs.Add(new Vector2(uvx + uvdelta, uvy));
            uvs.Add(new Vector2(uvx + uvdelta, uvy - uvdelta));

            uvs2.Add(Vector2.zero);
            uvs2.Add(Vector2.zero);
            uvs2.Add(Vector2.zero);
            uvs2.Add(Vector2.zero);

            index += 4;
        }

        mesh.vertices = vertices.ToArray();
        mesh.colors = colors.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.uv2 = uvs2.ToArray();
        mesh.subMeshCount = 4;

        List<Material> materials = new List<Material>();

        if (trianglesNormal.Count > 0)
            materials.Add(material);

        meshRenderer.sharedMaterials = materials.ToArray();

        int trianglesGroupIndex = 0;

        if (trianglesNormal.Count > 0)
            mesh.SetTriangles(trianglesNormal.ToArray(), trianglesGroupIndex++);
        if (trianglesTranslucid.Count > 0)
            mesh.SetTriangles(trianglesTranslucid.ToArray(), trianglesGroupIndex++);
        if (trianglesTransparent.Count > 0)
            mesh.SetTriangles(trianglesTransparent.ToArray(), trianglesGroupIndex++);
        if (trianglesDamage.Count > 0)
            mesh.SetTriangles(trianglesDamage.ToArray(), trianglesGroupIndex++);

    }
}
 