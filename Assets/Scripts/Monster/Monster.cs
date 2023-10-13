using UnityEngine;
using UnityEngine.Pool;

public class Monster : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    private int monsterDamage = 2;
    private bool isMove = true;
    private float moveSpeed = 1f;
    protected Define.MonsterType Type;

    public Define.MonsterType MType { get { return Type; } set { } }

    public int MonsterDamage { get { return monsterDamage; } }

    public virtual void Init()
    {

    }
    void MoveMonster()
    {
        if (isMove) { gameObject.transform.position += new Vector3(-1, 0, gameObject.transform.position.z) * moveSpeed * Time.deltaTime; }
        else { return; }
    }

    private void Update()
    {
        MoveMonster();

        if (gameObject.transform.position.x < -10f)
        {
            gameObject.SetActive(false);
        }
    }
}




