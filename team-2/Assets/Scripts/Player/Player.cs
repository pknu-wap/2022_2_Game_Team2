using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int life = 2; // �����.
    
    float hAxis, vAxis;     // ��� �������� �̵��� ������ �Է¹޾��� ����.
    float playerSpeed = 10;  // �÷��̾��� �⺻ �̵��ӵ�.
    float jumpPower = 10;    // �÷��̾��� ������

    bool isJump;            // ���� ������ Ȯ������ bool����.

    bool jDown;             // ���� Ű
    bool iDown;             // ��ȣ�ۿ� Ű
        
    public bool isLoading;  // �ε����϶� �÷��̾� �Ͻ��������(������ �� ���� x).

    Vector3 movingWay;      // �÷��̾ ���ư� ����

    Rigidbody rigid;        // �÷��̾��� ������ٵ�.

    public GameObject clickObject;  // �÷��̾ ��ȣ�ۿ� �� ������Ʈ�� �־��� ����.

    public GameManager gameManager; // ���ӸŴ���

    //public GameObject feildPointObj; // �ʵ��̵��� ���Ǵ� ����Ʈ ���� üũ�ϴ� ���� 

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        isJump = false; 
    }

    void Update()
    {
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
        if(iDown && clickObject != null && !isLoading)
        {
            if (clickObject.CompareTag("Door"))
            {
                Debug.Log("Interaction + " + clickObject.name);
                isLoading = true;
                gameManager.Field_Change(clickObject);
            }
            else if(clickObject.CompareTag("Artifact"))
            {
                gameManager.Get_Artifact(clickObject);
                clickObject = null;
            }
            else if(clickObject.CompareTag("Treasure"))
            {
                clickObject.GetComponent<Treasure>().OpenBox();
                clickObject = null;
            }
        }
        /*
        if (iDown && clickObject.CompareTag("Door") && !isLoading)
        {
            Debug.Log("Interaction + " + clickObject.name);
            isLoading = true;
            gameManager.Field_Change(clickObject);
        }
        else if(iDown && clickObject.CompareTag("Artifact"))
        {
            gameManager.Get_Artifact(clickObject);
        }
        else if(iDown && clickObject.CompareTag("Treasure"))
        {
            clickObject.GetComponent<Treasure>().OpenBox();
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        clickObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        clickObject = null;
    }
}
