using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadLock : MonoBehaviour
{
    public int keyCode;

    public GameObject keypadCanvas;

    public bool doubleDoor;
    public bool lockable = true;

    private bool locked = true;
    private int code;
    private Keypad keypad;

    // Start is called before the first frame update
    void Start()
    {
        if (lockable == false)
        {
            locked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            if (code == keyCode)
            {
                locked = false;

                keypadCanvas.SetActive(false);

                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<AudioSource>().Play();

                if (doubleDoor)
                {
                    transform.GetChild(1).gameObject.SetActive(false);
                }

                code = 0;
            }
            else if(code.ToString().Length == 4 && keypad != null)
            {
                keypad.ResetKeypad();
            }
        }
    }

    public void InputDigit(int value)
    {
        code = value;
    }

    public void SetCode(int value)
    {
        keyCode = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered Door");

        if (locked)
        {
            keypadCanvas.SetActive(true);
            keypad = keypadCanvas.GetComponent<Keypad>();
            keypad.SetKeypadLock(this);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(locked)
        {
            keypadCanvas.SetActive(false);
            keypad = null;
        }

        if (locked == false)
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
            string customName = "Gray" + "pad.png";
            Gizmos.DrawIcon(transform.position, customName, true);
        }
    }
#endif
}
