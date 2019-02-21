using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {

    [SerializeField]
    private GameObject players;
    [SerializeField]
    DeployChara DCdiff;
    
    private float angle;
    float playerAngle = 3f;
    bool isRotate = false;

    float h;    // Verticalの値
    Vector3 clickStartPos;  // click始点
    Vector3 clickPos;    // clickの変位

    void Start () {
        angle = DCdiff.AngleDiff;
        // Debug.Log(angle);
	}
	
	void Update () {
        h = Input.GetAxis("Horizontal");

        EnterKey();
        FrickRotate();
	}

    void EnterKey()
    {
        if (isRotate) return;

        if (h > 0)
        {
            StartCoroutine(MovePlayer(true));
        }
        if (h < 0)
        {
            StartCoroutine(MovePlayer(false));
        }
    }

    void FrickRotate()
    {
        if (isRotate) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clickStartPos = Input.mousePosition;
            // Debug.Log("start");
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            clickPos = Input.mousePosition;
            GetDirection();
            // Debug.Log("end");
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
        }
    }

    void GetDirection()
    {
        float directionX = clickPos.x - clickStartPos.x;
        string Direction = "";
        
            if (1000 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-1000 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }
        // Debug.Log(Direction);
        // Debug.Log(directionX);
        // Debug.Log(directionY);

        switch (Direction)
        {

            case "right":
                //右フリックされた時の処理
                StartCoroutine(MovePlayer(true));
                break;

            case "left":
                //左フリックされた時の処理
                StartCoroutine(MovePlayer(false));
                break;
            default:
                break;
        }
    }

    IEnumerator MovePlayer(bool sign)
    {
        isRotate = true;

        float sumAangle = 0f;
        while (sumAangle < angle)
        {            
            sumAangle += playerAngle;
            
            if(sign)
                players.transform.Rotate(new Vector3(0, 1, 0), playerAngle);
            else
                players.transform.Rotate(new Vector3(0, 1, 0), -playerAngle);

            yield return null;
        }

        isRotate = false;

        yield break;
    }
}
