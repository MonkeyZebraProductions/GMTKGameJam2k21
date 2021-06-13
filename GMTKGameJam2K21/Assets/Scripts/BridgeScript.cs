using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    public Sprite OnSprite;
    public GameObject Bridge;
    public Transform BridgePos;
    private SpriteRenderer _sp;
    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==20)
        {
            Instantiate(Bridge, BridgePos.position, Quaternion.identity);
            _sp.sprite = OnSprite;
        }
    }
}
