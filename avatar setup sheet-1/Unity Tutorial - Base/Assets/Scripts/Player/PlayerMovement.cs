using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Use this for initialization
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;

    private Animator anim;
    private HashIDs hash;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();

        anim.SetLayerWeight(1, 1f);

        //apply turn to correct error caused animation
        Quaternion fixRotation = Quaternion.Euler(0f, -90f, 0f);
        Rigidbody ourBody = this.GetComponent<Rigidbody>();
        ourBody.MoveRotation(fixRotation);

    }

    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        float mouseX = Input.GetAxis("Mouse X");

        Rotating(mouseX);
        MovementManager(v, sneak);
    }

    void update()
    {
        // Cache the attention attracting input
        bool shout = Input.GetButtonDown("Attract");

        //set the animator shouting parameter
        anim.SetBool(hash.shoutingBool, shout);

        AudioManagement(shout);
    }

    void MovementManager (float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (vertical > 0)
        {
            anim.SetFloat (hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0);
        }
    }

    void Rotating (float mouseXInput)
    {
        //access the avatars rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        //create a simple vector using the mouse data that we will use to turn the avatar
        Vector3 targetDirection = new Vector3((mouseXInput * sensitivityX), 0f, 1f);

        //express this in terms of the avatar rather than the world co-ordinate system
        transform.InverseTransformDirection(targetDirection);

        //generate a rotation from the the vector
        Quaternion deltaRotation = Quaternion.Euler(0f, (Input.GetAxis("Mouse X") * sensitivityX), 0f);

        //apply the rigid body
        ourBody.MoveRotation(ourBody.rotation * deltaRotation);
    }

    void AudioManagement (bool shout)
    {
        if (anim.GetCurrentAnimatorStateInfo (0). IsName("Walk"))
        {
            // and if the footsteps are not already playing
            if (!GetComponent<AudioSource>().isPlaying)
            {
                //play footsteps
                GetComponent<AudioSource>().pitch = 0.27f;
                GetComponent<AudioSource>().Play();

            }
        }
        else
        {
            //otherwise stop the foot steps
            GetComponent<AudioSource>().Stop();
        }
        if(shout)
        {
            // 3d sound so play the shouting clip where we are.
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}
