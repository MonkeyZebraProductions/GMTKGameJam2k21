using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject liftable;
    private bool canLift;
    public float BoxSize = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        canLift = Physics2D.OverlapBox(transform.position, new Vector2(BoxSize,BoxSize),0,10);
        Debug.Log(canLift);
        if(Input.GetButtonDown("Lift") && canLift)
        {
            Debug.Log("Hi");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(BoxSize, BoxSize, BoxSize));
    }
}
