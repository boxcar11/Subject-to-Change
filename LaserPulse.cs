using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPulse : MonoBehaviour
{
    private Rigidbody2D rb;
    private float thrust = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Firing");
        rb.AddForce(-transform.up * thrust, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(GameManager.Instance.Fade(0));
            StartCoroutine(GameManager.Instance.WaitToReload());
        }
    }
}
