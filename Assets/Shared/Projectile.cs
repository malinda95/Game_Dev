using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;

    void Start()
    {
        Destroy(gameObject, timeToLive); 
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit, 5f))
        {
            CheckDestructable(hit.transform);
        }
    }

    void CheckDestructable(Transform other)
    {
        var destructable = other.GetComponent<Destructable>();
        if (destructable == null)
            return;
        destructable.TakeDamage(damage);
    }
}
