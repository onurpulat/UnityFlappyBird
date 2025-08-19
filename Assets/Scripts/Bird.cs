using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float m_flyVelocity = 5.0f;
    [SerializeField] private float m_maxAngle = 45;
    [SerializeField] private float m_minAngle = -90;
    [SerializeField] private float m_rotationSpeed = 10;

    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();

        Reset();
    }

    public void Reset()
    {
        _rigid.gravityScale = 0;
        _rigid.linearVelocity = Vector2.zero;
        transform.SetPositionAndRotation(new Vector3(-1.5f, 0, 0), Quaternion.Euler(0, 0, 0));
    }

    public void StartFly()
    {
        _rigid.gravityScale = 1;

        ForceFly();
    }

    public void ForceFly()
    {
        SoundManager.Instance.PlaySound(SoundManager.ESound.Wing);
        _rigid.linearVelocity = Vector2.up * m_flyVelocity;
    }

    public void CheckRotation()
    {
        float newZ = Mathf.Clamp(_rigid.linearVelocity.y * m_rotationSpeed, m_minAngle, m_maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, newZ);
    }

    public void GameOver()
    {
        _rigid.gravityScale = 0;
        _rigid.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.GameState != GameManager.EGameState.Play) return;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SoundManager.Instance.PlaySound(SoundManager.ESound.Hit);
            SoundManager.Instance.PlaySound(SoundManager.ESound.Die);
            GameManager.Instance.GameOver();
        }
        else if (collision.gameObject.CompareTag("Score"))
        {
            SoundManager.Instance.PlaySound(SoundManager.ESound.Point);
            GameManager.Instance.IncreaseScore();
        }
    }
}
