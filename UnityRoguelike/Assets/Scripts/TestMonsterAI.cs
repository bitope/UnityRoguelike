using System.Linq;
using System.Runtime.InteropServices;
using Dungeon;
using UnityEngine;
using System.Collections;
using UnityRoguelike;

public class TestMonsterAI : MonoBehaviour
{

    private CharacterController cc;
    private Vector3 direction;
    private float nextChange;

    private int lastTurn;
    private bool performingTurn = false;
    private Vec currentPosition;

    private TextMesh label;

    // Use this for initialization
	void Start ()
	{
	    label = transform.FindChild("Label").GetComponent<TextMesh>();

	    cc = GetComponent<CharacterController>();
        ChangeDirection();

	    currentPosition = Util.GetVecPosition(transform.position);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameManagerScript.turnCount > lastTurn)
	        PerformTurn();

	    label.text = currentPosition.ToString();

	    //cc.SimpleMove(direction*0.05f);
	    //cc.SimpleMove(direction * 0.8f);
	    //nextChange -= Time.deltaTime;
	    //if (nextChange<0)
	    //    ChangeDirection();
	}

    void PerformTurn()
    {
        if (performingTurn)
            return;

        performingTurn = true;

        lastTurn++;
        var gotoPos = GetOpenSpace();

        Vector3 v = new Vector3(gotoPos.x,0,gotoPos.y);
        StartCoroutine(WaitAndMove(0.2f, transform.position, v));
    }

    void ChangeDirection()
    {
        direction = Random.onUnitSphere;
        direction.y = 0;
        nextChange = 2;
    }

    IEnumerator WaitAndMove(float delayTime, Vector3 start, Vector3 end)
    {
        //yield return new WaitForSeconds(delayTime); // start at time X
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point

        while (Time.time - startTime <= delayTime)
        { // until one second passed
            transform.position = Vector3.Lerp(start, end, (Time.time - startTime)/delayTime); // lerp from A to B in delay seconds
            yield return null; // wait for next frame
        }
        currentPosition = Util.GetVecPosition(transform.position);
        performingTurn = false;
    }

    Vec GetOpenSpace()
    {
        var stage = GameManagerScript.stage;
        var open = Direction.all.Where(d =>
        {
            var p = currentPosition + d;
            return stage[p.x, p.y] == Tiles.Floor;
        }).ToList();

        return GameManagerScript.rng.PickOne(open)+currentPosition;
    }
}
