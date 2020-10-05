using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    public TMP_Text display;

    private KeypadLock kpl;

    private int code;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKeypadLock(KeypadLock value)
    {
        kpl = value;
        ResetKeypad();
    }

    public void InputDigit(int value)
    {
        code = (code * 10) + value;
        kpl.InputDigit(code);

        display.text = code.ToString("d4");
        
        GetComponent<AudioSource>().Play();
    }

    public void ResetKeypad()
    {
        code = 0;
        display.text = "0000";
    }
}
