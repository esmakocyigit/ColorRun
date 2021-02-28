using UnityEngine.EventSystems;
using UnityEngine;
using System;
using Animancer;
public class JoystickController : MonoBehaviour
{
    public float speed;
    public FixedJoystick veriableJoystick;
    public Rigidbody rb;

    [SerializeField]
    private ParticleSystem particleObject;

    Animator animator;
    bool isFinished;
    bool isSmokeParticleStarted = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        CountdownTimer.OnCountDownFinished += OnFinished;
        GameUIController.OnGameStarted += GameUIController_OnGameStarted;
    }

    private void GameUIController_OnGameStarted()
    {
        enabled = true;
    }

    private void OnFinished()
    {
        animator.SetBool("Running", false);
        particleObject.Stop();
        speed = 0;
        isFinished = true;
    }

    Quaternion targetRotation;

    void FixedUpdate()
    {
        if (!isFinished)
        {
            Run();
            Rotate();
        }
    }

    void Run()
    {
        if (veriableJoystick.Vertical != 0 || veriableJoystick.Horizontal != 0)
        {
            Vector3 direction = Vector3.forward * veriableJoystick.Vertical + Vector3.right * veriableJoystick.Horizontal;
            rb.velocity = direction * speed * Time.fixedDeltaTime;
            animator.SetBool("Running", true);

            if (!isSmokeParticleStarted)
            {
                isSmokeParticleStarted = true;
                particleObject.Play();
            }
        }
        else
        {
            animator.SetBool("Running", false);
            isSmokeParticleStarted = false;
            particleObject.Stop();
        }
    }

    void Rotate()
    {
        var input = new Vector3(veriableJoystick.Horizontal, 0, veriableJoystick.Vertical);

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(-input);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * -Time.deltaTime);
    }

}