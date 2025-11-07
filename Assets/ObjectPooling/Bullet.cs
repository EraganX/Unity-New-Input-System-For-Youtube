using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private void OnEnable()
    {
        StartCoroutine(DestroyObject());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5f);
        //Destroy(this.gameObject);

        gameObject.SetActive(false);
        PoolManager.instance.SetToPool(gameObject);
    }
}
