using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeAI : MonoBehaviour {

    [SerializeField] Transform target;

    [SerializeField] float maxSpeed;

    [SerializeField] float stoppingDistance;

    [SerializeField] float accelerationRate;

    float timer = 0.1f;

    // Update is called once per frame
    void Update () {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * (maxSpeed * timer)) * Vector2.right;

        timer += Time.deltaTime * accelerationRate;
    }
}
