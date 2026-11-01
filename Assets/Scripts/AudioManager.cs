using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private AudioSource boostAudio;
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.5f;
    [SerializeField] private float fadeSpeed = 1.5f;

    private void Start()
    {
        engineAudio.Stop();
    }

    public void PlayBoostSound()
    {
        if (!boostAudio.isPlaying)
        {
            boostAudio.Play();
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
