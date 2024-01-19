using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MyEnemy : MonoBehaviour
{
    [SerializeField] private bool isMoving;
    [SerializeField] private Vector2 moveSpeedRange;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip deadSfx;

    private AudioSource audioSource;

    private float moveSpeed;

    private Transform target;

    private void Start()
    {
        moveSpeed = Random.Range(moveSpeedRange.x, moveSpeedRange.y);
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(FindTarget), 1);
    }

    private void FindTarget()
    {
        target = MyGameManager.instance.player.transform;
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

    public void OnDead()
    {
        audioSource.PlayOneShot(deadSfx);
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Destroy(gameObject, 1);
    }
}
