using System;
using System.Collections;
using UnityEngine;
using UnityRoguelike;
using Random = UnityEngine.Random;
using Rect = UnityEngine.Rect;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    //[RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking = true;
        [SerializeField] private float m_WalkSpeed = 0;
        [SerializeField] private float m_RunSpeed = 0;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten = 0;
        [SerializeField] private float m_JumpSpeed = 0;
        [SerializeField] private float m_StickToGroundForce = 0;
        [SerializeField] private float m_GravityMultiplier=0;
        [SerializeField] private MouseLook m_MouseLook =new MouseLook();
        [SerializeField] private float m_StepInterval =0;
        //[SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        //[SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        //[SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private bool m_Attack;

        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private bool m_Attacking;
        private AudioSource m_AudioSource;

        private Vector3 attackPosition;
        private Vector3 attackRotation;

        private float curSpeed=0f;

        private Actor actorRef = new Actor();
        private Vec oldPos;

        void Awake()
        {
            Debug.Log("Player Awake called.");
        }

        // Use this for initialization
        private void Start()
        {
            Debug.Log("Player Start called.");

            attackPosition = new Vector3(-0.05f, 0.1f, 0.06f);
            attackRotation = new Vector3(4.5f, -20f, -15.5f);

            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
            
            //m_MouseLook = new MouseLook();
			m_MouseLook.Init(transform , m_Camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }

            m_Attack = Input.GetButtonDown("Fire1");
            if (m_Attack)
            {
                PlayAttack();
                m_Attack = false;
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;

            var p = new Vec((int)(transform.position.x + 0.5f), (int)(transform.position.z + 0.5f));
            var f = new Vector3(p.x + 0.5f, 0, p.y + 0.5f) + transform.forward;

            if (oldPos != p)
            {
                GameManagerScript.stage.Player.Position = p;
                GameManagerScript.EndTurn();
                oldPos = p;
            }

        }

        private void PlayLandingSound()
        {
            //m_AudioSource.clip = m_LandSound;
            //m_AudioSource.Play();
            //m_NextStep = m_StepCycle + .5f;
        }

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);

            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo, m_CharacterController.height/2f);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);
            curSpeed = speed;
        }

        private void PlayAttack()
        {
            if (m_Attacking)
                return;

            m_Attacking = true;
            GameManagerScript.EndTurn();
            var weaponSlot = transform.FindChild("WeaponSlot");
            var anim  = weaponSlot.GetComponent<Animator>();
            anim.SetTrigger("Swing");
            StartCoroutine(ResetAttack());
        }

        private IEnumerator ResetAttack()
        {
            yield return new WaitForSeconds(0.2f);
            m_Attacking = false;
        }


        private void PlayJumpSound()
        {
            //m_AudioSource.clip = m_JumpSound;
            //m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            //int n = Random.Range(1, m_FootstepSounds.Length);
            //m_AudioSource.clip = m_FootstepSounds[n];
            //m_AudioSource.PlayOneShot(m_AudioSource.clip);
            //// move picked sound to index 0 so it's not picked next time
            //m_FootstepSounds[n] = m_FootstepSounds[0];
            //m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;

            //if (!m_UseHeadBob)
            //{
            //    return;
            //}

            //if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            //{
            //    m_Camera.transform.localPosition = m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
            //    newCameraPosition = m_Camera.transform.localPosition;
            //    newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            //}
            //else
            //{
            newCameraPosition = m_Camera.transform.localPosition;
            //    newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            //}

            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            //if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            //{
            //    StopAllCoroutines();
            //    StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            //}
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }

        void RegisterWithStage()
        {
            Debug.Log(name + " has registered.");
            actorRef = new Actor();
            GameManagerScript.stage.Player = actorRef;
            GameManagerScript.stage.Player.PropertyChanged += Player_PropertyChanged;
        }

        void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Debug.Log(e.PropertyName+" has changed.");
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter: "+other.name);
            //if (other.gameObject.CompareTag("Pick Up"))
            //{
            //    other.gameObject.SetActive(false);
            //}
        }

        void OnGUI()
        {
            var p = new Vec((int) (transform.position.x+0.5f), (int) (transform.position.z+0.5f));
            //Physics.Raycast(new Vector3(p.x,0,p.y),transform.forward,1.0f)
            var f = new Vector3(p.x+0.5f, 0, p.y+0.5f) + transform.forward;
            GUI.Label(new Rect(10, 10, 200, 40), "Positon: "+p);
            GUI.Label(new Rect(10, 20, 200, 40), "Facing: " + new Vec((int)f.x,(int)f.z));

            GUI.Label(new Rect(10, 30, 200, 40), "Current Turn: " + GameManagerScript.turnCount);

        }
    }
}
