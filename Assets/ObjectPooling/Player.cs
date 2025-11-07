using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;

        if (time>0.1f)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //Instantiate(prefab,spawnPoint);


                GameObject bullet = PoolManager.instance.GetPrefab();
                bullet.transform.position = spawnPoint.position;

                time = 0;
            }
        }
    }
}
