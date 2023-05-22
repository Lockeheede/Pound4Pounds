using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pounding : MonoBehaviour
{
    public float speed = 10f;
    public float moveAmount = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) 
        { 
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}
