using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImportRanking : MonoBehaviour
{ 
    private SqliteDatabase sqlDB;

    // 駒のIDの最低の値
    private int frame_min = 0;
    // 駒の数
    private int frame_max = 3;

    // プレイヤーの名前を格納
    private string nameStr;
    // プレイヤーの名前を出力するText
    [SerializeField]
    private Text[] nameTexts = new Text[5];

    // スコアを格納
    private int scoreInt;
    // スコアを出力するText
    [SerializeField]
    private Text[] scoreTexts = new Text[5];

    
    // 駒を格納
    private int frameInt;
    // Inspectorから駒のprefabを入れる。
    [SerializeField]
    private GameObject[] framePrefabs = new GameObject[4];
    // prefabのインスタンス格納
    private GameObject[] frameObjects = new GameObject[5];

    // ゲーム画面上の駒を置く位置座標取得
    [SerializeField]
    private Transform[] putTransform = new Transform[5];


    private void Start()
    {
        sqlDB = new SqliteDatabase("dicegame.db");
        DefaultPlayer();
        SelectOrderScore();
        DelieteDB();
    }

    // DBにアクセスしてスコアをの高い順から取得する
    private void SelectOrderScore()
    {
        int count = 0;

        // scoreを見て降順でソートしている
        string selectQuery = "select * from user order by score desc";
        DataTable dataTable = sqlDB.ExecuteQuery(selectQuery);

        foreach (DataRow dr in dataTable.Rows)
        {
            if (count > 4)
                break;

            nameStr = (string)dr["user_name"];
            scoreInt = (int)dr["score"];
            frameInt = (int)dr["frame"];

            ChangeRankingState(count);

            //Debug.Log(
            //    "name:" + (string)dr["user_name"] +
            //    "score:" + (int)dr["score"] +
            //    "frame:" + (int)dr["frame"]
            //    );
            count++;
        }
    }
    // Rankingの順位変更＋表示変化
    private void ChangeRankingState(int _count)
    {
        // textに名前を入れる
        nameTexts[_count].text = nameStr;
        // textにスコアのToStringをいれる
        scoreTexts[_count].text = scoreInt.ToString();
        // prefabインスタンス生成
        frameObjects[_count] = Instantiate(framePrefabs[frameInt], putTransform[_count].position, Quaternion.identity);

    }



    // 外部スクリプトで書く↓↓

    // 仮のランキング上位プレイヤーをインサート
    private void DefaultPlayer()
    {
        int already = 1000;
        // 重複のインサートを避ける
        string selectQuery = "select * from user where user_id = " + already;

        DataTable alreadyTable = sqlDB.ExecuteQuery(selectQuery);
        
        foreach (DataRow dr in alreadyTable.Rows)
        {
            if ((int)dr["user_id"] == already)
            {
                Debug.Log("既に存在している。");
                return;
            }
        }

        int score_num = 50;
        int frame_rnd = 0;
        // 初期ランキング上位者を設定してインサート
        for (int i = 1; i <= 5; i++)
        {
            frame_rnd = Random.Range(frame_min, frame_max);
            string insertQuery = "insert into user values(" 
                + already + ", 'techc',  0, " 
                + (score_num * i) + ", " 
                + frame_rnd +", 0)";
            sqlDB.ExecuteNonQuery(insertQuery);

            already++;
        }
    }
    // Debug用のDBのカラム削除用
    private void DelieteDB()
    {
        string query = "delete from user";
        sqlDB.ExecuteNonQuery(query);
    }
}
