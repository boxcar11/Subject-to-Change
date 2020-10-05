using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockDoor : MonoBehaviour
{
    public Airlock airlock;
    public int doorNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            airlock.OpenAirlock(doorNumber);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            airlock.ShutAirlock();
        }
    }
}
