﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Projectile;
    
    public Move movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float angle = (Mathf.Atan2(movement.facedirection.y, movement.facedirection.x) * Mathf.Rad2Deg);
        Debug.Log(movement.movem.x);
        if(Input.GetButtonDown("Shoot"))
        {
            Instantiate(Projectile, transform.position, Quaternion.Euler(0,0,angle));
        }
    }
}
