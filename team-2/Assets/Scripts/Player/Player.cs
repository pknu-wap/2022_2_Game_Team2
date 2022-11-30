using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int life = 2; // �����.
    public bool live;
    
    float hAxis, vAxis;     // ��� �������� �̵��� ������ �Է¹޾��� ����.
    float playerSpeed = 10;  // �÷��̾��� �⺻ �̵��ӵ�.
    float jumpPower = 8.0f;    // �÷��̾��� ������

    bool isJump;            // ���� ������ Ȯ������ bool����.

    bool jDown;             // ���� Ű
    bool iDown;             // ��ȣ�ۿ� Ű
    bool pauseDown;         // pause Button
        
    public bool isLoading;  // �ε����϶� �÷��̾� �Ͻ��������(������ �� ���� x).

    Vector3 movingWay;      // �÷��̾ ���ư� ����

    Rigidbody rigid;        // �÷��̾��� ������ٵ�.

    public GameObject clickObject;  // �÷��̾ ��ȣ�ۿ� �� ������Ʈ�� �־��� ����.

    public GameManager gameManager; // ���ӸŴ���
    public SystemManager systemManager; // �ý��� �Ŵ���
    public RSP rsp;

    //public GameObject feildPointObj; // �ʵ��̵��� ���Ǵ� ����Ʈ ���� üũ�ϴ� ���� 

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        isJump = false; 
        live = true;
    }

    void Update()
    {
        GetInput();
        Move();
        Jump();
        Interaction();
        if(pauseDown == true)
        {
            gameManager.PauseFunc();
        }
    }

    void GetInput()
    {
        hAxis = systemManager.isAction ? 0 : Input.GetAxis("Horizontal");
        vAxis = systemManager.isAction ? 0 : Input.GetAxis("Vertical");
        jDown = systemManager.isAction ? false : Input.GetButtonDown("Jump");
        iDown = systemManager.isSelectInformation ? false : Input.GetKeyDown(KeyCode.E);
        pauseDown = Input.GetKeyDown(KeyCode.Escape);
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
            else if(clickObject.CompareTag("NPC"))
            {
                systemManager.SetTextPanel(clickObject.gameObject);
            }
            else if(clickObject.CompareTag("Broken_Door"))
            {
                systemManager.PlayerText(clickObject);
            }
            else if(clickObject.name == "BossRoom In Point")
            {
                Debug.Log(clickObject.name);
                gameManager.BossInRoom();
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
        if(other.gameObject.CompareTag("Monster")&& live == true)
        {
            live = false;
            gameManager.GameOver();
        }
        else if(other.gameObject.CompareTag("Flame")&&live == true)
        {
            live = false;
            gameManager.GameOver();
        }
        else if(other.gameObject.name == "Paper" || other.gameObject.name == "Scissor" || other.gameObject.name == "Rock")
        {
            Debug.Log("���� ��� �ִ°� " + other.gameObject.name);
            rsp.SetPlayerRCP(other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        clickObject = null;
        if(other.name == "StartMessage")
        {
            systemManager.StartMessage();
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("spawnPoint", 1);
        }
    }
}
