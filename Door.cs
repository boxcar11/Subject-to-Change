using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Cards cardRequired;
    public bool doubleDoor;
    public bool lockable = true;

    private bool locked = true;
    private bool powered = true;

    // Start is called before the first frame update
    void Start()
    {
        if(lockable == false)
        {
            locked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PowerDoor(bool value)
    {
        powered = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Triggered Door");
        if (powered)
        {
            if (locked)
            {
                if (GameManager.Instance.CheckForKeyCard(cardRequired))
                {
                    locked = false;
                }
                else
                {
                    GameManager.Instance.DisplayMessage("I don't have the right key for this door");
                }
            }

            if (locked == false)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<AudioSource>().Play();

                if (doubleDoor)
                {
                    transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (locked)
        {
            GameManager.Instance.HideMessage();
        }
        else if(locked == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<AudioSource>().Play();

            if (doubleDoor)
            {
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (lockable)
        {
            string customName = cardRequired.ToString() + "pad.png";
            Gizmos.DrawIcon(transform.position, customName, true);
        }
    }
#endif
}
