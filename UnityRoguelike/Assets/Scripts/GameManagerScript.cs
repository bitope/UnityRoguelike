﻿using Dungeon;
using UnityEngine;
using System.Collections;
using UnityRoguelike;

public class GameManagerScript : MonoBehaviour
{
    public Color AmbientColor;

    public Material wallMaterial;
    public Material floorMaterial;
    public int seed = 20;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        bool setPlayer = true;
        var container = new GameObject("level");

		Stage stage = new Stage(23, 23);

        RoomDecorator rd = new RoomDecorator(stage);
        rd.ReadAll(Application.dataPath+"\\rooms.txt");

        Generator g = new Generator(seed);
        g.DecorateRoom += rd.DecorateRoom;

        g.numRoomTries = 500;
        g.generate(stage);

        MersenneTwister mt = new MersenneTwister((uint)seed);

        for (int y = 0; y < stage.height; y++)
        {
            for (int x = 0; x < stage.width; x++)
            {
                var tile = stage[x, y];
                //if (tile != Tiles.Wall)
                {
                    var cell = CreateFloor(x, y, Tileset.GetRandom(Tileset.f_orc));
                    cell.transform.parent = container.transform;
                    cell = CreateCeiling(x, y, Tileset.GetRandom(Tileset.f_orc));
                    cell.transform.parent = container.transform;

                    if (setPlayer && tile==Tiles.Floor)
                    {
                        setPlayer = false;
                        var vv = cell.transform.position;
                        GameObject.Find("Player").transform.position = new Vector3(vv.x, 0.15f, vv.z);
                    }
                }

                if (tile == Tiles.Wall)
                {
                    var tileId = Tileset.GetRandom(Tileset.w_orc);
                    var cell = CreateWall(x, y, tileId);
                    cell.transform.parent = container.transform;
                }

                if (tile == Tiles.ClosedDoor_EW)
                {
                    var cell = Instantiate(GameObject.Find("Door"));
                    cell.transform.position=new Vector3(x,0,y);
                    cell.transform.parent = container.transform;
                }

                if (tile == Tiles.ClosedDoor_NS)
                {
                    var cell = Instantiate(GameObject.Find("Door"));
                    cell.transform.position = new Vector3(x, 0, y);
                    cell.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                    cell.transform.parent = container.transform;
                }

                if (tile == Tiles.Pillar)
                {
                    var cell = Instantiate(GameObject.Find("crumbled_column_1"));
                    cell.name = "Pillar_" + x + "_" + y;
                    cell.transform.position = new Vector3(x, 0, y);
                    cell.transform.rotation = Quaternion.AngleAxis(45, Vector3.up);
                    cell.transform.parent = container.transform;
                }

                if (tile == Tiles.Brazier)
                {
                    var cell = CreateLight(x, y);
                    //var cell = Instantiate(GameObject.Find("crumbled_column_1"));
                    cell.name = "Brazier_" + x + "_" + y;
                    Debug.Log("Created: " + cell.name);
                    cell.transform.position = new Vector3(x, 0, y);
                    cell.transform.rotation = Quaternion.AngleAxis(45, Vector3.up);
                    cell.transform.parent = container.transform;
                }

                if (tile == Tiles.Floor)
                {
                    if (mt.OneIn(20))
                    {
                        var cell = Instantiate(GameObject.Find("Item"));
                        cell.name = "Item_" + x + "_" + y;
                        cell.transform.position = new Vector3(x, -0.0f, y);
                        //cell.transform.rotation = Quaternion.AngleAxis(45, Vector3.up);
                        cell.transform.parent = container.transform;
                    }

                    if (mt.OneIn(20))
                    {
                        var cell = Instantiate(GameObject.Find("EnemySmall"));
                        cell.name = "Rat_" + x + "_" + y;
                        cell.transform.position = new Vector3(x, 0.2f, y);
                        //cell.transform.rotation = Quaternion.AngleAxis(45, Vector3.up);
                        cell.transform.parent = container.transform;
                    }
                }
                
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject CreateWall(int x, int y, int tileid)
    {
        var c = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var tileScript = c.AddComponent<TileScript>();
        var tileMaterial = Instantiate(wallMaterial);
        tileScript.SetMaterial(tileMaterial);
        tileScript.SetTileId(tileid);
        c.transform.position = new Vector3(x, 0, y);
        c.name = "Wall_" + x + "_" + y;
        return c;
    }

    public GameObject CreateFloor(int x, int y, int tileid)
    {
        var c = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var tileScript = c.AddComponent<TileScript>();
        var tileMaterial = Instantiate(floorMaterial);
        tileScript.SetMaterial(tileMaterial);
        tileScript.SetTileId(tileid);
        c.transform.position = new Vector3(x, -1, y);
        c.name = "Floor_" + x + "_" + y;
        return c;
    }

    public GameObject CreateCeiling(int x, int y, int tileid)
    {
        var c = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var tileScript = c.AddComponent<TileScript>();
        var tileMaterial = Instantiate(floorMaterial);
        tileScript.SetMaterial(tileMaterial);
        tileScript.SetTileId(tileid);
        c.transform.position = new Vector3(x, 1, y);
        c.name = "Ceiling_" + x + "_" + y;
        return c;
    }

    public GameObject CreateLight(int x, int y)
    {
        var c = new GameObject("Light_"+x+"_"+y);
        var lightComp = c.AddComponent<Light>();
        lightComp.color = Color.yellow;
        lightComp.range = 3;
        lightComp.intensity = 5;
        lightComp.shadows = LightShadows.Soft;
        lightComp.shadowStrength = 0.5f;
        c.transform.position = new Vector3(x, .35f, y);
        return c;
    }

}
