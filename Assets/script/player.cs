using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("** Shot Settings **")]
    public Transform shotPoint;  //撃ちだし座標
    public GameObject bulletPrefab;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shot();
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
    }
    //PlayerInputから[]アクションを呼び出すメソッド
    public void OnAttack(InputValue value)
    {
        Debug.Log($"攻撃アクション[{value.Get<float>()}]");
        GameObject origin = bulletPrefab;
        Vector3 position = shotPoint.position;
        Quaternion rotation = shotPoint.rotation;

        GameObject bullet = Instantiate(origin, position, rotation);

        //弾丸を飛ばす
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotPoint.forward * 50, ForceMode.Impulse);

        Destroy(bullet, 3f);
    }
    void shot()
    {
        GameObject origin = bulletPrefab;
        Vector3 position = shotPoint.position;
        Quaternion rotation = shotPoint.rotation;

        GameObject bullet = Instantiate(origin, position, rotation);

        //弾丸を飛ばす
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotPoint.forward * 50, ForceMode.Impulse);

        Destroy(bullet, 3f);
    }
}
