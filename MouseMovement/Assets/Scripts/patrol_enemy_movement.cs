
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class patrol_enemy_movement : MonoBehaviour
{
    Rigidbody2D rb;
    int numsOfPatrolEnemy = 1;

    public int speed = 2;
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

    private Vector2 startPosition;
    private Vector2 endPosition;

    private Vector2 currentPosition;
    private Vector2 nextPosition;
    private int currentIndex = 0;
    private int nextIndex = 1;
    // Patrol behaviour: loop (wrap to start) or ping-pong (reverse at ends)
    public bool loop = true; // when true, wraps from last -> first
    public bool pingPong = false; // when true, reverses direction at ends
    private int direction = 1; // 1 = forward, -1 = backward (used for ping-pong)


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentIndex = 0;
        nextIndex = (currentIndex + 1) % patrolEnemyPositions.Length;
        startPosition = patrolEnemyPositions[currentIndex];
        endPosition = patrolEnemyPositions[nextIndex];
        transform.position = startPosition;

    }

    void FixedUpdate()
    {
        currentPosition = rb.position;
        Vector2 target = patrolEnemyPositions[nextIndex];
        nextPosition = target;

        Vector2 newPos = Vector2.MoveTowards(currentPosition, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // Consider target reached when very close to avoid float equality issues
        if (Vector2.Distance(newPos, target) < 0.05f)
        {
            if (loop)
            {
                Debug.Log("Looping patrol");
                currentIndex = nextIndex;
                nextIndex = (nextIndex + 1) % patrolEnemyPositions.Length;
            }
            else if (pingPong)
            {
                // Advance by direction; reverse when hitting ends
                currentIndex = nextIndex;
                if (currentIndex == patrolEnemyPositions.Length - 1)
                {
                    direction = -1;
                }
                else if (currentIndex == 0)
                {
                    direction = 1;
                }
                nextIndex = currentIndex + direction;
                // clamp just in case
                if (nextIndex < 0) nextIndex = 0;
                if (nextIndex >= patrolEnemyPositions.Length) nextIndex = patrolEnemyPositions.Length - 1;
            }
            else
            {
                // Default behaviour (if neither flag set): behave like loop
                currentIndex = nextIndex;
                nextIndex = (nextIndex + 1) % patrolEnemyPositions.Length;
            }

            // Guard: if nextIndex accidentally equals currentIndex, advance to avoid zero-length moves
            if (nextIndex == currentIndex)
            {
                nextIndex = (currentIndex + 1) % patrolEnemyPositions.Length;
            }

            if(nextIndex == patrolEnemyPositions.Length - 1)
            {
                nextIndex = 0;
            }

            startPosition = patrolEnemyPositions[currentIndex];
            endPosition = patrolEnemyPositions[nextIndex];

            Debug.Log($"Patrol: reached index {currentIndex}. New nextIndex={nextIndex}. loop={loop} pingPong={pingPong} direction={direction}");
        }
    }
}
