using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorSwitch : MonoBehaviour
{
    public GameObject statusLight;

    private bool switchPosition = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipSwitch()
    {
        switchPosition = !switchPosition;
        statusLight.SetActive(switchPosition);
    }

    public bool getSwitchStatus()
    {
        return switchPosition;
    }
}
