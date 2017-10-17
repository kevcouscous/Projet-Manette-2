using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerTest : MonoBehaviour {


    private float horizontal = 0;
    private float vertical = 0;
    public float multHorizontal = 0;
    public float multVertical = 0;
    public float sensitivity = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position = new Vector3(horizontal * multHorizontal, vertical * multVertical, 0);
        GetComponent<SpriteRenderer>().color = Color.white;
        if (Input.GetButton("Fire1"))
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (Input.GetButton("Fire2"))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (Input.GetButton("Fire3"))
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (Input.GetButton("Jump"))
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    /*float deadZone(float x)
    {
        if (Mathf.Abs(x) < sensitivity)
        {
            return 0;
        }
        else
        {
            return x;
        }
    }*/
}
