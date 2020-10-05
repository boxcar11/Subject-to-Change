using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    public Cards card;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.AddKeyCard(card);

        Destroy(this.gameObject);
    }
}
