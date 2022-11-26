using System.Collections;
using UnityEngine;

public class FlameWall : MonoBehaviour
{
    BossPlayer PlayerAttacked;
    private bool FlameCollider;
    void Start()
    {
        StartCoroutine("Flame");
        FlameCollider = gameObject.GetComponent<BoxCollider>().enabled;
        PlayerAttacked = GameObject.Find("Player").GetComponent<BossPlayer>();
    }

    IEnumerator Flame()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject.Find("FlameWallGroup").transform.GetChild(i).gameObject.SetActive(true);
        }
        Debug.Log("광역기 생성됨");
        FlameCollider = true;
        Invoke("FlameFalse", 3f);
        yield return new WaitForSeconds(10f);
        StartCoroutine("Flame");
    }
    void FlameFalse()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject.Find("FlameWallGroup").transform.GetChild(i).gameObject.SetActive(false);
        }
        FlameCollider = false;
        Debug.Log("광역기 사라짐");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerAttacked.isAttacked = true;
            Debug.Log("광역기 접촉함");
        }
    }
}
