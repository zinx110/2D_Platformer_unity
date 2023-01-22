using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0f;
    public int damage = 10;
    public LayerMask whatToHit;


    public Transform BulletTrailPrefab;
    public Transform NuzzleFlashPrefab;
    public Transform hitPrefab;

    public float effectSpawnRate = 10;


    float timeToFire = 0f;
    float timeToSpawnEffect = 0f;
    Transform firePoint;



    // Handle Canera shaking
    public float camShakeAmount = 0.05f;
    public float camShakeDuration = 0.1f;
    CameraShake camShake;






    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No 'FirePoint' child found under Pistol.");
        }



    }

    private void Start()
    {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null)
        {
            Debug.LogError("No Camerashake script found on 'GM' object.");

        }
    }


    // Update is called once per frame
    void Update()
    {

        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;

                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100f, whatToHit);



        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
               
                enemy.DamageEnemy(damage);
            }
        }

        if (Time.time > timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {

                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(999, 999, 999);
                Effect(hitPos, hitNormal);
                timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;



                Effect(hitPos, hitNormal);
                timeToSpawnEffect = Time.time + 1 / effectSpawnRate;




            }



        }
    }

    void Effect(Vector3 hitPosition, Vector3 hitNormal)
    {

        Transform trail = Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPosition);
        }

        Destroy(trail.gameObject, 0.04f);

        if (hitNormal != new Vector3(999, 999, 999))
        {
            Transform hitParticle = Instantiate(hitPrefab, hitPosition, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(hitParticle.gameObject, 1f);
        }

        Transform clone = Instantiate(NuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 1f);
        clone.localScale = new Vector3(size, size, 0f);
        Destroy(clone.gameObject, 0.02f);


        //  Shake the camera
        camShake.ShakeCamera(camShakeAmount, camShakeDuration);
    }




}
