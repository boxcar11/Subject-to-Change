using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject laserPulse;
    public float fireDistance = 10;

    private PlayerController player;

    private bool lineOfSight = false;

    private float offset = 90;
    private float dist;

    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        Vector2 playerLoc = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerLoc);
        //Debug.DrawRay(transform.position, playerLoc);

        if (hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                lineOfSight = true;
            }
            else
            {
                lineOfSight = false;
            }
            //Debug.Log(hit.collider.name);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (lineOfSight)
        {
            Vector3 targetPos = player.transform.position;
            Vector3 thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

            dist = Vector2.Distance(player.transform.position, thisPos);
            //Debug.Log(dist);

            if (dist < fireDistance)
            {
                if (timeLeft <= 0)
                {
                    //Debug.Log("BANG");
                    GameObject lp = Instantiate(laserPulse, transform.position, transform.rotation);
                    timeLeft = 5;
                }
                timeLeft -= Time.deltaTime;
            }
        }
    }
}
