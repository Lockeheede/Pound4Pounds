using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[ExecuteInEditMode]
public class AddCoinFX : MonoBehaviour
{
    public GameObject Coin;
    public GameObject[] SwooshList;
   // 
    public ParticleSystem Particles1;
    public ParticleSystem Particles2;
    public Naira_UI NairiUI;

    public float CoinStartHeight = -5.81f;
    public float MaxCoinHeight = -5.0f;
    public float CoinSpeed = 1.0f;

    public float SwooshStart = -1.0f;
    public float SwooshEnd = 1.0f;
    public float SwooshSpeed = 0.1f;
    private float SwooshU = -1.0f;

    private GameObject Swoosh;
    private Renderer SwooshRenderer;
    public int Score = 0;

    public bool DebugStart = false;
    enum eCoinState
    {
        None,
        SpawnCoin,
        CoinSwoosh,
        SpinUICoin
    };
    eCoinState CoinState = eCoinState.None;

    private float CoinZRotation = 0;
    private float StateTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        StartEffect();
    }

    void StartEffect()
    {
        int range = Random.Range(0, SwooshList.Length);
        Swoosh = SwooshList[range];
        SwooshRenderer = Swoosh.GetComponentInChildren<Renderer>();
        Coin.SetActive(true);
        Swoosh.SetActive(false);
        Coin.transform.localPosition = new Vector3(-2.745652f, CoinStartHeight, 13.66129f);
        CoinState = eCoinState.SpawnCoin;
        StateTimer = 0;
    }

    // Update is called once per frame
    void Update()
    { // Swoosh.GetComponent<Renderer>().material.SetFloat("_UOffset", 0.5f);
        if (DebugStart == true)
        {
            DebugStart = false;
            StartEffect(); 
        }
        if (CoinState == eCoinState.SpawnCoin)
        {
            Coin.transform.localPosition = Coin.transform.localPosition + new Vector3(0.0f, Time.deltaTime * CoinSpeed, 0.0f);

            if (Coin.transform.localPosition.y > MaxCoinHeight)
            {
                Coin.transform.localPosition = new Vector3(Coin.transform.localPosition.x, MaxCoinHeight, Coin.transform.localPosition.z);
                if (StateTimer <= 0.0)
                {
                    StateTimer = Time.time;
                }

                if (Time.time > StateTimer + 0.35f)
                {
                    CoinState = eCoinState.CoinSwoosh;
                    Coin.SetActive(false);
                    Swoosh.SetActive(true);
                    SwooshU = SwooshStart;
                    SwooshRenderer.material.SetFloat("_UOffset", SwooshU);
                    Particles1.Play();
                }
            }
            CoinZRotation += Time.deltaTime * 355;
            Quaternion rotator = Coin.transform.rotation;
            Coin.transform.rotation = Quaternion.Euler(Coin.transform.rotation.eulerAngles.x, CoinZRotation,Coin.transform.rotation.eulerAngles.z );

        }
        else if (CoinState == eCoinState.CoinSwoosh)
        {
            SwooshU += Time.deltaTime * SwooshSpeed;
            SwooshRenderer.material.SetFloat("_UOffset", SwooshU);
            if (SwooshU >= SwooshEnd)
            {
                CoinState = eCoinState.SpinUICoin;
                Particles2.Play();
                StateTimer = Time.time;
                NairiUI.StartSpin(Score);
            }
        }
        else if (CoinState == eCoinState.SpinUICoin)
        {
            if (Time.time > StateTimer + 3.0f)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
