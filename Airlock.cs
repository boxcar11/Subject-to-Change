using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    public ParticleSystem particle;

    public AudioClip doorClip;
    public AudioClip decontaminationClip;

    public bool doubleDoor = false;

    private int doorOpen;
    private float timeLeft;

    private bool squencing = false;
    private bool doorShut = false;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0 && doorShut)
        {
            if (doorOpen == 1)
            {
                door2.transform.GetChild(0).gameObject.SetActive(false);
                audioSource.clip = doorClip;
                audioSource.Play();
                if (doubleDoor)
                {
                    door2.transform.GetChild(1).gameObject.SetActive(false);
                }
                doorOpen = 0;
                //squencing = false;
            }
            if (doorOpen == 2)
            {
                door1.transform.GetChild(0).gameObject.SetActive(false);
                audioSource.clip = doorClip;
                audioSource.Play();
                if (doubleDoor)
                {
                    door1.transform.GetChild(1).gameObject.SetActive(false);
                }
                doorOpen = 0;
                //squencing = false;
            }
        }

        timeLeft -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        squencing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        squencing = false;
    }

    public void OpenAirlock(int door)
    {
        if(door == 1 && timeLeft <= 0)
        {
            door1.transform.GetChild(0).gameObject.SetActive(false);
            audioSource.clip = doorClip;
            audioSource.Play();
            if (doubleDoor)
            {
                door1.transform.GetChild(1).gameObject.SetActive(false);
            }
            doorOpen = 1;
            //squencing = true;
            doorShut = false;
            //Debug.Log("Open Door 1");
        }
        else if(door == 2 && timeLeft <= 0)
        {
            door2.transform.GetChild(0).gameObject.SetActive(false);
            audioSource.clip = doorClip;
            audioSource.Play();
            if (doubleDoor)
            {
                door2.transform.GetChild(1).gameObject.SetActive(false);
            }
            doorOpen = 2;
            //squencing = true;
            doorShut = false;
            //Debug.Log("Open Door 2");
        }
    }

    public void ShutAirlock()
    {
        if (doorOpen == 1)
        {
            door1.transform.GetChild(0).gameObject.SetActive(true);
            audioSource.clip = doorClip;
            audioSource.Play();
            if (doubleDoor)
            {
                door1.transform.GetChild(1).gameObject.SetActive(true);
            }
            //doorOpen = 0;
            if (squencing && doorShut == false)
            {
                timeLeft = 5;
                doorShut = true;
                if (particle)
                {
                    particle.Play();
                    PlayDecontamination();
                }
            }
            //Debug.Log("Shut Door 1");
        }
        else if (doorOpen == 2)
        {
            door2.transform.GetChild(0).gameObject.SetActive(true);
            audioSource.clip = doorClip;
            audioSource.Play();
            if (doubleDoor)
            {
                door2.transform.GetChild(1).gameObject.SetActive(true);
            }
            //doorOpen = 0;
            if (squencing && doorShut == false)
            {
                timeLeft = 5;
                doorShut = true;
                if (particle != null)
                {
                    PlayDecontamination();
                    particle.Play();
                }
            }
            //Debug.Log("Shut Door 2");
        }
    }

    private void PlayDecontamination()
    {
        audioSource.clip = decontaminationClip;
        audioSource.loop = true;
        audioSource.Play();
        StartCoroutine(StopPlayingAfterTime(5));
    }

    IEnumerator StopPlayingAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.Stop();
        audioSource.loop = false;
    }
}
