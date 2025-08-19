using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    private bool _isPlaying;

    private void OnDisable()
    {
        _isPlaying = false;
    }

    private void Update()
    {
        if (_isPlaying && !_source.isPlaying)
        {
            SoundManager.Instance.ReturnToPool(this);
        }
    }

    public void Play(AudioClip clip)
    {
        _isPlaying = true;
        _source.clip = clip;
        _source.Play();
    }
}
