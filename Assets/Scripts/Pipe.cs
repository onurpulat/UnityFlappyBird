using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float m_speed = 5.0f;

    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.EGameState.Ready)
            PipeManager.Instance.ReturnToPool(this);
        else if (GameManager.Instance.GameState == GameManager.EGameState.GameOver)
            return;

        transform.position += m_speed * Time.deltaTime * Vector3.left;

        if (transform.position.x < -4.5f)
        {
            PipeManager.Instance.ReturnToPool(this);
        }
    }
}
