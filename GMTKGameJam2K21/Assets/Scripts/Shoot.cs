using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Projectile;
    
    public Move movement;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float angle = (Mathf.Atan2(movement.facedirection.y, movement.facedirection.x) * Mathf.Rad2Deg);
        Debug.Log(movement.movem.x);
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Instantiate(Projectile, transform.position, Quaternion.Euler(0,0,angle));
            animator.Play("ShootShoot");
            FindObjectOfType<AudioManager>().Play("Shoot");
        }
    }
}
