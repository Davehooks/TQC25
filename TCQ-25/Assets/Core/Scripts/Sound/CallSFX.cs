using UnityEngine;

public class CallSFX : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip[] AudioClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void CallSfx(int i)
    {
        if (AudioClip != null)
        {
            AudioSource.clip = AudioClip[i];
            if (!AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
        }
    }
    public void CallSfxAnimation(int i)
    {
        if (AudioClip != null)
        {
            AudioSource.clip = AudioClip[i];
            if (!AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
        }
    }
    public void StopSFX()
    {
        if (AudioSource.isPlaying)
        {
            AudioSource.Stop();
        }
    }
}
