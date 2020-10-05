using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostitNote : MonoBehaviour
{
    public string note;
    public TMP_Text postitText;
    public GameObject postitCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCanvas()
    {
        postitText.text = note;
        postitCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        postitCanvas.SetActive(false);
    }
}
