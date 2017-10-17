using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

    public float gravityModifier = 1f;
    public float minGroundNormalY = 0.65f;
    public float minWallNormalX = 0.65f;
    public float coefWall = 0.8f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected bool walled;
    protected Vector2 groundNormal;
    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitbuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected int InvertedGravity = 1;

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	void Update () {

        targetVelocity = Vector2.zero;
        ComputeVelocity();
	}

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime * InvertedGravity;
        velocity.x = targetVelocity.x;

        grounded = false;
        walled = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;
        

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        
        if (moveAlongGround.x > 0)
        {
            moveAlongGround.x *= -1;
        }

        Vector2 move = -moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitbuffer, distance + shellRadius);
            hitBufferList.Clear();
            for(int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitbuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if(currentNormal.y*InvertedGravity > minGroundNormalY)
                {
                    grounded = true;

                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                else if(Mathf.Abs(currentNormal.x) > minWallNormalX)
                {
                    walled = true;
                    velocity = velocity * coefWall;
                    if (!yMovement)
                    {
                        currentNormal.y = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;

            }

        }

        rb2d.position = rb2d.position + move.normalized*distance;
    }
}
