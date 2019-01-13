using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SeekingPlayerNoise))]
[RequireComponent(typeof(SeekingPlayerVision))]
[RequireComponent(typeof(Patrol))]
public class AICharacterBehavior : MonoBehaviour {

    private SeekingPlayerNoise seekingPlayerNoiseBehavior;
    private SeekingPlayerVision seekingPlayerVisionBehavior;
    private Patrol patrolBehavior;
    private bool seeingPlayer = false;
    private bool hearingPlayer = false;
    private bool patroling = true;
    private bool resting = false;

    public float gracePeriodTime;
    private float gracePeriodTimer = 0;

    public float timer = 2;
    public Animator animator;

    private GameController gameController;

    // Use this for initialization
    void Start() {
        seekingPlayerNoiseBehavior = GetComponent<SeekingPlayerNoise>();
        seekingPlayerVisionBehavior = GetComponent<SeekingPlayerVision>();
        patrolBehavior = GetComponent<Patrol>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update() {
        gracePeriodTimer -= Time.deltaTime;
        UpdateAnimator();
    }

    public void AIHeardPlayer(Vector3 SoundPosition)
    {
        Debug.Log("AI " + transform.name + " Heard player");

        // Seeing player supercedes hearing
        if (seeingPlayer)
            return;

        resting = false;
        hearingPlayer = true;
        patroling = false;
        patrolBehavior.StopMoving();
        patrolBehavior.enabled = false;
        seekingPlayerNoiseBehavior.enabled = true;
        seekingPlayerNoiseBehavior.Init(SoundPosition);
        StartCoroutine(BackToPatrol());

    }

    public void AISawPlayer()
    {
        Debug.Log("AI " + transform.name + " Seeing player");
        resting = false;
        hearingPlayer = false;
        patroling = false;
        seeingPlayer = true;

        seekingPlayerNoiseBehavior.enabled = false;
        patrolBehavior.enabled = false;
        seekingPlayerVisionBehavior.enabled = true;
    }

    public void BackToPatroling()
    {
        resting = false;
        hearingPlayer = false;
        seeingPlayer = false;
        patroling = true;

        seekingPlayerNoiseBehavior.enabled = false;
        seekingPlayerVisionBehavior.enabled = false;
        patrolBehavior.enabled = true;
        patrolBehavior.KeepMoving();
    }

    public void Attack()
    {
        Debug.Log("Attack!");
        //gameController.KillPlayer();
        // Kill player will come from animation trigger at end of attack animation
        BackToPatroling();
        gracePeriodTimer = gracePeriodTime;
        animator.SetTrigger("attack");
    }

    public bool IsInGracePeriod()
    {
        return gracePeriodTimer > 0;
    }

    IEnumerator BackToPatrol()
    {
        yield return new WaitForSeconds(timer);
        BackToPatroling();
    }

    void UpdateAnimator()
    {
        animator.SetBool("hunting", hearingPlayer || seeingPlayer);
        animator.SetBool("resting", resting);
    }

    public void SetResting(bool resting)
    {
        if (resting)
        {
            animator.SetTrigger("rest");
        }

        this.resting = resting;
    }
}
