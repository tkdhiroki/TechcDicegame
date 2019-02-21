using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NumberSelect : MonoBehaviour
{
    [SerializeField, Range(0, 9)]
    private int numId; // 0 -> 0
    [SerializeField]
    private EditNumber editNumber;  // 外部スクリプト参照

    private Button numButton;

    private void Start()
    {
        numButton = this.GetComponent<Button>();
    }

    private void Update()
    {
        if (editNumber.NowNumber < 3) return;

        if (editNumber.NowNumber > 3)
            ButtonActive(false);
        else if (editNumber.NowNumber < 4)
            ButtonActive(true);
    }

    public void OnClick()
    {
        editNumber.ClickedNumber(numId);
        editNumber.HideFrag = true;
    }

    // 入力がMaxになったときボタン入力をできなくする
    private void ButtonActive(bool flag)
    {
        if (flag)
            numButton.interactable = true;    // Button On
        else if (!flag)
            numButton.interactable = false;   // Button Off
    }
}
