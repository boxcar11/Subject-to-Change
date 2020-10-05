using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalMixer : MonoBehaviour
{
    private int count;

    public void Mix()
    {
        count = 0;
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.Instance.CheckForChemical((Chemicals)i))
            {
                count++;
            }
    }
        if(count == 3)
        {
            Debug.Log("Chemicals mixed");
            GameManager.Instance.DisplayMessage("I have mixed the chemicals together.");
            
            for(int i = 0; i < 3; i++)
            {
                GameManager.Instance.RemoveChemicals((Chemicals)i);
            }
        }
        else
        {
            GameManager.Instance.DisplayMessage("I don't have the right chemicals.");
        }
    }
}
