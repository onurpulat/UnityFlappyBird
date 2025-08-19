using System.Collections;
using UnityEngine;

public class GroundParallax : MonoBehaviour
{
    [SerializeField] private float m_animationSpeed = 1.0f;

    private MeshRenderer _meshRenderer;
    private Coroutine _parallaxCoroutine;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void StartParallax() => _parallaxCoroutine = StartCoroutine(ParallaxCoroutine());
    public void StopParallax() => StopCoroutine(_parallaxCoroutine);

    private IEnumerator ParallaxCoroutine()
    {
        while (true)
        {
            _meshRenderer.sharedMaterial.mainTextureOffset += m_animationSpeed * Time.deltaTime * Vector2.right;
            yield return null;
        }
    }
}
