using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// dicegame.db
/// create table user   テーブル名 user
/// ...> int user_id       ユーザーID
/// ...> string user_name  ユーザーの名前
/// ...> int password      パスワード
/// ...> int score         スコア情報
/// ...> int frame         選んだ駒
/// ...> int point         最終地点( 初期地点は0 )
/// 
/// </summary>
public class UserData : MonoBehaviour
{
    // var sql  
    private SqliteDatabase sqlDB;
    // ユーザーID
    private int userId = 1;
    public int UserId { get { return userId; } set { userId = value; } }

    // DBクラス　作成
    DiceGameDB DB;

    void Start()
    {
        //DB作成
        sqlDB = new SqliteDatabase("dicegame.db");
        // INSERT
        string query = "insert into user values(1, 'hoge', 20, 0, 0, 0)";
        sqlDB.ExecuteNonQuery(query);

        string selectQuery = "select * from user";
        DataTable dataTable = sqlDB.ExecuteQuery(selectQuery);

        int a = 0;
        string b = "";
        int c = 0;
        int d = 0;
        int e = 0;
        int f = 0;
           
        foreach (DataRow dr in dataTable.Rows)
        {
            a = (int)dr["user_id"];
            Debug.Log("user_id:" + a);

            b = (string)dr["user_name"];
            Debug.Log("name:" + b);

            c = (int)dr["password"];
            Debug.Log("password:" + c);

            d = (int)dr["score"];
            Debug.Log("score:" + d);

            e = (int)dr["frame"];
            Debug.Log("frame:" + e);

            f = (int)dr["point"];
            Debug.Log("point:" + f);
        }

        // UPDATE
        string updateQuery = "update user set user_name = 'huge' where user_id = 1";
        sqlDB.ExecuteNonQuery(updateQuery);
        // Select
        selectQuery = "select * from user";
        dataTable = sqlDB.ExecuteQuery(selectQuery);
                       
        foreach (DataRow dr in dataTable.Rows)
        {
            a = (int)dr["user_id"];
            Debug.Log("user_id:" + a);

            b = (string)dr["user_name"];
            Debug.Log("name:" + b);

            c = (int)dr["password"];
            Debug.Log("password:" + c);

            d = (int)dr["score"];
            Debug.Log("score:" + d);

            e = (int)dr["frame"];
            Debug.Log("frame:" + e);

            f = (int)dr["point"];
            Debug.Log("point:" + f);
        }
    }

    /// <summary>
    /// userDB初期化
    /// </summary>
    public void SaveGameStart()
    {
        DB = new DiceGameDB()
        {
            userID = userId++,
            userName = "",
            password = 0000,
            score = 0,
            frame = 0,
            point = 0
        };
        string query = "insert into user values(" + DB.userID + ", " + DB.userName + ", " + DB.password + ", " + DB.score +", " + DB.frame + ", "+ DB.point +")";
        sqlDB.ExecuteNonQuery(query);
    }

    public void SaveEachAction(DiceGameDB db)
    {
        if (db.userName != "")
        {
            string query = "update user ";
            sqlDB.ExecuteNonQuery(query);
        }
    }

    /// <summary>
    /// ゲーム終了時にされるInsert
    /// </summary>
    public void SaveGameEnd()
    {
    }

    private void DelieteDB()
    {
        string query = "delete from user";
        sqlDB.ExecuteNonQuery(query);
    }
}
