using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public Transform PairedButton;
    public GameObject SpawnedObject;
    private bool hasPressed;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==15 && hasPressed==false)
        {
            Instantiate(SpawnedObject, PairedButton.position, Quaternion.identity);
            hasPressed = true;
        }
    }
}
