using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public GameManager m_GameManager;
    public int points;
    public float colliderTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = gameObject.AddComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*
    private void OnTriggerEnter(Collider other)
    {
        colliderTimer = Time.time;
        if (other.CompareTag("Player") && Time.time - colliderTimer < 0f)
        {
            m_GameManager.ChangeScore(points);
        }
        if (Time.time - colliderTimer > 1f)
        {
            colliderTimer = 0;
        }
      
    }
    */
}
