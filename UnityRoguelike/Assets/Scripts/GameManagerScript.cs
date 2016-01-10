﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dungeon;
using UnityEngine;
using System.Collections;
using UnityRoguelike;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public Color AmbientColor;

    public Material wallMaterial;
    public Material floorMaterial;

    public static int seed = 20;
    public static int turnCount = 0;
    public static MersenneTwister rng = new MersenneTwister(seed);

    public static Stage stage = null;

    public static bool MouseLook;

    [HideInInspector]
    public GameObject inventoryCanvas;
    
    [HideInInspector]
    public GameObject container;

    [HideInInspector] 
    public GameObject player;

    void Awake()
    {
        Debug.Log("GM Awake called.");
        instance = this;

        SpriteResourceManager.Initialize();
        ItemFactory.Initialize();

        inventoryCanvas = GameObject.Find("InventoryCanvas");
        ToggleInventory();
    }

    void Start()
    {
        Debug.Log("GM Start called.");

        Cursor.lockState = CursorLockMode.Confined;

        bool setPlayer = true;
        container = new GameObject("level");
        player = GameObject.Find("Player");

		stage = new Stage(23, 23);

        RoomDecorator rd = new RoomDecorator(stage);
        var roomLines = Resources.Load("rooms") as TextAsset;
        rd.ReadAll(roomLines.text.Split('\n'));

        Generator g = new Generator(seed);
        g.DecorateRoom += rd.DecorateRoom;

        g.numRoomTries = 500;
        g.generate(stage);

        rng = new MersenneTwister(seed);

        var wallset = Tileset.GetRandomSet(Tileset.ws_everything);
        var floorset = Tileset.GetRandomSet(Tileset.fs_everything);
        var ceilingset = Tileset.GetRandomSet(Tileset.fs_everything);

        for (int y = 0; y < stage.height; y++)
        {
            for (int x = 0; x < stage.width; x++)
            {
                var tile = stage[x, y];
                //if (tile != Tiles.Wall)
                {
                    var cell = CreateFloor(x, y, Tileset.GetRandom(floorset));
                    cell.transform.parent = container.transform;
                    //cell = CreateCeiling(x, y, Tileset.GetRandom(ceilingset));
                    //cell.transform.parent = container.transform;

                    if (setPlayer && tile==Tiles.Floor)
                    {
                        setPlayer = false;
                        var vv = cell.transform.position;
                        GameObject.Find("Player").transform.position = new Vector3(vv.x, 0.15f, vv.z);
                        GameObject.Find("Player").SendMessage("RegisterWithStage", SendMessageOptions.DontRequireReceiver);
                        GameObject.Find("Inventory").SendMessage("RegisterWithStage", SendMessageOptions.RequireReceiver);
                    }
                }

                if (tile == Tiles.Wall)
                {
                    var tileId = Tileset.GetRandom(wallset);
                    var cell = CreateWall(x, y, tileId);
                    cell.transform.parent = container.transform;
                }

                if (tile == Tiles.ClosedDoor_EW)
                {
                    var pre = Resources.Load("Prefabs/Decoration/Door") as GameObject;
                    var cell = Instantiate(pre);
                    cell.transform.position = new Vector3(x, 0, y);
                    cell.transform.parent = container.transform;
                    var dc = cell.GetComponent<DoorController>();
                    dc.Position = new Vec(x, y);
                    dc.Tile = tile;
                }

                if (tile == Tiles.ClosedDoor_NS)
                {
                    var pre = Resources.Load("Prefabs/Decoration/Door") as GameObject;
                    var cell = Instantiate(pre);
                    cell.transform.position = new Vector3(x, 0, y);
                    cell.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                    cell.transform.parent = container.transform;
                    var dc = cell.GetComponent<DoorController>();
                    dc.Position = new Vec(x, y);
                    dc.Tile = tile;
                }

                if (tile == Tiles.Pillar)
                {
                    var pre = Resources.Load("Prefabs/Decoration/Pillar") as GameObject;
                    var cell = Instantiate(pre);
                    cell.name = "Pillar_" + x + "_" + y;
                    cell.transform.position = new Vector3(x, 0, y);
                    //cell.transform.rotation = Quaternion.AngleAxis(45, Vector3.up);
                    cell.transform.parent = container.transform;
                }

                if (tile == Tiles.Brazier)
                {
                    var pre = Resources.Load("Prefabs/Decoration/Brazier") as GameObject;
                    var cell = Instantiate(pre);
                    cell.name = "Brazier_" + x + "_" + y;
                    cell.transform.position = new Vector3(x, 0, y);
                    cell.transform.rotation = Quaternion.AngleAxis(45, Vector3.up);
                    cell.transform.parent = container.transform;
                }

                if (tile == Tiles.Floor)
                {
                    if (rng.OneIn(20))
                    {
                        var item = ItemFactory.GetRandom();
                        CreateItem(x, y, item);
                    }

                    if (rng.OneIn(20))
                    {
                        var pre = Resources.Load("Prefabs/Decoration/EnemySmall") as GameObject;
                        var cell = Instantiate(pre);
                        cell.name = "Rat_" + x + "_" + y;
                        cell.transform.position = new Vector3(x, 0.2f, y);
                        cell.transform.parent = container.transform;
                    }
                }
                
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

	    if (Input.GetKeyDown(KeyCode.F1))
	    {
	        seed++;
	        var x = GameObject.Find("level");
            GameObject.Destroy(x);
	        turnCount = 0;

            Start();
	    }
	}

    private void ToggleInventory()
    {
        var canvas = inventoryCanvas.GetComponent<Canvas>();
        canvas.enabled = !canvas.enabled;
        GameManagerScript.MouseLook = !canvas.enabled;
    }


    public static void EndTurn(int count = 1)
    {
        turnCount+=count;
    }

    public GameObject CreateWall(int x, int y, int tileid)
    {
        var c = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var obstacle = c.AddComponent<NavMeshObstacle>();
        obstacle.carving = true;

        c.layer = 2;
        var r = c.GetComponent<MeshRenderer>();
        r.receiveShadows = true;
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
        c.layer = 2;
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
        c.layer = 2;
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
        c.layer = 2;
        var lightComp = c.AddComponent<Light>();
        lightComp.color = Color.yellow;
        lightComp.range = 3;
        lightComp.intensity = 5;
        lightComp.shadows = LightShadows.Soft;
        lightComp.shadowStrength = 0.5f;
        c.transform.position = new Vector3(x, .35f, y);
        return c;
    }

    public static GameObject CreateItem(int x, int y, Item item)
    {
        
        var pre = Resources.Load("Prefabs/Item") as GameObject;
        var cell = Instantiate(pre);
        var ic = cell.GetComponent<ItemController>();
        ic.Item = item;
        cell.name = "Item_" + x + "_" + y;
        cell.transform.position = new Vector3(x, 0.35f, y).Nudge(0.2f);

        cell.transform.parent = instance.container.transform;
        return cell;
    }

    public static void ItemDroppedBy(Item item, Actor actor)
    {
        var itemGo = CreateItem(actor.Position.x, actor.Position.y, item);
        if (actor.Name == "Player")
        {
            var rb = itemGo.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(instance.player.transform.forward,instance.player.transform.position,ForceMode.Impulse);
        }
    }

    public static List<Vec> Pathfind(Vec start, Vec[] goal)
    {
        DijkstraMap dm = new DijkstraMap(stage, Mathf.Sqrt(2.0f));
        dm.Compute(goal,false);
        if (dm.CalculatePath(start.x,start.y))
            return dm.GetPath().ToList();
        
        return new List<Vec>();
    }

}