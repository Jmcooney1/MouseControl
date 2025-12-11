using UnityEngine;
using UnityEngine.SceneManagement;

public class patrol_enemy_movement : MonoBehaviour
{
    Rigidbody2D rb;

    ElvisHandler elvisHandler;
    public int speed = 5;

    private Vector2[] patrolEnemyPositions = new Vector2[8]
    {
        new Vector2(-8f, 2.5f),
        new Vector2(-8f, -2.5f),
        new Vector2(-3f, -2.5f),
        new Vector2(-3,-1.5f),
        new Vector2(2f, -1.5f),
        new Vector2(2f, 1f),
        new Vector2(-3f, 2f),
        new Vector2(-3f, 2.5f)
    };

    private Vector2[] instrumentPatrol = new Vector2[8]
    {
        new Vector2(-8f,-1.5f),
        new Vector2(-8f,-1.5f),
        new Vector2(5.5f, 0f),
        new Vector2(5.5f, -3.5f),
        new Vector2(1.5f, -3.5f),
        new Vector2(5.5f, -3.5f),
        new Vector2(5.5f, 0f),
        new Vector2(-8f,-1.5f)
    };
    private Vector2 startPosition;
    private Vector2 endPosition;

    private Vector2 currentPosition;
    private Vector2 nextPosition;
    public int currentIndex = 0;
    private int nextIndex = 1;
    // Patrol behaviour: loop (wrap to start) or ping-pong (reverse at ends)
    public bool loop = true; // when true, wraps from last -> first
    public bool heardInstrument = false;

    public bool reverse = false; // when true, reverses direction at ends
    private int direction = 1; // 1 = forward, -1 = backward (used for ping-pong)

    public AudioSource audioSource;
    AudioClip alertClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentIndex = 0;
        nextIndex = (currentIndex + 1) % patrolEnemyPositions.Length;
        startPosition = patrolEnemyPositions[currentIndex];
        endPosition = patrolEnemyPositions[nextIndex];
        transform.position = startPosition;
        audioSource = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        Vector2 target;
        if (!heardInstrument)
        {
            //Debug.Log("Continuing normal patrol");
            target = patrolEnemyPositions[nextIndex];
            loop = true;
        }
        else
        {
            //Debug.Log("Following instrument patrol");
            target = instrumentPatrol[nextIndex % instrumentPatrol.Length];
            loop = false;
        }

        currentPosition = rb.position;
        nextPosition = target;

        Vector2 newPos = Vector2.MoveTowards(currentPosition, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        //Debug.Log($"Patrol Enemy moving from {currentPosition} to {target} at speed {speed}");
        if (Vector2.Distance(newPos, target) < 0.05f)
        {
            if (loop)
            {
                //Debug.Log("Looping patrol");
                currentIndex = nextIndex;
                nextIndex = (nextIndex + 1) % patrolEnemyPositions.Length;
            }

            if (heardInstrument)
            {
                //Debug.Log("Heard instrument, moving to instrument patrol");
                target = instrumentPatrol[nextIndex % instrumentPatrol.Length];
                nextIndex = (nextIndex + 1) % instrumentPatrol.Length;
                nextPosition = target;

                if (target == instrumentPatrol[0])
                {
                    //Debug.Log("Finished instrument patrol, resuming normal patrol");
                    heardInstrument = false;
                    loop = true;
                }
            }
        }

        // Consider target reached when very close to avoid float equality issues
        
        //     else if (pingPong)
        //     {
                 // Advance by direction; reverse when hitting ends
        //         currentIndex = nextIndex;
        //         if (currentIndex == patrolEnemyPositions.Length - 1)
        //         {
        //             direction = -1;
        //         }
        //         else if (currentIndex == 0)
        //         {
        //             direction = 1;
        //         }
        //         nextIndex = currentIndex + direction;
        //          // clamp just in case
        //         if (nextIndex < 0) nextIndex = 0;
        //         if (nextIndex >= patrolEnemyPositions.Length) nextIndex = patrolEnemyPositions.Length - 1;
        //     }
        //     else
        //     {
        // //         // Default behaviour (if neither flag set): behave like loop
        //         currentIndex = nextIndex;
        //         nextIndex = (nextIndex + 1) % patrolEnemyPositions.Length;
        //     }

             // Guard: if nextIndex accidentally equals currentIndex, advance to avoid zero-length moves
            if (nextIndex == currentIndex)
            {
                //Debug.Log("Adjusting nextIndex to avoid zero-length move");
                nextIndex = (currentIndex + 1) % patrolEnemyPositions.Length;
            }

            if(nextIndex == patrolEnemyPositions.Length - 1)
            {
                //Debug.Log("Resetting patrol to start");
                nextIndex = 0;
            }

            startPosition = new Vector2(transform.position.x, transform.position.y);
            endPosition = patrolEnemyPositions[nextIndex];

            //Debug.Log($"Patrol: reached index {currentIndex}. New nextIndex={nextIndex}. loop={loop} pingPong={pingPong} direction={direction}");
        }
        //patrolMovement();
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Patrol Enemy collided with Player");
            audioSource.PlayOneShot(alertClip, 1.0f);
            Reset();
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
