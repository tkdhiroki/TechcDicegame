using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Alphabet : MonoBehaviour
{
    enum SceneName
    {
        title,
        CharctorSelect,
        Size,
    }

    [SerializeField]
    private GameObject firstAlphabet; // 最初にfocusされるオブジェクト -> A
    public GameObject FocusAlphabet { get { return firstAlphabet; } set { firstAlphabet = value; } }

    [SerializeField]
    private Image bButton;

    private int nowName = 0; // 何番目の名前を入力中か確認
    public int NowName { get { return nowName; } }
    // 選んだ文字を表示するList
    [SerializeField]
    private Text[] decideText = new Text[10];

    // 選んだ文字を格納するList
    [HideInInspector]
    public List<string> alphabetName = new List<string>();

    // 完成したユーザーの名前
    private static string userNames;
    /// <summary>
    /// 決定したユーザーの名前
    /// </summary>
    public static string UserNames { get { return userNames; } }

    private SoundManager _soundManager;

    private int count = 0;

    private GameObject activeObject;
    private void Start()
    {
        // 初期化処理
        userNames = "";
        count = 0;
        alphabetName.Clear();
        ButtonAlphaChange(120);
        // 一度名前を打っているならその値を獲得
        string name = PlayerPrefs.GetString(GameCommon.PlayerSelectName);
        //for (int i = 0; i < name.Length; i++)
        //{
        //    decideText[i].text = name.Substring(i, 1);
        //}
        foreach (var d in name.Select((item, index) => new { item, index }))
        {
            decideText[d.index].text = d.item.ToString();
            alphabetName.Add(d.item.ToString());
            count++;
        }
        // けつの場所から始めるため
        nowName = count;
        // focusオブジェクトを設定
        EventSystem.current.SetSelectedGameObject(firstAlphabet);
        var sm = GameObject.Find("SoundManager");
        if (sm != null)
            _soundManager = sm.GetComponent<SoundManager>();
        PlayerPrefs.SetString(GameCommon.PlayerSelectName, userNames);
    }

    private void Update()
    {
        if (activeObject != EventSystem.current.currentSelectedGameObject)
        {
            if (_soundManager != null)
                _soundManager.PlaySe((int)SoundManager.SeType.Select);
        }

        EnterKey();
        activeObject = EventSystem.current.currentSelectedGameObject;
        
        if (nowName < 1)
            ButtonAlphaChange(120);
        else if (nowName != 0)
            ButtonAlphaChange(200);
    }

    /// <summary>
    /// click時に画像を変える
    /// 引数にアルファベットのID番号 0 -> A , 25 -> Z
    /// </summary>
    public void ClickedNumber(string alphabetText)
    {
        Debug.Log(nowName);
        // クリックしたときにテキストを変える
        decideText[nowName].text = alphabetText;
        nowName++; // 次の文字の入力に移る
        if (_soundManager != null)
            _soundManager.PlaySe((int)SoundManager.SeType.Select);
    }

    void EnterKey()
    {
        // Aボタン入力  一文字削除ebug.Log("a");
        if (nowName > 0)
        {
            if (Input.GetButtonDown("Abutton") || Input.GetMouseButtonDown(1))
            {
                decideText[--nowName].text = "";
                alphabetName.RemoveAt(nowName);
                EventSystem.current.SetSelectedGameObject(firstAlphabet);
                if (_soundManager != null)
                    _soundManager.PlaySe((int)SoundManager.SeType.Cancel);
            }
        }
        // Bボタン入力　入力キー
        // defaultでsubmitにBボタン入力が定められている

        // Yボタン入力　名前決定キー
        // Scene遷移 パスワード入力画面へ
        if (Input.GetButtonDown("Bbutton") || Input.GetKeyDown(KeyCode.Space))
        {
            if (nowName < 1) return;

            for (int i = 0; i < nowName; i++)
            {
                userNames += alphabetName[i];
            }
            if (_soundManager != null)
                _soundManager.PlaySe((int)SoundManager.SeType.Select);
            // ユーザー名を保存
            PlayerPrefs.SetString(GameCommon.PlayerSelectName, userNames);
            SceneManager.LoadScene(SceneName.CharctorSelect.ToString());
        }

        // Xボタン入力　タイトル戻るキー
        if (Input.GetButtonDown("Xbutton") || Input.GetKeyDown(KeyCode.RightShift))
        {
            SceneManager.LoadScene(SceneName.title.ToString());
            if (_soundManager != null)
                _soundManager.PlaySe((int)SoundManager.SeType.Cancel);
        }

        // Debug用
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < nowName; i++)
            {
                userNames += alphabetName[i];
            }
            Debug.Log(userNames);
            if (_soundManager != null)
                _soundManager.PlaySe((int)SoundManager.SeType.CursolMove);
        }

    }

    private void ButtonAlphaChange(int alphaNum)
    {
        int alpha = alphaNum;
        Color color = bButton.color;
        color.a = alpha / 255f;
        bButton.color = color;
    }
}