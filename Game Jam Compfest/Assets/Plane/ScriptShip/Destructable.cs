using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    //public GameObject explosion;

    bool canBeDestroyed = false;
    public int scoreValue = 100;
    public bool isBoss = false; // Tambahkan variabel isBoss
    public int hitPoints = 1; // Tambahkan variabel hitPoints, default 1 untuk objek biasa

    // Start is called before the first frame update
    void Start()
    {
        Level.instance.AddDestructable();
        if (isBoss)
        {
            hitPoints = 100; // Set hit points untuk bos
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -27)
        {
            DestroyDestructable();
        }

        if (transform.position.y < 14.5f && !canBeDestroyed)
        {
            canBeDestroyed = true;
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns)
            {
                gun.isActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.isEnemy)
        {
            Destroy(bullet.gameObject);
            hitPoints--; // Kurangi hit points setiap terkena peluru

            if (hitPoints <= 0)
            {
                Level.instance.AddScore(scoreValue);
                DestroyDestructable();
            }
        }
    }

    void DestroyDestructable()
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);
        Level.instance.RemoveDestructable();
        Destroy(gameObject);
    }
}
