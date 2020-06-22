using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObj_NotPool : MonoBehaviour {

    [SerializeField] private LineRenderer line_renderer = null;
    [SerializeField] private BoxCollider boxCollider = null;

    Vector3 moveDirection = Vector3.zero;
    bool isSetDirectionEnd = false;
    private float speed = 30f;

    Vector3 lineEndPosition;
    Vector3 initPosition;//発射された位置
    Vector3 myPosFromEndPosDistance;//先頭から最後尾までのベクトル

    //1つ後ろのレーザーとの位置関係取得用
    Vector3 nextLaserPosition;
    Vector3 nextLaserDirection;

    int debugCount = 0;

    private void Start()
    {
        initPosition = transform.position;
        lineEndPosition = initPosition;
        myPosFromEndPosDistance = initPosition;
        nextLaserPosition = initPosition;
        nextLaserDirection = initPosition;
        debugCount++;
        Debug.Log("start : " + debugCount);
    }

    private void LateUpdate()
    {
        if (isSetDirectionEnd)
        {
            debugCount++;
            Debug.Log("lateUpdate : " + debugCount);
            transform.Translate(moveDirection * Time.deltaTime * speed);

            //後ろのレーザーと自分をラインでつなぐ
            if (nextLaserDirection != initPosition)
            {
                nextLaserPosition += nextLaserDirection * Time.deltaTime * speed;
                myPosFromEndPosDistance = nextLaserPosition - transform.position;
            }
            else
            {
                //後ろにレーザーがなければ自分の進行方向と逆の位置の単位ベクトルを終点にする
                myPosFromEndPosDistance = -moveDirection;
            }

            lineEndPosition = transform.position + myPosFromEndPosDistance;
            if (lineEndPosition != initPosition)
            {
                line_renderer.positionCount = 2;
                line_renderer.SetPosition(0, transform.position);
                line_renderer.SetPosition(1, lineEndPosition);
            }
            if (transform.position.x > 15f || transform.position.y > 15f ||
                transform.position.x < -15f || transform.position.y < -15f)
            {
                Destroy(this.gameObject);
            }

            UpdateColliderSize();
        }
    }
    /// <summary>
    /// レーザーの進行方向を登録
    /// </summary>
    /// <param name="dir"></param>
    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir;
        myPosFromEndPosDistance = -dir;
        isSetDirectionEnd = true;
        debugCount++;
        Debug.Log("setDirection : " + debugCount);
    }
    /// <summary>
    /// ラインの最後尾を登録
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="dir"></param>
    public void SetEndPosition(Vector3 pos, Vector3 dir)
    {
        lineEndPosition = pos;
        nextLaserPosition = pos;
        nextLaserDirection = dir;
        debugCount++;
        Debug.Log("setEndPosition : " + debugCount);
    }
    /// <summary>
    /// コライダー設定
    /// http://www.theappguruz.com/blog/add-collider-to-line-renderer-unity
    /// </summary>
    private void UpdateColliderSize()
    {
        float lineLength = Vector3.Distance(transform.position, lineEndPosition);
        boxCollider.size = new Vector3(lineLength, 0.1f, 1f);
        Vector3 midPoint = (transform.position + lineEndPosition) / 2f;
        boxCollider.transform.position = midPoint;

        float angle = (Mathf.Abs(transform.position.y - lineEndPosition.y) / Mathf.Abs(transform.position.x - lineEndPosition.x));
        if ((transform.position.y < lineEndPosition.y && transform.position.x > lineEndPosition.x) ||
            (lineEndPosition.y < transform.position.y && lineEndPosition.x > transform.position.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        boxCollider.transform.Rotate(0f, 0f, angle);
    }

    public void CollisionAction(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
