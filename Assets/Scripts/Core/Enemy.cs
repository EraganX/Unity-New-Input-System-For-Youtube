using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Material damagedMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.GetComponent<Renderer>().material = damagedMaterial;
            Destroy(this.gameObject,20f);
        }
    }
}
