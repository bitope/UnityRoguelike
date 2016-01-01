using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorGenerate : EditorWindow {

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Custom Generate")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(EditorGenerate));
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Materials for each imported sprite."))
        {
            CreateAllMaterials();
        }

        //if (GUILayout.Button("Create doublesided Quad."))
        //{
        //    CreateDoubleSidedQuad();
        //}

        //if (GUILayout.Button("Create crossed doublesided quads."))
        //{
        //    CrossMesh();
        //}
    }

    private void CreateDoubleSidedQuad()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),

            new Vector3(0.5f, 0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
        };

        Vector2[] uv = new Vector2[]
        {
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(0, 0),

            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),
        };

        Vector2[] uvunflipped = new Vector2[]
        {
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(0, 0),

            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0),
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3,

            4, 5, 6,
            4, 6, 7
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        AssetDatabase.CreateAsset(mesh, "Assets/Automated/DoublesidedQuad.asset");

        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvunflipped;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        AssetDatabase.CreateAsset(mesh, "Assets/Automated/DoublesidedQuadUnflippedUV.asset");
    }

    private void CrossMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),

            new Vector3(0.5f, 0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),

            new Vector3(0.0f, 0.5f, 0.5f),
            new Vector3(0.0f, -0.5f, 0.5f),
            new Vector3(0.0f, 0.5f, -0.5f),
            new Vector3(0.0f, -0.5f, -0.5f),

            new Vector3(0.0f, 0.5f, 0.5f),
            new Vector3(0.0f, 0.5f, -0.5f),
            new Vector3(0.0f, -0.5f, -0.5f),
            new Vector3(0.0f, -0.5f, 0.5f),
        };

        Vector2[] uv = new Vector2[]
        {
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(0, 0),

            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),

            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(0, 0),

            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3,

            4, 5, 6,
            4, 6, 7,

            8,9,10,
            10,9,11,

            12,13,14,
            12,14,15
        };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        AssetDatabase.CreateAsset(mesh, "Assets/Automated/CrossMesh.asset");
    }

    private void CreateAllMaterials()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Automated/Materials/Items"))
            AssetDatabase.CreateFolder("Assets/Automated/Materials", "Items");

        var x = AssetDatabase.FindAssets("", new[] { "Assets/Graphics/Sprites/Weapons" });

        foreach (var s in x)
        {
            var image = AssetDatabase.GUIDToAssetPath(s);
            var t = AssetDatabase.LoadAssetAtPath<Texture2D>(image) as Texture2D;

            if (t == null)
                continue;

            t.alphaIsTransparency = true;
            t.filterMode = FilterMode.Point;
            
            Material m = AssetDatabase.LoadAssetAtPath<Material>("Assets/Automated/Materials/Items/mat_" + t.name + ".mat");
            bool doCreate = false;
            if (m == null)
            {
                doCreate = true;
                m = new Material(Shader.Find("Standard"));
            }

            m.name = "mat_" + t.name;
            m.mainTexture = t;
            m.SetFloat("_Mode", 1.0f); // Set to Cutout.
            m.SetFloat("_Glossiness", 0.0f); //Smoothness
            m.SetFloat("_Cutoff", 0.5f); // Alpha cutoff
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            //m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;

            if (doCreate)
                AssetDatabase.CreateAsset(m, "Assets/Automated/Materials/Items/" + m.name + ".mat");
            
            AssetDatabase.SaveAssets();
        }
        
    }
}
