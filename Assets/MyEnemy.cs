using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MyEnemy : MonoBehaviour
{
    [SerializeField] private bool isMoving;
    [SerializeField] private Vector2 moveSpeedRange;
    [SerializeField] private Animator anim;

    private float moveSpeed;

    private Transform target;

    private void Start()
    {
        target = MyGameManager.instance.player.transform;
        moveSpeed = Random.Range(moveSpeedRange.x, moveSpeedRange.y);
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (target == null) { return; }

        if (Vector3.Distance(transform.position, target.position) < 3)
        {
            isMoving = false;
            anim.SetBool("Attack", true);
        }
        else
        {
            isMoving = true;
            anim.SetBool("Attack", false);
        }

        if (isMoving)
        {
            var step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.LookAt(target);
        }
    }
}
