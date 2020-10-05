using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public Transform player;
    public Transform playerCamera;
    public Vector3 newPosition;
    public Vector3 newCameraPosition;

    private bool[] steps = new bool[7];
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSeconds(0,5));
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.DisplayMessage("Oh no!");
        if (steps[0])
        {
            StartCoroutine(WaitForSeconds(1,5));
            GameManager.Instance.DisplayMessage("The warp core is overloading!");
        }
        if(steps[1])
        {
            StartCoroutine(WaitForSeconds(2,2));
            StartCoroutine(GameManager.Instance.Fade(0));
        }
        if(steps[2])
        {
            StartCoroutine(WaitForSeconds(3, .5f));
            player.position = newPosition;
            playerCamera.position = newCameraPosition;
        }
        if(steps[3])
        {
            StartCoroutine(WaitForSeconds(4, .5f));
            GameManager.Instance.DisplayMessage("Wait why does my watch show a time of 5 mins ago?");
        }
        if(steps[4])
        {
            StartCoroutine(WaitForSeconds(5, 8));
            StartCoroutine(GameManager.Instance.Fade(1));
        }
        if(steps[5])
        {
            StartCoroutine(WaitForSeconds(6, 5));
            GameManager.Instance.DisplayMessage("It must be the warp core! I need to find a way to stop it.");
        }
        if(steps[6])
        {
            GameManager.Instance.ReloadLevel();
        }
    }

    IEnumerator WaitForSeconds(int stepNumber, float time)
    {
        yield return new WaitForSeconds(time);
        steps[stepNumber] = true;
    }
}
