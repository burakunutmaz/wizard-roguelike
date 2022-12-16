using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] SimpleTouchController joystick;
    [SerializeField] GameManager gm;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] float speedMultiplier = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVel = new Vector3(joystick.GetTouchPosition.x, 0, joystick.GetTouchPosition.y) * speedMultiplier;
        rb.velocity = Vector3.zero + moveVel.normalized * speedMultiplier;

        if (joystick.GetTouchPosition.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (joystick.GetTouchPosition.x > 0)
            transform.localScale = new Vector3(1, 1, 1);

        if (moveVel.magnitude > 0.2f )
            animator.SetBool(name: "walking", value: true);
        else
            animator.SetBool(name: "walking", value: false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Exp")
        {
            ExpOrb orb1 = collision.gameObject.GetComponent<ExpOrb>();
            Debug.Log("touched exp" + orb1.expLevel);
            gm.gainExp(orb1.expLevel * 10);
            Destroy(collision.gameObject);

        }
    }
}
