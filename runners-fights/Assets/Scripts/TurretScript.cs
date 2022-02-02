using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject player;
    public GameObject BulletPreFab;
    public Transform bulletPosicion;
    public AudioClip hurtSound;

    private float LastShoot;
    private int Health = 3;

    private void Update()
    {
        if (player == null) return;
        float distanceY = player.transform.position.y - transform.position.y;
        if (distanceY < 2.0f && distanceY > -2.0f) {

            Vector3 direction = player.transform.position - transform.position;
            if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);

            if (distanceX < 5.0f && Time.time > LastShoot + 0.25f)
            {
                Shoot();
                LastShoot = Time.time;
            }
        }
    }
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPreFab, bulletPosicion.position + direction * 0.2f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
  
    public void Hit()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(hurtSound);
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}

   

