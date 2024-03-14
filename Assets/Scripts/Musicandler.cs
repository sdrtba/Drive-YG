using UnityEngine;

public class Musicandler : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    private int _index = 0;

    void Start()
    {
        audioSource.clip = audioClips[_index];
        audioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            _index = (_index + 1) % 4;
            audioSource.clip = audioClips[_index];
            audioSource.Play();
        }
    }
}
