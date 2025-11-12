using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 20f;
    public float turnSpeed = 30f;
    private Vector2 moveInput;
    private Vector3 move;

    [Header("Fire")]
    public Transform firePointPosition;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public ParticleSystem smokeParticle;

    public LayerMask groundLayer;
    public Transform aimObTransform;
    public Transform towerTransform;
    public float towerRotateSpeed = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        smokeParticle.Stop();
    }

    private void Update()
    {
        MoveInputs();
        AimToTarget();
        TurretRotationControl();
        TankFire();
    }

    private void FixedUpdate()
    {
        move = moveInput.y * Time.fixedDeltaTime * moveSpeed * transform.forward;
        rb.linearVelocity = move;
    }

    private void TankFire()
    {
        if (PlayerInputManager.Instance.GetFireInput())
        {
            GameObject bullet = Instantiate(bulletPrefab, firePointPosition.position, firePointPosition.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = firePointPosition.transform.forward * bulletSpeed;
            Destroy(bullet, 10f);
            smokeParticle.Play();
        }
    }

    private void TurretRotationControl()
    {
        Vector3 direction = aimObTransform.position - towerTransform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        towerTransform.transform.rotation = Quaternion.RotateTowards(towerTransform.rotation, targetRotation, towerRotateSpeed);
    }

    private void MoveInputs()
    {
        moveInput = PlayerInputManager.Instance.GetMoveInput();

        if (moveInput.y < 0)
        {
            transform.Rotate(Vector3.up * -moveInput.x * Time.deltaTime * turnSpeed);
        }
        else
        {
            transform.Rotate(Vector3.up * moveInput.x * Time.deltaTime * turnSpeed);
        }
    }

    private void AimToTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(PlayerInputManager.Instance.GetMousePosition());
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundLayer))
        {
            Vector3 lookTarget = hitInfo.point;
            lookTarget.y = aimObTransform.position.y;
            aimObTransform.position= lookTarget;
        }

    }
}
