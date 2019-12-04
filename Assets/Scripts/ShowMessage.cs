using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    public GameObject objekti;
    Transform player;
    GameObject HUD;
    bool hover = false;
    bool open = false;
    AudioSource audioSource;
    public AudioClip paper;
    

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        HUD = GameObject.Find("HUD");
        audioSource = GetComponent<AudioSource>();
    }
    private void OnMouseEnter()
    {
        hover = true;
    }

    private void OnMouseExit()
    {
        hover = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && hover && HaveLineOfSight())
        {
            objekti.gameObject.SetActive(true);
            open = true;
            HUD.gameObject.SetActive(false);
            audioSource.clip = paper;
            audioSource.Play();
        }

        if (Input.anyKey && open)
        {
            objekti.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            HUD.gameObject.SetActive(true);
            var hinge = GameObject.Find("StartDoorway").gameObject.GetComponent<HingeJoint>();
            hinge.useMotor = true;
        }
    }
    bool HaveLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, 1))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
