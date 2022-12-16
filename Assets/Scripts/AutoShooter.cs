using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoShooter : MonoBehaviour
{

    [SerializeField] float rangeRadius;
    [SerializeField] float fireInterval;
    [SerializeField] Transform closestEnemy;
    [SerializeField] GameObject shotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = null;
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fire()
    {
        while (true) {

            if (closestEnemy)
            {
                
                Vector3 pos = transform.localPosition;
                Vector3 enemyPos = closestEnemy.position;
                var shot = Instantiate(shotPrefab, pos, Quaternion.identity);
                Vector3 enemyDir = new Vector3(enemyPos.x, pos.y, enemyPos.z) - pos;
                enemyDir.Normalize();
                Debug.Log(enemyDir);
                shot.GetComponent<Bullet>().ShootTheShot(enemyDir);
            }
            yield return new WaitForSeconds(fireInterval);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!closestEnemy)
            {
                closestEnemy = other.transform;
                return;
            }

            if (ManhattanDistance(transform.position, closestEnemy.position) > ManhattanDistance(transform.position, other.transform.position))
            {
                closestEnemy = other.transform;
            }
        }

    }

    private float ManhattanDistance(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
}
