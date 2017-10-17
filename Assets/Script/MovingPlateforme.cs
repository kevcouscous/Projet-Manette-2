﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateforme : MonoBehaviour {

    public float amplitudeX = 0;
    public float amplitudeY = 0;
    public float desynchro = 0;
    public float vitesse_oscillationX = 1;
    public float vitesse_oscillationY = 1;

    public Vector2 velocity_plateforme;

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
        float deltaX = -(Mathf.Cos((Time.time-Time.deltaTime)* vitesse_oscillationX) - Mathf.Cos(Time.time* vitesse_oscillationX)) * amplitudeX;
        float deltaY = -(Mathf.Cos((Time.time - Time.deltaTime)* vitesse_oscillationY + desynchro) - Mathf.Cos(Time.time* vitesse_oscillationY + desynchro)) * amplitudeY;
        velocity_plateforme = new Vector2(deltaX, deltaY);
        rb2d.position = new Vector2(X + Mathf.Cos(Time.time* vitesse_oscillationX) * amplitudeX, Y + Mathf.Cos(Time.time* vitesse_oscillationY + desynchro) * amplitudeY);
    }
}
