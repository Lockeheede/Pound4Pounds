using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AddCoinFX : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Swoosh;
    public float CoinStartHeight = -5.81f;
    public float MaxCoinHeight = -5.0f;
    public float CoinSpeed = 1.0f;

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
     //   Coin.active = true;
     //   Swoosh.active = false;
      //  Coin.transform.position = new Vector3(-2.745652f, CoinStartHeight, 13.66129f);
    }

    void Awake()
    {
        Coin.SetActive(false);
        Swoosh.SetActive(false);

    }

    void StartEffect()
    {
        Coin.SetActive(true);
        Swoosh.SetActive(false);
        Coin.transform.localPosition = new Vector3(-2.745652f, CoinStartHeight, 13.66129f);
        CoinState = eCoinState.SpawnCoin;
        StateTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
                }
            }
            CoinZRotation += Time.deltaTime * 355;
            Quaternion rotator = Coin.transform.rotation;
            Coin.transform.rotation = Quaternion.Euler(Coin.transform.rotation.eulerAngles.x, CoinZRotation,Coin.transform.rotation.eulerAngles.z );

        }
    }
}
