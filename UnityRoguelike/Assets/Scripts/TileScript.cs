using Dungeon;
using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour
{
    private int tileId;
    private Material tileMaterial;

    void Start () {
        Util.FixCubeUv(this.gameObject);
	}

    public void SetTileId(int id)
    {
        tileMaterial.mainTextureOffset = Util.GetTileUvOffset(id);
        tileId = id;
    }

	void Update ()
	{
	}

    public void SetMaterial(Material tileMaterial)
    {
        this.tileMaterial = tileMaterial;
        var mr = GetComponent<MeshRenderer>();
        mr.material = tileMaterial;
    }

}
