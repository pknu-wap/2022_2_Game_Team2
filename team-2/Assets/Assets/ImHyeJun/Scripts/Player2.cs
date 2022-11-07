using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public enum playState//�÷��̾� ���� ����
    {
        Normal,
        Stun
    }
    float hp = 15f;
    float hAxis, vAxis;     // ��� �������� �̵��� ������ �Է¹޾��� ����.
    float playerSpeed = 5;  // �÷��̾��� �⺻ �̵��ӵ�.
    float jumpPower = 5;    // �÷��̾��� ������

    bool isJump;            // ���� ������ Ȯ������ bool����.

    bool jDown;             // ���� Ű
    bool iDown;             // ��ȣ�ۿ� Ű
        
    public bool isLoading;  // �ε����϶� �÷��̾� �Ͻ��������(������ �� ���� x).

    Vector3 movingWay;      // �÷��̾ ���ư� ����

    Rigidbody rigid;        // �÷��̾��� ������ٵ�.

    GameObject clickObject;  // �÷��̾ ��ȣ�ۿ� �� ������Ʈ�� �־��� ����.

    public GameManager gameManager; // ���ӸŴ���
    public playState p_State;
    public Transform enemypos;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        p_State = playState.Normal;//�÷��̾� ���¸� �⺻ ���·� 
    }

    void Update()
    {
        if(p_State == playState.Stun)//����(�� ��ġ�� �÷��̾� �������) ���°� �Ǹ� ����
        {
            Vector3 way = (enemypos.position - transform.position).normalized;//�÷��̾ ��ܿ��� ���� ���ͷ� ������ ����ϰ�
            transform.Translate(way * 2 * Time.deltaTime, Space.World);//���� ���� �ش� ����ŭ ��ܿ´�.
            return;//�÷��̾ ���� ���¸� ���� ����� �����Ű�� ����->������� ������ �Է��� ��������
        }
        GetInput();
        Move();
        Jump();
        Interaction();
    }

    void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        jDown = Input.GetButtonDown("Jump");
        iDown = Input.GetKeyDown(KeyCode.E);
    }

    private void Move()
    {
        if (!isLoading)
        {
            movingWay = new Vector3(hAxis, 0, vAxis).normalized;

            transform.Translate(movingWay * playerSpeed * Time.deltaTime);
            //transform.position += movingWay * playerSpeed * Time.deltaTime;
        }
    }

    void Jump()
    {
        if (jDown && !isJump && !isLoading)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }
    }

    void Interaction()
    {
        if (iDown)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2.5f)) // ������, ������ ���� ���, ��Ÿ�
            {
                if (hit.collider.CompareTag("Door"))
                {
                    clickObject = hit.collider.gameObject;
                    gameManager.Field_Change(clickObject);
                    isLoading = true;
                }
            }
            Debug.Log(clickObject.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
        }
    }

    public void DamageAction(int damage)
    {
        hp -= damage;
        print("�÷��̾� ü��: " + hp);
    }
}
