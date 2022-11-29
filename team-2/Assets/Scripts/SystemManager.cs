using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    // �ý��� �Ŵ����� ���� UI �� ������ ���.
    public Player player;
    public Camera playerCamera;
    public GameObject MainMenu;
    public GameObject scanOBJ;
    public NPC npc_Cat;
    public GameManager gameManager;
    public GameObject pause_Panel;
    public GameObject mainMenu_Panel;
    public GameObject talkPanel;
    public GameObject Information_Panel;
    public GameObject Tutorial_Panel;
    public Text talkText;
    public Text nameText;
    public bool isAction;
    public bool isSelectInformation;
    public bool isInformationAction = false;

    public GameObject startMessage;

    public int talkId = 0;
    public int informationId = -1;
    public int contentNum = 0;
    public int informationNum = 0;

    public void PauseCancel()
    {
        gameManager.PauseFunc();
    }

    public void MainMenuButton()
    {
        gameManager.PauseFunc();
        mainMenu_Panel.SetActive(true);
        player.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
        Cursor.visible = true;
    }

    public void NewGameButton()
    {
        mainMenu_Panel.SetActive(false);
        PlayerPrefs.DeleteAll();
        gameManager.Gameload();
        MainMenu.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        startMessage.SetActive(true);
        Tutorial_Panel.SetActive(true);
    }

    public void ContinueGameButton()
    {
        mainMenu_Panel.SetActive(false);
        gameManager.Gameload();
        MainMenu.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        Cursor.visible = false;
    }

    public void TutorialButton()
    {
        Tutorial_Panel.SetActive(false);
        Cursor.visible = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void narration(GameObject OBJ = null)
    {
        if(OBJ.CompareTag("Door"))
        {
            nameText.text = "�����";
            talkText.text = "�ź�ο� ���� ���ؼ� ���� ������ �ʴ´�. ������ ã�ƺ���.";
            talkPanel.SetActive(true);
            StartCoroutine(TextPanelOut());
        }
    }

    public void PlayerText(GameObject scanObject)
    {
        if(scanObject.CompareTag("Broken_Door") && isAction == false)
        {
            isAction = true;
            nameText.text = "Player";
            talkText.text = "���� ������ �ʴ´�...";
        }
        else
        {
            isAction = false;
        }
        talkPanel.SetActive(isAction);   
    }

    public void StartMessage()
    {
        nameText.text = "NPC_CAT";
        talkText.text = "�ȳ�? ���� ��ȭ���� ������?";
        talkPanel.SetActive(true);
        StartCoroutine(TextPanelOut());
    }

    public void SetTextPanel(GameObject scanObject)
    {
        string content = npc_Cat.GetContent(talkId, contentNum);
        Debug.Log("content : " + content);
        if(content == null)
        {
            Debug.Log("����Ʈ����");
            if(talkId >= 2)
            {
                if(isInformationAction == false)
                {
                    isInformationAction = true;
                    SetInformationPanel();
                }
                else 
                {
                    GetInformation(informationId, informationNum);
                }
                return;
            }
            contentNum = 0;
            talkId = talkId >= 2 ? 2 : talkId+1;
            isAction = false; 
        }
        else {
            Debug.Log("����Ʈ�־�");
            isAction = true;
            scanOBJ = scanObject;
            nameText.text = scanOBJ.name;
            talkText.text = content;
            contentNum++;
        }

        talkPanel.SetActive(isAction);
    }

    public void SetInformationPanel()
    {
        isSelectInformation = true;
        Cursor.visible = true;
        Information_Panel.SetActive(true);
    }

    public void GetInformation(int information_id, int information_num)
    {
        string information = npc_Cat.GetInformation(information_id, information_num);

        if(information == null)
        {
            isInformationAction = false;
            informationNum = 0;
            informationId = -1;
            isAction = false;
            contentNum = 0;

            talkPanel.SetActive(isAction);
        }
        else
        {
            talkText.text = information;
            informationNum++;
        }
    }

    public void MazeInformation()
    {
        Cursor.visible = false;
        informationId = 0;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }

    public void JumpInformation()
    {
        Cursor.visible = false;
        informationId = 1;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }

    public void TreasureInformation()
    {
        Cursor.visible = false;
        informationId = 2;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }

    public void RCPInformation()
    {
        Cursor.visible = false;
        informationId = 3;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }

    public void BossInformation()
    {
        Cursor.visible = false;
        informationId = 4;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }

    IEnumerator TextPanelOut()
    {
        yield return new WaitForSeconds(2.0f);
        talkPanel.SetActive(false);
    }
}
