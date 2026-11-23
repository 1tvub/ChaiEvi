using UnityEngine;

// Singleton: müzik menüden oyuna geçerken kesilmeden devam eder
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = GetComponent<AudioSource>();
        musicSource.clip = Resources.Load<AudioClip>("Cay_Bahcesi_Muzik");
        musicSource.loop = true;
        musicSource.volume = 0.6f;
        musicSource.Play();
    }

    public static void Olustur()
    {
        if (Instance == null)
            new GameObject("AudioManager").AddComponent<AudioManager>();
    }
}
