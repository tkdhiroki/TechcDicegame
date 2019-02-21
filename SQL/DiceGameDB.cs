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


public class DiceGameDB
{
    // ユーザーID
    public int userID = 0;
    // ユーザー名前
    public string userName = "";
    // パスワード
    public int password = 0000;
    // スコア
    public int score = 0;
    // 選んだ駒
    public int frame = 0;
    // 最終地点
    public int point = 0;

}
