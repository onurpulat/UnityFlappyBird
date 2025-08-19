using System.Collections;
using UnityEngine;

public class PipeManager : ManagerBase<PipeManager>
{
    [SerializeField] private Pipe m_pipePrefab;
    [SerializeField] private Transform m_pipeParent;

    private ObjectPool<Pipe> _pipePool;

    private Coroutine _spawnCoroutine;

    protected override void Awake()
    {
        base.Awake();

        _pipePool = new ObjectPool<Pipe>(m_pipePrefab, m_pipeParent, 4, 10, false);
    }

    public void ReturnToPool(Pipe pipe) => _pipePool.ReturnToPool(pipe);
    public void StartSpawn() => _spawnCoroutine = StartCoroutine(SpawnPipe());
    public void StopSpawn()
    {
        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }

    private IEnumerator SpawnPipe()
    {
        while (true)
        {
            Pipe pipe = _pipePool.GetFromPool();
            pipe.transform.position = new Vector3(10f, Random.Range(-1f, 3f), 0f);

            yield return new WaitForSeconds(1.25f);
        }
    }
}
