using System.Linq;
using Dungeon;
using UnityEngine;
using System.Collections;
using UnityRoguelike;

public class TestMonsterAI : MonoBehaviour
{
    private CharacterController cc;
    private int lastTurn;
    private bool performingTurn = false;
    private Vec currentPosition;

    private TextMesh label;
    
    private Actor actorRef;
    // Use this for initialization
	void Start ()
	{
	    label = transform.FindChild("Label").GetComponent<TextMesh>();
	    cc = GetComponent<CharacterController>();
	    currentPosition = Util.GetVecPosition(transform.position);

        actorRef = new Actor();
        GameManagerScript.stage.Creatures.Add(actorRef);	
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameManagerScript.turnCount > lastTurn)
	        PerformTurn();

	    label.text = currentPosition.ToString();
	}

    void PerformTurn()
    {
        if (performingTurn)
            return;

        performingTurn = true;

        lastTurn++;
        var gotoPos = GetOpenSpace();
        actorRef.NextPosition = gotoPos;

        var v = new Vector3(gotoPos.x,0,gotoPos.y);
        StartCoroutine(WaitAndMove(0.2f, transform.position, v));
    }

    IEnumerator WaitAndMove(float delayTime, Vector3 start, Vector3 end)
    {
        float startTime = Time.time; 
        while (!MoveTowardsTarget(end))
        {
            if ((Time.time - startTime) > 10)
            {
                Debug.LogError("WaitAndMove: " + name + " stuck for more than 10 sec. Aborting.");
                break;
            }
            yield return null;
        }
        currentPosition = Util.GetVecPosition(transform.position);

        actorRef.Position = currentPosition;
        performingTurn = false;
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
            return (isFloor && !isOccupied);
        }).ToList();

        if (!open.Any())
        {
            Debug.LogError("GetOpenSpace: "+name + " has nowhere to go. Standing still.");
            return currentPosition;
        }

        return GameManagerScript.rng.PickOne(open)+currentPosition;
    }

    private bool MoveTowardsTarget(Vector3 target)
    {
        var offset = target - transform.position;
        if (offset.magnitude <= .3f) //We have reached acceptable tolerance.
            return true;

        //If we're further away than .3 unit, move towards the target.
        //The minimum allowable tolerance varies with the speed of the object and the framerate. 
        // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
        offset = offset.normalized*2f;
        cc.Move(offset*Time.deltaTime);
        return false;
    }

}
