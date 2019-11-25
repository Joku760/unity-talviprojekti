using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 Direction;
    public float range = 30f;
    public int damage = 20;
    CrossBow crossBow;
    Vector3 spawnPosition;
    AudioSource audioSource;
    public AudioClip arrowhit;
    public AudioClip glass;
    public AudioClip enemyhit;
    MeshRenderer render;

    void Start()
    {
        spawnPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(Direction * 40f);
        crossBow = FindObjectOfType<CrossBow>();
        damage = crossBow.damage;
        audioSource = GetComponent<AudioSource>();
        render = GetComponent<MeshRenderer>();
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
            audioSource.clip = enemyhit;
            audioSource.Play();
            render.enabled = false;
            collision.gameObject.GetComponent<EnemyController>().UpdateHp(damage);
            Destroy(gameObject, audioSource.clip.length);
        }
        else if( collision.transform.tag == "Arrow")
        {
            Destroy(collision.gameObject);
        }
        else if(collision.transform.tag == "Breakable")
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = glass;
                audioSource.Play();
            }
            Destroy(collision.gameObject);
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = arrowhit;
                audioSource.Play();
            }
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject, 10);
        }
        
    }
}
