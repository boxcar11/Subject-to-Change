using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vial : MonoBehaviour
{
    public Chemicals chemical;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.AddChemical(chemical);

        Destroy(this.gameObject);
    }
}
