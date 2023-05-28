using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naira_UI : MonoBehaviour
{
    public float SpinDurationSeconds = 1.0f;

    private float SpinStartTime = -1.0f;

    private Vector3 StartEuler = Vector3.zero;
    private float EndZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartSpin", 3);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpin();
    }

    public void StartSpin()
    {
        if (SpinStartTime != -1.0f)
        {
            return;
        }

        SpinStartTime = Time.time;
        StartEuler = gameObject.transform.rotation.eulerAngles;
        EndZ = StartEuler.y + 2880;
    }

    private void UpdateSpin()
    {
        if (SpinStartTime == -1.0f)
        {
            return;
        }

        float normalizedTime = (Time.time - SpinStartTime) / SpinDurationSeconds;
        if (normalizedTime > 1)
        {
            gameObject.transform.rotation = Quaternion.Euler(StartEuler);
            SpinStartTime = -1.0f;
            Invoke("StartSpin", 3);
            return;
        }

        float CurZRot = StartEuler.z;

        if (normalizedTime > 0)
        {
            normalizedTime = 1.0f + 0.1f * Mathf.Log(normalizedTime);
            CurZRot = Mathf.Lerp(StartEuler.z, EndZ, normalizedTime);
        }

        Quaternion rotator = gameObject.transform.rotation;
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, CurZRot,gameObject.transform.rotation.eulerAngles.z );

    
    }
}
