using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditNumber : MonoBehaviour
{
    enum SceneName
    {
        EditName,
        CharctorSelect,
        Size,
    }

    [SerializeField]
    private Sprite[] numbers = new Sprite[11];    // 0 ~ 9の画像とパスワードを見えなくするマークの画像格納
    [SerializeField]
    private GameObject firstNumber;   // 最初にfocusされるオブジェクト -> 1

    private int nowNumber = 0;    // 何番目の名前を入力中か確認
    public int NowNumber { get { return nowNumber; } }

    List<Image> numberList = new List<Image>();
    [SerializeField]
    GameObject numberParent;     // 数字を格納する親のオブジェクト

    int hideCount = 1;

    float hidenum;    // パスワードを隠す処理
    bool hideFrag;    // ボタン入力を確認
    public bool HideFrag { set { hideFrag = value; } }

    private void Start()
    {
        foreach (Transform alpha in numberParent.transform)
        {
            // １０文字分の配列を作る
            numberList.Add(alpha.GetComponent<Image>());
        }
        // focusオブジェクトを設定
        EventSystem.current.SetSelectedGameObject(firstNumber);
    }

    private void Update()
    {
        EnterKey();
        
        if (false == hideFrag) return;

        hidenum += Time.deltaTime;
        if (hidenum > 1.0f)
        {
            HideNumber(nowNumber - 1);
            hidenum = 0.0f;
            hideFrag = false;
        }
        else if (nowNumber > hideCount)
        {
            if (hideCount == 1) return;

            HideNumber(hideCount - 1);
            hidenum = 0.0f;
        }
    }

    /// <summary>
    /// click時に画像を変える
    /// 引数に数字のID番号 0 -> 0
    /// </summary>
    public void ClickedNumber(int num)
    {
        Debug.Log(nowNumber);
        // クリックしたときに画像を変える
        numberList[nowNumber].sprite = numbers[num];

        nowNumber++;  // 次の文字の入力に移る
    }

    void EnterKey()
    {
        // Aボタン入力  一文字削除ebug.Log("a");
        if (nowNumber > 0)
        {
            if (Input.GetButtonDown("Abutton") || Input.GetMouseButtonDown(1))
            {
                numberList[--nowNumber].sprite = null;
                hideCount--;
            }
        }
        // Bボタン入力　入力キー
        // defaultでsubmitにBボタン入力が定められている

        // Yボタン入力　名前決定キー
        // Scene遷移 キャラクターセレクト画面へ
        if (Input.GetButtonDown("Ybutton") || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneName.CharctorSelect.ToString());
        }

        // Xボタン入力　名前入力画面に戻るキー
        if (Input.GetButtonDown("Xbutton") || Input.GetKeyDown(KeyCode.RightShift))
        {
            SceneManager.LoadScene(SceneName.EditName.ToString());
        }
    }
    // 数字を隠す
    void HideNumber(int num)
    {
        numberList[num].sprite = numbers[10];
        hideCount++;       
    }
}
