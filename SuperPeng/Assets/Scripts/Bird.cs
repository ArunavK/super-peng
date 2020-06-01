using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private static Bird instance;

    private const float JUMP_AMOUNT = 80f;
    private Rigidbody2D birdRigidbody2D;

    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;
    private State state;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    public static Bird GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        birdRigidbody2D = GetComponent<Rigidbody2D>();
        instance = this;
        birdRigidbody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    birdRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    state = State.Playing;
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }

                transform.eulerAngles = new Vector3(0, 0, birdRigidbody2D.velocity.y * 0.25f);
                break;
            case State.Dead:
                break;

        }
       
    }

    private void Jump()
    {
        birdRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
        SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        birdRigidbody2D.bodyType = RigidbodyType2D.Static;
        SoundManager.PlaySound(SoundManager.Sound.Lose);
        if (OnDied != null) OnDied(this, EventArgs.Empty);
    }
}
