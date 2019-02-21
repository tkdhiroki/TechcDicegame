using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeployChara : MonoBehaviour
{

    [SerializeField]
    private float radius; // 半径
    private float angleDiff;
    public float AngleDiff { get { return angleDiff; } }
    List<GameObject> playerList = new List<GameObject> ();

    void Start ()
    {
        Deploy ();
    }

    private void OnValidate ()
    {
        Deploy ();
    }
    private void Deploy ()
    {
        // リストの中身をクリア
        playerList.Clear ();
        // 再登録
        foreach (Transform player in this.transform)
        {
            playerList.Add (player.gameObject);
        }

        // object間の角度を求める
        angleDiff = 360f / (float) playerList.Count;

        // オブジェクトを円周上に配置
        for (int i = 0; i < playerList.Count; i++)
        {
            Vector3 playerPos = this.transform.position;

            float angle = (90 - angleDiff * i) * Mathf.Deg2Rad;
            playerPos.x += radius * Mathf.Cos (angle);
            playerPos.z += radius * Mathf.Sin (angle);

            playerList[i].transform.position = playerPos;
        }

    }

    /// <summary>
    /// 選択したオブジェクトのリストインデックスを取得
    /// </summary>
    /// <param name="target">対象オブジェクト</param>
    /// <returns></returns>
    public int SelectIndex (GameObject target)
    {
        var selectNum = playerList.FindIndex (x => x == target);
        return selectNum;
    }
}