using UnityEngine;
using System.Collections;

public class TestMonsterAI : MonoBehaviour
{

    private CharacterController cc;
    private Vector3 direction;
    private float nextChange;
	// Use this for initialization
	void Start ()
	{
	    cc = GetComponent<CharacterController>();
        ChangeDirection();
	}
	
	// Update is called once per frame
	void Update ()
	{
        
	    //cc.SimpleMove(direction*0.05f);
        cc.SimpleMove(direction * 0.8f);
	    nextChange -= Time.deltaTime;
        if (nextChange<0)
            ChangeDirection();
	}

    void ChangeDirection()
    {
        direction = Random.onUnitSphere;
        direction.y = 0;
        nextChange = 2;
    }
}
