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
        bool isInForcedJump;
        private float originJumpForce;


        [Header("Feedbacks")]
        public CheckpointBehaviour checkpointBehaviour;


        [Header("Animation")]
        public Animator playerAnimator;
        public SpriteRenderer sr;

        public void Start()
        {
            currentPlayerState = PlayerState.ON_GROUND;
            playerAnimator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            originJumpForce = jumpForce;
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
            if (inputDirection > 0)
            {
                horizontalHit = raycaster.ThrowRays(RayDirection.Right);
                sr.flipX = true;
            }
            else if (inputDirection < 0)
            {
                horizontalHit = raycaster.ThrowRays(RayDirection.Left);
                sr.flipX = false;
            }
            else horizontalHit = false;

            if(!horizontalHit)
            {
                self.position += Vector3.right * inputDirection * speed * Time.deltaTime;
                //anim walk
                playerAnimator.SetBool("isWalking", true);
            }
            else
            {
                //anim idle
                playerAnimator.SetBool("isWalking", false);
            }
                
        }

        public void VerticalMovement(bool forceJump = false, float jumpForceFactor = 1)
        {
            if(Input.GetKeyDown(KeyCode.Space) || forceJump)
            {
                if(forceJump)
                {
                   isInForcedJump = true;
                   if(jumpForce == originJumpForce) jumpForce *= jumpForceFactor;
                }

                if(currentPlayerState == PlayerState.ON_GROUND || forceJump)
                {
                    currentJumpDuration = 0;
                    currentPlayerState = PlayerState.JUMPING;
                }
            }
            if(Input.GetKeyUp(KeyCode.Space) && !isInForcedJump)
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
                    self.transform.position += Vector3.up * jumpForce * jumpForceFactor * jumpAccelerationCurve.Evaluate(currentJumpDuration/maxJumpDuration) * Time.deltaTime ;
                }
            }
            else if(currentPlayerState == PlayerState.FALLING)
            {
                ApplyGravity();

                //If collision ground change playerstate
                if (raycaster.ThrowRays(RayDirection.Down))
                {
                    currentPlayerState = PlayerState.ON_GROUND;

                    if (isInForcedJump)
                    {
                        isInForcedJump = false;
                        jumpForce = originJumpForce;
                    }
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
