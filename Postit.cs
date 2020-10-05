using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Postit : MonoBehaviour
{
    public KeypadLock codedDoor;
    public TMP_Text postitText;
    public GameObject postitCanvas;

    private int codeNumber;
    private int code;

    // Start is called before the first frame update
    void Start()
    {
        code = Randomizer.Instance.GetCode(transform.position);
        codedDoor.SetCode(code);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCanvas()
    {
        Debug.Log("Updating canvas");
        postitText.text = code.ToString("d4");
        postitCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        postitCanvas.SetActive(false);
    }

    public void SetCodeNumber(int number)
    {
        codeNumber = number;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
