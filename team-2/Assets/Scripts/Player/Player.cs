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
        if (iDown && clickObject.CompareTag("Door"))
        {
            Debug.Log("Interaction + " + clickObject.name);
            gameManager.Field_Change(clickObject);
            isLoading = true;
        }
        else if(iDown && clickObject.CompareTag("Artifact"))
        {
            gameManager.Get_Artifact(clickObject);
        }
        /*
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
