using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct AudioContext
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

    [SerializeField] private List<AudioContext> audioClips = new List<AudioContext>();

    private AudioSource source = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound)
    {
        if (source.isPlaying) source.Stop();
        source.clip = audioClips.Find(item => item.name == sound).clip;
        source.Play();
    }
}
