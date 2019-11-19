using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 Direction;
    public float range = 30f;
    public int damage = 20;
    Vector3 spawnPosition;
    void Start()
    {
        spawnPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(Direction * 40f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(spawnPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().UpdateHp(damage);
            Destroy(gameObject);
        }
        else if( collision.transform.tag == "Arrow")
        {
            Destroy(collision.gameObject);
        }
        else if(collision.transform.tag == "Breakable")
        {
            Destroy(collision.gameObject);
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject, 10);
        }
        
    }
}
