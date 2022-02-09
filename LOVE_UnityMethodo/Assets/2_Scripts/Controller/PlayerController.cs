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
        public int maxLifeCount;
        private int _lifeCount;
        public int LifeCount
        {
            get
            {
                return _lifeCount;
            }
            set
            {
                _lifeCount = value;
                GameManager.Instance.uiHandler.UpdateLifeCount(_lifeCount, maxLifeCount);
            }
        }
        public float speed;
        public float jumpForce;
        public float maxJumpDuration;
        public AnimationCurve jumpAccelerationCurve;
        public AnimationCurve fallingAccelerationCurve;

        [HideInInspector] public PlayerState currentPlayerState;

        private bool _isInHelicopterMode;
        public bool isInHelicopterMode 
        {
            get => _isInHelicopterMode;
            set
            {
                _isInHelicopterMode = value;
                if (value)
                {
                    sr.color = Color.red;
                    Setting.gravityForce *= 2;
                }
                else
                {
                    sr.color = Color.white;
                    Setting.gravityForce *= 0.5f;
                }
            }
        }
        float inputDirection;
        float currentJumpDuration;
        float fallingDuration;
        bool horizontalHit;
        bool isInForcedJump;
        private float originJumpForce;


        [Header("Feedbacks")]
        public CheckpointBehaviour checkpointBehaviour;
        public PlayerFX playerFXScript;
        public PlayerSound playerSoundScript;


        [Header("Animation")]
        public Animator playerAnimator;
        public SpriteRenderer sr;

        public int numberOfCheckpoint;
        public int numberOfDeath;

        public void Start()
        {
            LifeCount = maxLifeCount;
            currentPlayerState = PlayerState.ON_GROUND;
            playerAnimator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            playerFXScript = GetComponent<PlayerFX>();
            playerSoundScript = GetComponent<PlayerSound>();
            originJumpForce = jumpForce;
        }

        public void Update()
        {
            if (!GameManager.Instance.isPaused)
            {
                HorizontalMovement();
                VerticalMovement();
            }

            if (currentPlayerState == PlayerState.ON_GROUND)
            {
                PlaceCheckpoint();
            }
            if (currentPlayerState == PlayerState.FALLING)
            {
                //Check void death
                if (self.transform.position.y <= Setting.voidHeight)
                {
                    playerFXScript.PlayerDies();
                    playerSoundScript.PlayerDies();
                    Respawn();
                }
            }

        }
        public void FixedUpdate()
        {
            if (currentPlayerState == PlayerState.ON_GROUND)
            {
                CheckFalling();
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
            if (inputDirection > 0.3)
            {
                horizontalHit = raycaster.ThrowRays(RayDirection.Right);
                sr.flipX = true;

                //anim walk
                playerAnimator.SetBool("isWalking", true);
            }
            else if (inputDirection < -0.3)
            {
                horizontalHit = raycaster.ThrowRays(RayDirection.Left);
                sr.flipX = false;

                //anim walk
                playerAnimator.SetBool("isWalking", true);
            }
            else
            {
                //anim idle
                playerAnimator.SetBool("isWalking", false);
                horizontalHit = false;
            }

            if(!horizontalHit)
            {
                self.position += Vector3.right * inputDirection * speed * Time.deltaTime;
            }
                
        }

        bool raycastUp;
        public void VerticalMovement(bool forceJump = false, float jumpForceFactor = 1)
        {
            if(Input.GetKeyDown(KeyCode.Space) || forceJump)
            {
                if(forceJump)
                {
                   isInForcedJump = true;
                   if(jumpForce == originJumpForce) jumpForce *= jumpForceFactor;
                }

                if(currentPlayerState == PlayerState.ON_GROUND || forceJump || isInHelicopterMode)
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
                raycastUp = raycaster.ThrowRays(RayDirection.Up);

                if (currentJumpDuration >= maxJumpDuration || raycastUp)
                {
                    if (raycastUp && isInHelicopterMode)
                        isInHelicopterMode = false;

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
                numberOfCheckpoint++;
                currentCheckpoint = new Checkpoint(self.position, checkpointBehaviour.gameObject);
                playerFXScript.PlayerSetNewSpawn();
            }
        }

        public void Respawn()
        {
            numberOfDeath++;
            LifeCount--;
            if (isInHelicopterMode) isInHelicopterMode = false;

            if(LifeCount > 0)
            {
                if(currentCheckpoint != null)
                {
                    self.position = currentCheckpoint.position;
                    playerFXScript.PlayerSpawns();
                }
                else
                {
                    self.position = GameManager.Instance.currentLevel.playerStart.localPosition;
                    playerFXScript.PlayerSpawns();
                }
            }
            else
            {
                //Calculate score 
                int score = GameManager.Instance.currentLevelCount * 50 + (numberOfCheckpoint * (-5)) + (numberOfDeath * (-10));

                //Appear score UI
                GameManager.Instance.uiHandler.AppearScoreScreen(score, "You lost !");
            }

            //lower life points if == 0 then loose
        }

        public void OnBecameInvisible()
        {
            if (isInHelicopterMode)
                isInHelicopterMode = false;
        }
    }
}
