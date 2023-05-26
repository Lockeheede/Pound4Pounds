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
    float LastNotePlayedTime = -1;

    MusicManager()
    {
     //   PianoKeys = new AudioClip[PianoNotes.Length];
    }

    void Update()
    {
        float CurTime = Time.time;
        if (LastNotePlayedTime == -1 || CurTime > LastNotePlayedTime + 0.45f)
        {
            LastNotePlayedTime = CurTime;
            PlayNextNote();
        }
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
