using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject Liftable;
    public Transform holdSpot;
    private Collider2D liftCollider;
    private bool canLift, iscarrying;
    public float BoxSize = 1;
    public LayerMask LiftLayer;
    public float distance;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        canLift =liftCollider= Physics2D.OverlapBox(transform.position, new Vector2(BoxSize,BoxSize),0, LiftLayer);
        animator.SetBool("isCarrying", iscarrying);
        Debug.Log(canLift);
        if(Input.GetKeyDown(KeyCode.RightControl) && canLift && !iscarrying)
        {
            Debug.Log("Hi");
            Liftable = liftCollider.gameObject;
            Liftable.transform.SetParent(holdSpot);
            Liftable.transform.position = holdSpot.position;
            Liftable.GetComponent<Rigidbody2D>().simulated = false;
            Liftable.GetComponent<BoxCollider2D>().enabled = false;
            animator.Play("PickUp");
            FindObjectOfType<AudioManager>().Play("Lift");
            iscarrying = true;
        }
        if (Input.GetKeyUp(KeyCode.RightControl) && iscarrying)
        {
            Debug.Log("Hi");
            
            Liftable.transform.SetParent(null);
            Liftable.transform.position = holdSpot.position+new Vector3(0,distance,0);
            Liftable.GetComponent<Rigidbody2D>().simulated = true;
            Liftable.GetComponent<BoxCollider2D>().enabled = true;
            Liftable = null;
            animator.Play("Drop");
            FindObjectOfType<AudioManager>().Play("Drop");
            iscarrying = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(BoxSize, BoxSize, BoxSize));
    }
}
