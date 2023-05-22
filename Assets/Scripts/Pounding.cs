using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pounding : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody m_RigidBody;
    private bool pounded = false;
    private float poundReset = 1.5f;
    public GameObject poundEffect;
    public GameObject poundLocation;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0) && pounded == false) 
        { 
            m_RigidBody.AddForce(Vector3.down * speed);
            Invoke("PoundEffect", 0.5f);
        }
    }

    void PoundEffect()
    {
        Instantiate(poundEffect, poundLocation.transform);
        m_RigidBody.AddForce(Vector3.up * speed);
        pounded = true;
        Invoke("StopLift", 0.3f);
    }

    void StopLift()
    {
        m_RigidBody.isKinematic = true;
        Debug.Log("Cooldown");
        Invoke("StopKinematics", 1.5f);
    }

    void StopKinematics()
    {
        m_RigidBody.isKinematic = false;
        pounded = false;
    }
}
