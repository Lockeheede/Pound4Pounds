using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pounding : MonoBehaviour
{
    [field: SerializeField] public GameManager m_GameManager;
    public float speed = 20f;
    private bool bHitPancake = false;
    private Rigidbody m_RigidBody;
    public Vector3 startingPos = new Vector3(0,0,0);
    public GameObject poundEffect;
    public GameObject poundLocation;
    public GameObject whiteCakePrefab;
    public GameObject greenCakePrefab;
    public float colliderTimer = 0.0f;
    public enum PoundingState
    {
        PoundDown,
        PoundUp,
        PoundNeutral
    };
    PoundingState poundingState = PoundingState.PoundNeutral;
    public float stateTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        startingPos = m_RigidBody.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(colliderTimer);
        switch (poundingState)
        {
            case PoundingState.PoundDown:
                PoundDown();
                break;
            case PoundingState.PoundUp:
                PoundUp();
                break;
            case PoundingState.PoundNeutral:
                PoundNeutral();
                break;
            default:
                Debug.Log("No State Found!");
                break;
        }
    }

    void PoundNeutral()
    {
        m_RigidBody.velocity = Vector3.zero;
        bHitPancake = false;
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            poundingState = PoundingState.PoundDown;
            stateTimer = Time.time;
        }
    }

    void PoundUp()
    {
        m_RigidBody.AddForce(Vector3.up * speed);
        if (m_RigidBody.transform.position.y >= startingPos.y)
        {
            m_RigidBody.transform.position = startingPos;
            poundingState = PoundingState.PoundNeutral;
        }
    }

    void PoundDown()
    {
        m_RigidBody.AddForce(Vector3.down * speed);
        if (Time.time - stateTimer > 1f)
            poundingState = PoundingState.PoundUp;
    }

    void PoundEffect()
    {
        
    }
  
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "GreenPancake" && bHitPancake == false)
        { 
            m_GameManager.ChangeScore(2);
            bHitPancake = true;
            Instantiate(greenCakePrefab, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 2f, collision.gameObject.transform.position.z),  collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "WhitePancake" && bHitPancake == false) 
        {
            m_GameManager.ChangeScore(-1);
            bHitPancake = true;
            Instantiate(whiteCakePrefab, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 2f, collision.gameObject.transform.position.z), collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}
