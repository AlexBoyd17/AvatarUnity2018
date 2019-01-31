using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour {

    public Camera triggeredCam;
    public Camera liveCam;

    private void OnTriggerEnter(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if(other == PlayerCollider)
        {
            liveCam = Camera.allCameras[0];

            triggeredCam.enabled = true;
            liveCam.enabled = false;

            triggeredCam.GetComponent<AudioListener>().enabled = true;
            PlayerCharacter.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    { 

        triggeredCam.enabled = false;
        liveCam.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
