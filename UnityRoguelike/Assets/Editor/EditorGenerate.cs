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
