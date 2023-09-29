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
    [SerializeField] bool phone = false;
    [SerializeField] Camera cam;
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

    private Rigidbody2D rb;
    [SerializeField] float moveSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (phone)
        {
            if (Input.touchCount > 0)
            {
                Vector3 touchPosition;
                Vector3 direction;
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
        else
        {
            Vector3 mousePosition;
            //Vector3 direction;

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            //direction = (mousePosition - transform.position);
            //rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, Time.deltaTime * moveSpeed);
        }

        //transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

        /*
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == shipCollider)
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * moveSpeed);
            }
        }
        */

        if (reload == false)
        {
            Shoot();
            reload = true;
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
