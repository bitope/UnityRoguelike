﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dungeon;
using UnityEngine;
using System.Collections;
using UnityRoguelike;

public class TestMonsterAI : MonoBehaviour
{
    private CharacterController cc;
    private NavMeshAgent nav;
    private int lastTurn;
    private bool performingTurn = false;
    private Vec currentPosition;
    
    private List<Vec> currentPath;
    
    private TextMesh label;
    
    private Actor actorRef;
    // Use this for initialization
	void Start ()
	{
	    label = transform.FindChild("Label").GetComponent<TextMesh>();
	    cc = GetComponent<CharacterController>();
	    nav = GetComponent<NavMeshAgent>();

	    currentPosition = Util.GetVecPosition(transform.position);
        currentPath = new List<Vec>();

        actorRef = new Actor();
	    actorRef.Name = "TestMonsterAI";
        GameManagerScript.stage.Creatures.Add(actorRef);

	    nav.Warp(transform.position); // cached values wierdness...
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameManagerScript.turnCount > lastTurn)
	        PerformTurn();

        currentPosition = Util.GetVecPosition(transform.position);
        actorRef.Position = currentPosition;

        label.text = currentPosition.ToString();
	}

    void PerformTurn()
    {
        if (performingTurn)
            return;
        performingTurn = true;
        lastTurn++;

        Vec gotoPos;
        if (CanSeePlayer())
            gotoPos = GoToPlayer();
        else
        {
            if (currentPath.Any())
            {
                var first = currentPath.First();
                currentPath.RemoveAt(0);
                gotoPos = first;

                if (!GameManagerScript.stage.IsOpenSpace(gotoPos))
                    gotoPos = GetOpenSpace();
            } 
            else
                gotoPos = GetOpenSpace();
        }

        actorRef.NextPosition = gotoPos;

        var v = new Vector3(gotoPos.x,0,gotoPos.y);
        StartCoroutine(WaitAndMove(0.2f, transform.position, v));
    }

    IEnumerator WaitAndMove(float delayTime, Vector3 start, Vector3 end)
    {
        nav.SetDestination(end);
        nav.Resume();
        yield return new WaitForSeconds(delayTime);
        //float startTime = Time.time; 
        //while (!MoveTowardsTarget(end))
        //{
        //    if ((Time.time - startTime) > 1)
        //    {
        //        Debug.LogError("WaitAndMove: " + name + " stuck for more than 1 sec. Aborting.");
        //        break;
        //    }
        //    yield return null;
        //}
        //currentPosition = Util.GetVecPosition(transform.position);
        //actorRef.Position = currentPosition;
        performingTurn = false;
    }

    private bool CanSeePlayer()
    {
        return GameManagerScript.stage.CheckLineOfSight(currentPosition, GameManagerScript.stage.Player.Position);
    }

    private Vec GoToPlayer()
    {
        currentPath = GameManagerScript.Pathfind(currentPosition, new[] {GameManagerScript.stage.Player.Position});

        // Remove last step.
        if (currentPath.Any())
            currentPath.RemoveAt(currentPath.Count - 1);

        var debugPath = String.Join(" => ", currentPath.Select(vec => vec.ToString()).ToArray());
        Debug.Log(debugPath);

        if (currentPath.Any())
        {
            Vec last = currentPath.First();
            foreach (var d in currentPath)
            {
                Debug.DrawLine(d.Convert(0), (last).Convert(0), Color.green, 2.0f);
                last = d;
            }
        }

        if (currentPath.Any())
        {
            var first = currentPath.First();
            currentPath.RemoveAt(0);
            return first;
        }

        return currentPosition;
    }


    Vec GetOpenSpace()
    {
        var stage = GameManagerScript.stage;
        var open = Direction.all.Where(d =>
        {
            var p = currentPosition + d;
            bool isFloor  = stage[p.x, p.y] == Tiles.Floor;
            bool isOccupied = stage.Creatures.Any(c => c.Position == p);
            bool isReserved = stage.Creatures.Any(c => c.NextPosition == p);
            bool isPlayer = stage.Player != null && stage.Player.Position == p;
            return (isFloor && !isOccupied && !isReserved && !isPlayer);
        }).ToList();

        if (!open.Any())
        {
            Debug.LogError("GetOpenSpace: "+name + " has nowhere to go. Standing still.");
            return currentPosition;
        }

        //foreach (var d in open)
        //{
        //    Debug.DrawLine(currentPosition.Convert(0), (currentPosition + d).Convert(0), Color.green, 2.0f);
        //}

        return GameManagerScript.rng.PickOne(open)+currentPosition;
    }

    //private bool MoveTowardsTarget(Vector3 target)
    //{
    //    var offset = target - transform.position;
    //    if (offset.magnitude <= .3f) //We have reached acceptable tolerance.
    //        return true;

    //    //If we're further away than .3 unit, move towards the target.
    //    //The minimum allowable tolerance varies with the speed of the object and the framerate. 
    //    // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
    //    offset = offset.normalized*2f;
    //    cc.Move(offset*Time.deltaTime);
    //    return false;
    //}

}
