using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject spawnPrefab; //スポーンするオブジェクトのプレハブ

    public float spawnInterval = 2f;//スポーンの間隔
    public float spawnTimer = 0f;//スポーンのタイマー
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0f;
        }
    }

        private void SpawnObject()
      {
        //Player player = GameObject.FindAnyObjectByType<Player>();
        NewMonoBehaviourScript player =
        GameObject.FindAnyObjectByType<NewMonoBehaviourScript>();
        float PlayerZ = player.transform.position.z;

         Vector3 randomPos = Vector3.zero;
         randomPos.x =Random.Range(-8,8);
         randomPos.z = PlayerZ + 100;
         Instantiate(spawnPrefab, randomPos, transform.rotation);
     }
}
       
