using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SoundManager : ManagerBase<SoundManager>
{
    public enum ESound
    {
        Die,
        Hit,
        Point,
        Wing
    }

    [SerializeField] private SoundObject m_soundObjectPrefab;
    [SerializeField] private Transform m_soundObjectParent;

    [SerializeField] private AudioClip m_dieClip;
    [SerializeField] private AudioClip m_hitClip;
    [SerializeField] private AudioClip m_pointClip;
    [SerializeField] private AudioClip m_wingClip;

    private ObjectPool<SoundObject> _soundPool;

    protected override void Awake()
    {
        base.Awake();

        _soundPool = new ObjectPool<SoundObject>(m_soundObjectPrefab, m_soundObjectParent, 5, 10, true);
    }

    public void PlaySound(ESound sound)
    {
        SoundObject soundObject = _soundPool.GetFromPool();
        AudioClip clip = GetClip(sound);

        soundObject.Play(clip);
    }

    private AudioClip GetClip(ESound sound)
    {
        return sound switch
        {
            ESound.Die => m_dieClip,
            ESound.Hit => m_hitClip,
            ESound.Point => m_pointClip,
            ESound.Wing => m_wingClip,
            _ => null,
        };
    }

    public void ReturnToPool(SoundObject source) => _soundPool.ReturnToPool(source);
}
