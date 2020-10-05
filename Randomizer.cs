using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public List<GameObject> switches;
    public List<Transform> locations;

    public List<int> codes;

    private int numberOfCodes;
    private List<Vector3> postitPositions;

    #region Singleton
    private static Randomizer instance;
    public static Randomizer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Randomizer>();
            }

            return instance;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Awake()
    {       
        // Don't destroy gameobject on scene reload
        DontDestroyOnLoad(this.gameObject);

        if(instance != null)
        {
            Destroy(this.gameObject);
        }

        // Randomize switch positioning
        for (int i = 0; i < switches.Count; i++)
        {
            GameObject temp = switches[i];
            int randomIndex = Random.Range(i, switches.Count);
            switches[i] = switches[randomIndex];
            switches[randomIndex] = temp;
        }
        for (int i = 0; i < switches.Count; i++)
        {
            switches[i].transform.position = locations[i].position;
        }

        // Assign number to all postits
        postitPositions = new List<Vector3>();
        Postit[] postits = Resources.FindObjectsOfTypeAll(typeof(Postit)) as Postit[];
        numberOfCodes = postits.Length;
        for(int i = 0; i < postits.Length; i++)
        {
            postitPositions.Add(postits[i].GetPosition());
            postits[i].SetCodeNumber(i);
        }
        
        // Build random 4 digit codes
        for (int i = 0; i < numberOfCodes; i++)
        {
            int firstDigit = Random.Range(0, 9);
            int secondDigit = Random.Range(0, 9);
            int thirdDigit = Random.Range(0, 9);
            int fourthDigit = Random.Range(0, 9);

            Debug.Log(firstDigit.ToString() + secondDigit.ToString() + thirdDigit.ToString() + fourthDigit.ToString());
            codes.Add((firstDigit * 1000) + (secondDigit * 100) + (thirdDigit * 10) + fourthDigit);
        }  
    }

    public int GetCode(Vector3 value)
    {
        for(int i = 0; i < postitPositions.Count; i++)
        {
            if(postitPositions[i] == value)
            {
                return codes[i];
            }
        }

        return 999999;
    }
}
