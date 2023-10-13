using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{

    public static ObjectPoolManager instance;

    public int defaultCapacity = 10;
    public int maxPoolSize = 15;

    public IObjectPool<GameObject> objPool { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);


        Init();
    }

    private void Init()
    {
        objPool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        // �̸� ������Ʈ ���� �س���
        for (int i = 0; i < defaultCapacity; i++)
        {
            Monster monster = CreatePooledItem().GetComponent<Monster>();
            monster.Pool.Release(monster.gameObject);
            Debug.Log("���ͻ���");
        }
    }

    // ����
    private GameObject CreatePooledItem()
    {
        int i = Random.Range(1, 3);
        Debug.Log((Define.MonsterType)i);
        GameObject poolGo = Instantiate(Resources.Load("Prefabs/Obstacle/" + (Define.MonsterType)i) as GameObject);
        if((Define.MonsterType)i == Define.MonsterType.Eagle)
        {
            poolGo.GetComponent<Monster>().MType = Define.MonsterType.Eagle;
        }
        else 
        {
            poolGo.GetComponent<Monster>().MType = Define.MonsterType.Cow;
        }
        poolGo.GetComponent<Monster>().Pool = objPool;
        Debug.Log("1");
        return poolGo;
    }

    // ���
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // ��ȯ
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // ����
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}




