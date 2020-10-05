using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public Vector3 newPosition;

    private Transform playerTransform;

    public void MovePosition()
    {
        playerTransform.position = newPosition;
        StartCoroutine(GameManager.Instance.WaitToFade(1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerTransform = collision.gameObject.transform;
        StartCoroutine(GameManager.Instance.Fade(0, this));       
    }
}
