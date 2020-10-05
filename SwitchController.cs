using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchController : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;

    public GameObject[] light2D;
    public TurretController[] turretController;
    public Door[] doorPower;

    private SpriteRenderer sr;
    private AudioSource audioSource;

    private bool on = true;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void FlipSwitch()
    {
        on = !on;
        audioSource.Play();

        if (on)
        {
            sr.sprite = onSprite;
            
            foreach(GameObject go in light2D)
            {
                go.SetActive(true);
            }

            foreach(TurretController tc in turretController)
            {
                tc.enabled = true;
            }

            foreach(Door d in doorPower)
            {
                d.PowerDoor(true);
            }
        }
        else
        {
            sr.sprite = offSprite;

            foreach (GameObject go in light2D)
            {
                go.SetActive(false);
            }

            foreach (TurretController tc in turretController)
            {
                tc.enabled = false;
            }

            foreach (Door d in doorPower)
            {
                d.PowerDoor(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
        }
    }
}
