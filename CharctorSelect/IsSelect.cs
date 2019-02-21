using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsSelect : MonoBehaviour
{
    [SerializeField]
    GameObject firstChara;
    private GameObject selectChara;
    // 選択したキャラクターを渡す関数
    public GameObject SelectChara { get { return selectChara; } }

    [SerializeField]
    private DeployChara _deployChara;

    // charctorがLookする地点
    [SerializeField]
    private GameObject lookObject;

    private void Start ()
    {
        this.transform.position = firstChara.transform.position;
    }

    private void OnTriggerStay (Collider other)
    {
        if (selectChara != null)
        {
            selectChara.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
            //selectChara.transform.rotation = new Quaternion(90, 0, 0, 0);
        }
        selectChara = other.gameObject;
        other.gameObject.transform.localScale = new Vector3 (2.0f, 2.0f, 2.0f);
        other.gameObject.transform.Rotate (new Vector3 (0, 1, 0), 0.5f);

        //  オブジェクトの選択（選択したオブジェクトのインデックスを取得）
        if (Input.GetButtonDown ("Bbutton"))
        {
            var selectNum = _deployChara.SelectIndex (other.gameObject);
            PlayerPrefs.SetInt (GameCommon.PlayerSelectUnit, selectNum);
            SceneManager.LoadScene ("LoadScene");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.LookAt(lookObject.transform);
    }
}