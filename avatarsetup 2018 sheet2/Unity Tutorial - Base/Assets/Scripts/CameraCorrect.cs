using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCorrect : MonoBehaviour {

    public GameObject target;
    private void Awake()
    {
        this.transform.RotateAround(target.transform.position, Vector3.up ,90f);
    }
}
