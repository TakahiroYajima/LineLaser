using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLaser_NoPool : MonoBehaviour {

    [SerializeField] private LaserObj_NotPool laserObjPref = null;
    private LaserObj_NotPool prevLaser = null;//1つ前に発射したレーザーオブジェクト

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //レーザー発射
        if (Input.GetKey(KeyCode.Space))
        {
            LaserObj_NotPool instance = Instantiate(laserObjPref);
            instance.gameObject.transform.position = transform.position;
            instance.SetDirection(transform.right);
            if (prevLaser != null)
            {
                prevLaser.SetEndPosition(transform.position, transform.right);
            }
            prevLaser = instance;
        }
        //ボタンを離したらレーザーを途切れさせる
        else
        {
            prevLaser = null;
        }

        //以下、発射元の移動・回転の処理

        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotate += 10f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotate -= 10f;
        }
        transform.Rotate(new Vector3(0f, 0f, rotate * Time.deltaTime * 50f));

        Vector3 moveDir = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, y, 0f).normalized;
        transform.position += moveDir * Time.deltaTime * 20f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), Mathf.Clamp(transform.position.y, -4.3f, 6f), 0f);
    }
}
