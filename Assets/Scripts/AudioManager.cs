using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private AudioSource boostAudio;
    [SerializeField] private AudioSource driftAudio;
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.5f;
    [SerializeField] private float fadeSpeed = 1.5f;

    private void Start()
    {
        engineAudio.Stop();
        boostAudio.Stop();
        driftAudio.Stop();
    }

    private void OnEnable()
    {
        PlayerMovement.playBoostSoundEvent += HandleBoostSound;
        PlayerMovement.playDriftSoundEvent += HandleDriftSound;
    }

    private void OnDisable()
    {
        PlayerMovement.playBoostSoundEvent -= HandleBoostSound;
        PlayerMovement.playDriftSoundEvent -= HandleDriftSound;
    }   

    public void HandleBoostSound(object sender, EventArgs args)
    {
        if (!boostAudio.isPlaying)
        {
            boostAudio.Play();
        }
    }

    public void HandleDriftSound(object sender, EventArgs args)
    {
        if (!driftAudio.isPlaying)
        {
            driftAudio.Play();
        }
    }

    public void ManageEngineSound(float speed)
    {
        if (Mathf.Abs(speed) > 0.1f)
        {
            if (!engineAudio.isPlaying)
            {
                engineAudio.Play();
            }

            // Fondu entrant
            engineAudio.volume = Mathf.MoveTowards(
                engineAudio.volume,
                1f,
                fadeSpeed * Time.deltaTime
            );
        }
        else
        {
            // Fondu sortant
            engineAudio.volume = Mathf.MoveTowards(
                engineAudio.volume,
                0f,
                fadeSpeed * Time.deltaTime
            );

            // Arrêt lorsque le volume est nul
            if (engineAudio.volume <= 0.001f)
                engineAudio.Stop();
        }
    }
}
