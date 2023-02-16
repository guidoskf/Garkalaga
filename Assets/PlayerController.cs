using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject firePoint;
    private bool reload = false;
    [SerializeField] float reloading = 0.2f;
    [SerializeField] float bulletForce = 25f;
    [SerializeField] GameObject bulletPrefab;
    /*
    private Touch touch;
    [SerializeField] float speedModifier = 0.01f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector2(transform.position.x + touch.deltaPosition.x * speedModifier, transform.position.y + touch.deltaPosition.y * speedModifier);
            }
        }
    }
    */
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    [SerializeField] float moveSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            if(reload == false)
            {
                Shoot();
                reload = true;
            }

            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed * Time.deltaTime;

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, firePoint.transform.position.z), firePoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * bulletForce, ForceMode2D.Impulse);
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloading);

        reload = false;
    }
}
