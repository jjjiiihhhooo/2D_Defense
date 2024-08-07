using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AudioData
{
    [Header("Audio")]
    public AudioClip audio;
    [Header("Sound Name (BGM = Scene Name)")]
    public string audioName;
}
public class SoundManager : MonoBehaviour
{
    public AudioData[] audioDatas;

    public Dictionary<string, AudioClip> audioDictionary;

    [SerializeField] private AudioSource[] audioSources;

    public string curBGM;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        EndBGM();
    }

    private void Init()
    {
        audioDictionary = new Dictionary<string, AudioClip>();
        for (int i = 0; i < audioDatas.Length; i++) audioDictionary.Add(audioDatas[i].audioName, audioDatas[i].audio);
    }

    private void EndBGM()
    {
        if (!audioSources[0].isPlaying)
        {
            if (curBGM != "")
                Play(curBGM, true);
        }
    }

    public void StopBGM()
    {
        curBGM = "";
        audioSources[0].Stop();
    }

    public void Play(string name, bool _isBgm, float pitch = 1.0f)
    {
        if (audioDictionary[name] == null)
            return;

        if (_isBgm) // BGM 배경음악 재생
        {
            curBGM = name;
            if (audioSources[0].isPlaying)
                audioSources[0].Stop();

            audioSources[0].pitch = pitch;
            audioSources[0].clip = audioDictionary[name];
            audioSources[0].Play();
        }
        else // Effect 효과음 재생
        {
            audioSources[1].pitch = pitch;
            audioSources[1].PlayOneShot(audioDictionary[name]);
        }
    }
}
