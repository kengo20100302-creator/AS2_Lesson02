using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header(" * * * 移動値の設定")]
    private Vector3 inputMoveVelocity;

    [Header("** Shot Settings **")]
    public Transform shotPoint;  //撃ちだし座標
    public GameObject bulletPrefab;

    [Header(" * * * 回転軸の設定")]
    public GameObject lookAxis;  //向きベクトル軸(オブジェクト)
    public GameObject gyroAxis;  //ジャイロベクトル軸(オブジェクト)
    private Vector3 loolAngls;
    private float gyroAngls;

    //バリア設定
    [Header(" * * * バリアの設定")]
    public GameObject barrire;           //バリアオブジェクトの参照
    public MeshRenderer barrireRenderer; //バリアのレンダラー参照
    public bool barrireActivation;　     //バリアのフラグ

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float zSpeed = 5 * Time.deltaTime;
        transform.Translate(0, 0, zSpeed);
        //移動する方向に回転
        loolAngls.x += inputMoveVelocity.y;
        loolAngls.y += inputMoveVelocity.x;

        loolAngls.x = Mathf.Clamp(loolAngls.x, -15, 15);
        loolAngls.y = Mathf.Clamp(loolAngls.y, -15, 15);
        gyroAngls = Mathf.Clamp(gyroAngls, -15, 15);

        //角度の代入
        lookAxis.transform.eulerAngles = loolAngls;
        gyroAxis.transform.eulerAngles = new Vector3(0, 0, gyroAngls);

        loolAngls = Vector3.Lerp(loolAngls, Vector3.zero, Time.deltaTime);
        gyroAngls = Mathf.Lerp(gyroAngls, 0, Time.deltaTime);



    }
    //PlayerInputから[Move]アクションを呼び出すメソッド
    public void OnMove(InputValue value)
    {
        Debug.Log($"移動[{value.Get<Vector2>()}]"); Vector3
            move = new Vector3(value.Get<Vector2>().x,
            value.Get<Vector2>().y, 0);


        //第一引数playerinputから渡された
        if (transform.position.x + value.Get<Vector2>().x < -8 ||
             transform.position.x + value.Get<Vector2>().x > 8)
            return;

        if (transform.position.y + value.Get<Vector2>().y < -4 ||
             transform.position.y + value.Get<Vector2>().y > 6)
            return;

        move.x = Mathf.Round(move.x);
        move.y = Mathf.Round(move.y);
        transform.Translate(move);

        inputMoveVelocity = move;

        Debug.Log(inputMoveVelocity);
    }
    //PlayerInputから[]アクションを呼び出すメソッド
    public void OnAttack(InputValue value)
    {
        Debug.Log($"攻撃アクション[{value.Get<float>()}]");
        GameObject origin = bulletPrefab;
        Vector3 position = shotPoint.position;
        Quaternion rotation = shotPoint.rotation;

        shot();

        //GameObject bullet = Instantiate(origin, position, rotation);

        //弾丸を飛ばす
        //Rigidbody rb = bullet.GetComponent<Rigidbody>();
        //rb.AddForce(shotPoint.forward * 50, ForceMode.Impulse);

        //Destroy(bullet, 3f);
    }
    void shot()
    {
        GameObject origin = bulletPrefab;
        Vector3 position = shotPoint.position;
        Quaternion rotation = shotPoint.rotation;

        GameObject bullet = Instantiate(origin, position, rotation);

        //弾丸を飛ばす
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 50, ForceMode.Impulse);

        Destroy(bullet, 3f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag.Equals("Item/Barrire"))
        {

            Material m = barrireRenderer.material;

            barrireActivation = true;

            m.SetInt("_IsActive", 1);

        }

    }
}