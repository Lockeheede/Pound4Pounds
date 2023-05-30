using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Naira_UI : MonoBehaviour
{
    public float SpinDurationSeconds = 1.0f;
    public TMPro.TextMeshPro TMPText;
    private float SpinStartTime = -1.0f;

    private Vector3 StartEuler = Vector3.zero;
    private float EndZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject GameMgrObj = GameObject.Find("GameManager");
        GameManager GameMgr = GameMgrObj.GetComponent<GameManager>();
        if (GameMgr != null)
        {
            GameMgr.ScoreChangedCallback += ScoreChangedCallback;
        }
    }

    private void OnDestroy()
    {
        GameObject GameMgrObj = GameObject.Find("GameManager");
        GameManager GameMgr = GameMgrObj.GetComponent<GameManager>();
        if (GameMgr != null)
        {
            GameMgr.ScoreChangedCallback -= ScoreChangedCallback;
        }     
    }

    void ScoreChangedCallback(int currentScore, int change)
    {
        TMPText.text = "x " + currentScore;

        if (change > 0)
        {
            Invoke("StartSpin", 0);
        }
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
