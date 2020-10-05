using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpButton : MonoBehaviour
{
    public ReactorSwitch reactorSwitch1;
    public ReactorSwitch reactorSwitch2;

    public bool CheckReactorStatus()
    {
        if(!reactorSwitch1.getSwitchStatus() && !reactorSwitch2.getSwitchStatus())
        {           
            return true;
        }
        else
        {
            return false;
        }
    }
}
