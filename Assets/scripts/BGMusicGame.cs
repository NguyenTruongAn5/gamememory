using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicGame : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        if (GameSetting.Instance.IsSoundEffectMutedPermamently() == false)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }         
    }
}
