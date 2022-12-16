using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float followSpeed;
    [SerializeField] float health;
    [SerializeField] int expAward;
    [SerializeField] GameObject expOrb;
    public int scaleX = -1;
    private bool flipping = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (flipping)
        {
            if (transform.localScale.x != scaleX)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(scaleX, 1, 1), Time.deltaTime * 4f);
            } else
            {
                flipping = false;
            }
        }

        Vector3 dir = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.localPosition;
        var dirX = -Mathf.Sign(dir.x);
        if (dirX != 0 && dirX != scaleX)
            //FlipScale();
            scaleX = scaleX * -1;
            flipping = true;

        dir.Normalize();
        transform.position += dir * followSpeed * Time.deltaTime;
    }

    private void FlipScale()
    {
        Debug.Log(scaleX);
        scaleX *= -1;
        transform.localScale = new Vector3(scaleX, 1, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            this.health -= 10;
            if (this.health <= 0)
            {
                var orb = Instantiate(expOrb, transform.position, Quaternion.identity);
                orb.GetComponent<ExpOrb>().expLevel = expAward;
                Destroy(this.gameObject);
            }
        }
    }
}
