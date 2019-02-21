using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSelect : MonoBehaviour
{
    [SerializeField]
    private Alphabet alphabet; // 外部スクリプト参照
    [SerializeField]
    private Text alphabet_text; // ボタン自身がもつ1つの文字

    private Button alphaButton;

    private SoundManager _soundManager;
    
    private void Start ()
    {
        alphaButton = this.GetComponent<Button> ();
        var sm = GameObject.Find ("SoundManager");
        if (sm != null)
            _soundManager = sm.GetComponent<SoundManager> ();
    }

    private void Update ()
    {
        if (alphabet.NowName < 8) return;

        if (alphabet.NowName > 9)
            ButtonActive(false);
        else if (alphabet.NowName < 10)
            ButtonActive(true);            
        
    }

    public void OnClick ()
    {
        alphabet.ClickedNumber (this.alphabet_text.text);
        alphabet.alphabetName.Add (alphabet_text.text);
        if (_soundManager != null)
            _soundManager.PlaySe ((int) SoundManager.SeType.Select);
        alphabet.FocusAlphabet = this.gameObject;
    }

    private void ButtonActive (bool flag)
    {
        if (flag)
            alphaButton.interactable = true; // Button On
        else if (!flag)
            alphaButton.interactable = false; // Button Off
    }
}