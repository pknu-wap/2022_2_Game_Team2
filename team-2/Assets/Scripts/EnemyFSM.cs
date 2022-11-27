using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFSM : MonoBehaviour
{
    private NavMeshAgent smith;
    //public float moveSpeed = 5f;
    public float attackDistance = 2f;//���� ��Ÿ�
    public float findDistance = 8f;//�÷��̾� �ν� �Ÿ�
    public float stopDistance = 100;//Idle���·� ���ƿ��� �Ÿ� 
    public int attackPower = 3;//���ݷ�
    float extraRotationSpeed = 5f;
    
    Transform player;//�÷��̾��� ��ǥ ��������

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

    }

    public enum EnemyState{
        Idle,//�⺻ ����(������ ����)
        Move,//�÷��̾ �����ϱ� ���� �����̴� ����
        Attack,//�÷��̾ �����Ϸ��� ����
        Attacking//���ݸ���� ó���ϴ� ���� 2 1.3 44
    }
    float currentTime = 0;//���ݼӵ��� ���̴� �ð�����
    float attackDelay = 2f;//���ݰ� ����
    public EnemyState m_State;//���� ����
    void Awake()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Test_Player").transform;//�÷��̾��� ��ġ�� ������
        smith = GetComponent<NavMeshAgent>();//���ʹ��� �׺�޽ÿ�����Ʈ ��������
        smith.speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 lookrotation = smith.steeringTarget-transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(lookrotation), extraRotationSpeed*Time.deltaTime); 

        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
        }
    }

    void Idle()
    {
        if(Vector3.Distance(transform.position, player.position) <= findDistance)//�÷��̾ �ν� �Ÿ� ���� ������ �����̴� ���·� ��ȯ
        {
            m_State = EnemyState.Move;
            Debug.Log("Idle -> Move");
        }
    }

    void Move()
    {
        float Distance = Vector3.Distance(transform.position, player.position);//����ȭ

        if (Distance > attackDistance && Distance <= stopDistance)//�÷��̾ ���� ��Ÿ����� �ְ�, ���� ������������ Ż������ �ʾ����� ��ã��� �÷��̾� ã�ư���
        {
            //Vector3 dir = (player.position - transform.position).normalized;

            //cc.Move(dir * moveSpeed * Time.deltaTime);

            //smith.isStopped = true;

            //smith.ResetPath();

            //smith.stoppingDistance = attackDistance;

            smith.SetDestination(player.position);
        }
        else if (Distance > stopDistance)// �÷��̾ ���������� Ż�������� Idle���·� ��ȯ
        {
            m_State = EnemyState.Idle;
            Debug.Log("Move -> Idle");
        }
        else if(Distance <= attackDistance)//�÷��̾ ���ݻ�Ÿ� ���϶�(else if��?)
        {
            m_State = EnemyState.Attack;
            Debug.Log("Move -> Attack");
            currentTime = attackDelay;//���� ���� ��ȯ �� �ٷ� ������ �� �ְ� ����
        }

    }

    void Attack()//���� ���� ���� �÷��̾ ���� ��Ÿ� ���̸�currentTime�� ���� attackDelay���� ũ�� ����, ���� �� ���ݸ�� ���·� �ѱ�
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                Debug.Log("Attack");
                //player.GetComponent<Player2>().DamageAction(attackPower);//player��ũ��Ʈ�� �ִ� �÷��̾� ���� �Լ��� ������ ����
                currentTime = 0;//���� �� �����̸� ���� 0���� 
                m_State = EnemyState.Attacking;
            }
            
        }
        else//�÷��̾ ���� ��Ÿ����� ����� Move���·� ��ȯ
        {
            m_State = EnemyState.Move;
            Debug.Log("Attack -> Move");
        }
    }

    void Attacking()//���� ��� ���¿��� ���� �Լ�
    {
        StartCoroutine(Attackmotion());//�ڷ�ƾ�Լ��� �̿�
    }

    IEnumerator Attackmotion()//���� ��� �� ó��
    {
        yield return new WaitForSeconds(0.5f);//0.5�� �Ŀ� ���¸� attack���� ����->0.5�� ���� Attacking ���°� �����Ǹ� �� ������ ���ʹ̰� ������ ����
        m_State = EnemyState.Attack;          //
        StopCoroutine(Attackmotion());        //Attacking ���¿��� Attacking �Լ��� ���� �� ����ʿ� ���� �ڷ�ƾ �Լ��� ���� �� �Լ��� �̿��� ���� �ڷ�ƾ �Լ� ����
    }

}
