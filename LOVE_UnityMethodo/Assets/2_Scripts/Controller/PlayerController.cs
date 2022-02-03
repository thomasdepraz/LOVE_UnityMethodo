using UnityEngine;

namespace Player
{

    public enum PlayerState
    {
        ON_GROUND,
        JUMPING,
        FALLING
    }

    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        public Transform self;
        public Raycaster2D raycaster;
        public Checkpoint currentCheckpoint;

        [Header("Data")]
        public float speed;
        public float jumpForce;
        public float maxJumpDuration;
        public AnimationCurve jumpAccelerationCurve;
        public AnimationCurve fallingAccelerationCurve;

        [HideInInspector] public PlayerState currentPlayerState;
        float inputDirection;
        float currentJumpDuration;
        float fallingDuration;
        bool horizontalHit;


        [Header("Feedbacks")]
        public CheckpointBehaviour checkpointBehaviour;


        public void Start()
        {
            currentPlayerState = PlayerState.ON_GROUND;
        }

        public void Update()
        {
            HorizontalMovement();
            VerticalMovement();

            if (currentPlayerState == PlayerState.ON_GROUND)
            {
                CheckFalling();
                PlaceCheckpoint();
            }
            if (currentPlayerState == PlayerState.FALLING)
            {
                //Check void death
                if (self.transform.position.y <= Setting.voidHeight)
                    Respawn();
            }

        }
        public void ApplyGravity()
        {
            fallingDuration += Time.deltaTime;
            self.position += Vector3.down * Setting.gravityForce * Time.deltaTime * fallingAccelerationCurve.Evaluate(fallingDuration);
        }

        public void HorizontalMovement()
        {
            inputDirection = Input.GetAxisRaw("Horizontal");
            if (inputDirection > 0) horizontalHit = raycaster.ThrowRays(RayDirection.Right);
            else if (inputDirection < 0) horizontalHit = raycaster.ThrowRays(RayDirection.Left);
            else horizontalHit = false;

            if(!horizontalHit)
                self.position += Vector3.right * inputDirection * speed * Time.deltaTime;
        }

        public void VerticalMovement()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(currentPlayerState == PlayerState.ON_GROUND)
                {
                    currentJumpDuration = 0;
                    currentPlayerState = PlayerState.JUMPING;
                }
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                if (currentPlayerState == PlayerState.JUMPING)
                {
                    fallingDuration = 0;
                    currentPlayerState = PlayerState.FALLING;
                }
            }

            if(currentPlayerState == PlayerState.JUMPING)
            {
                currentJumpDuration += Time.deltaTime;

                if (currentJumpDuration >= maxJumpDuration || raycaster.ThrowRays(RayDirection.Up))
                {
                    fallingDuration = 0;
                    currentPlayerState = PlayerState.FALLING;
                } 
                else
                {
                    self.transform.position += Vector3.up * jumpForce * jumpAccelerationCurve.Evaluate(currentJumpDuration/maxJumpDuration) * Time.deltaTime ;
                }
            }
            else if(currentPlayerState == PlayerState.FALLING)
            {
                ApplyGravity();

                //If collision ground change playerstate
                if(raycaster.ThrowRays(RayDirection.Down))
                {
                    currentPlayerState = PlayerState.ON_GROUND; 
                }
            }
        }

        public void CheckFalling()
        {
            if (!raycaster.ThrowRays(RayDirection.Down))
                currentPlayerState = PlayerState.FALLING;
        }

        public void Spawn()
        {
            gameObject.SetActive(true);
            self.position = GameManager.Instance.currentLevel.playerStart.localPosition;
            currentPlayerState = PlayerState.FALLING;
        }

        public void PlaceCheckpoint()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                currentCheckpoint = new Checkpoint(self.position, checkpointBehaviour.gameObject);
            }
        }

        public void Respawn()
        {

            if(currentCheckpoint != null)
            {
                self.position = currentCheckpoint.position;
            }
            else
            {
                self.position = GameManager.Instance.currentLevel.playerStart.localPosition;
            }

            //lower life points if == 0 then loose
        }
    }
}
