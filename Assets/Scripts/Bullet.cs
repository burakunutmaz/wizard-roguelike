using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool moving = false;
    public Vector3 direction;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void ShootTheShot(Vector3 direction)
    {
        this.direction = direction;
        moving = true;
    }

}
