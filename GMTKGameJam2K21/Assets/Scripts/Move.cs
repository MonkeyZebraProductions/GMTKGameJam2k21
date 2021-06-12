using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float Speed;
    float horizontalInput;
    float verticalInput;
    public bool Arrowkey;
    public Vector3 movem;
    public Vector3 facedirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Arrowkey)
        {
            //get the Input from Horizontal axis
            horizontalInput = Input.GetAxis("ArrowHoriz");
            //get the Input from Vertical axis
            verticalInput = Input.GetAxis("ArrowVert");
        }
        else
        {
            //get the Input from Horizontal axis
            horizontalInput = Input.GetAxis("Horizontal");
            //get the Input from Vertical axis
            verticalInput = Input.GetAxis("Vertical");
        }

        if (horizontalInput > 0.7 || horizontalInput < -0.7 || verticalInput > 0.7 || verticalInput < -0.7)
        {
            facedirection = movem;
        }
        movem = new Vector3(horizontalInput,
            verticalInput, 0);
        movem.Normalize();
        //update the position
        transform.position = transform.position + movem * Speed * Time.deltaTime;
        //Debug.Log(movem.x);
    }
        
    
}
