using Dungeon;
using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour
{
    private int tileId;
    private Material tileMaterial;
    //private int a = 0;

	// Use this for initialization
	void Start () {
        Util.FixCubeUv(this.gameObject);
	}

    public void SetTileId(int id)
    {
        tileMaterial.mainTextureOffset = Util.GetTileUvOffset(id);
        tileId = id;
    }

	// Update is called once per frame
	void Update ()
	{
	    //a++;
        //SetTileId(52+(a%4));
	    
        //SetTileId(tileId);
	}

    public void SetMaterial(Material tileMaterial)
    {
        this.tileMaterial = tileMaterial;
        var mr = GetComponent<MeshRenderer>();
        mr.material = tileMaterial;
    }
}
