using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateforme : MonoBehaviour {

    public float amplitudeX = 0;
    public float amplitudeY = 0;
    public float desynchro = 0;

    private float X = 0;
    private float Y = 0;

    protected Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        X = rb2d.position.x;
        Y = rb2d.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        rb2d.position = new Vector2( X + Mathf.Cos(Time.time) * amplitudeX, Y + Mathf.Cos(Time.time + desynchro)*amplitudeY);
    }
}
