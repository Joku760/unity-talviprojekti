using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    public GameObject objekti;
    private void OnMouseDown()
    {
        objekti.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            objekti.gameObject.SetActive(false);
        }
    }
}
