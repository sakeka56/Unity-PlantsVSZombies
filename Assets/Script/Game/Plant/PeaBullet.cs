using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float Attack = 20;
    public float speed;

    public GameObject HitPre;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.GetComponent<Zombie>().Hp -= Attack;
            GameObject.Instantiate(HitPre,transform.position,Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
