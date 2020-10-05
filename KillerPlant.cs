using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerPlant : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        anim.SetBool("Attack", true);
        StartCoroutine(GameManager.Instance.Fade(0));
        StartCoroutine(GameManager.Instance.WaitToReload());
    }
}
