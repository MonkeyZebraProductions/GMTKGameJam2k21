using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public Transform PairedButton;
    public GameObject SpawnedObject;
    private bool hasPressed;
    public bool destroy;
    public LayerMask Liftlayer;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==21 && hasPressed==false)
        {
            if(destroy)
            {
               SpawnedObject.SetActive(false);
            }
            else
            {
                Instantiate(SpawnedObject, PairedButton.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("Switch On");
            }
            Debug.Log("yo");
            hasPressed = true;
        }
    }
}
