using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Physics {

    public float jumptakeOffSpeed = 7;
    public float maxSpeed = 7;

    public int nbJump = 0;

	// Use this for initialization
	void Start () {
		
	}

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        if (grounded)
        {
            nbJump = 0;
        }
        if (walled)
        {
            nbJump = 1;
        }

        move.x = Input.GetAxis("Horizontal");
        
        if(Input.GetButtonDown("Fire1") && ((grounded||walled) ||(nbJump<2)))
        {
            velocity.y = jumptakeOffSpeed * InvertedGravity;
            nbJump++;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (velocity.y * InvertedGravity> 0)
                velocity.y = velocity.y * .5f;
        }

        targetVelocity = move * maxSpeed;

        if (Input.GetButtonDown("Fire2"))
        {
            InvertedGravity = -InvertedGravity;
        }
    }
}
