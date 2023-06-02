using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] PianoKeys;
    public int[] SongNotes;
    public AudioSource[] AudioSrc;
    int CurAudioSrc = 0;

    int CurNote = 0;

    float StartTime;
    MusicManager()
    {
     //   PianoKeys = new AudioClip[PianoNotes.Length];
        
    }

    private void Start()
    {
        StartTime = Time.time;
        GameObject GameMgrObj = GameObject.Find("GameManager");
        GameManager GameMgr = GameMgrObj.GetComponent<GameManager>();
        if (GameMgr != null)
        {
            GameMgr.ScoreChangedCallback += ScoreChangedCallback;
        }
    }

    void ScoreChangedCallback(int currentScore, int change)
    {
        if (change > 0)
        {
            PlayNextNote();
        }
        else
        {

        }
    }

    private void OnDestroy()
    {
        GameObject GameMgrObj = GameObject.Find("GameManager");
        if (GameMgrObj != null)
        {
            GameManager GameMgr = GameMgrObj.GetComponent<GameManager>();
            if (GameMgr != null)
            {
                GameMgr.ScoreChangedCallback -= ScoreChangedCallback;
            }
        }
    }

    void Update()
    {
      /*  if (Time.time < StartTime + 1.0)
            return;

        float CurTime = Time.time;
        if (LastNotePlayedTime == -1 || CurTime > LastNotePlayedTime + 0.30f)
        {
            LastNotePlayedTime = CurTime;
        }*/
    }

    void PlayNextNote()
    {
        if (AudioSrc == null)
        {
            return;
        }

        if (SongNotes.Length == 0)
        {
            return;
        }

        if (CurNote >= SongNotes.Length)
        {
            CurNote = 0;
        }
        
        int KeyIdx = SongNotes[CurNote];
        CurNote++;
        if (KeyIdx < 0 || KeyIdx >= PianoKeys.Length)
        {
            // Throw warning
            return;
        }

        if (CurAudioSrc >= AudioSrc.Length)
        {
            CurAudioSrc = 0;
        }
        AudioSrc[CurAudioSrc].clip = PianoKeys[KeyIdx];
        AudioSrc[CurAudioSrc].Play();
        CurAudioSrc++;
    }
}
