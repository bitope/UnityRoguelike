using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRoguelike;
using Random = UnityEngine.Random;
using Rect = UnityEngine.Rect;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
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

        public List<GameObject> pointerList;

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

            pointerList = new List<GameObject>();

            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
            
			m_MouseLook.Init(transform , m_Camera.transform);

            EquipWeapon(null);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!GameManagerScript.MouseLook)
                return;

            RotateView();

            if (Input.GetKeyDown(KeyCode.E))
            {
                List<int> ignoreList = new List<int>();
                foreach (var o in pointerList)
                {
                    Debug.Log("Activate: "+o.name);
                    o.transform.SendMessageUpwards("Activate", SendMessageOptions.DontRequireReceiver);

                    if (o.tag == "Item" && !ignoreList.Contains(o.GetInstanceID()))
                    {
                        if (GameManagerScript.stage.Player.PickupItem(o))
                        {
                            Debug.Log("Got item "+o.name);
                            ignoreList.Add(o.GetInstanceID());
                            Destroy(o);
                        }
                    }
                }
            }

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
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            newCameraPosition = m_Camera.transform.localPosition;
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            bool waswalking = m_IsWalking;
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);

            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);

            var all = Physics.RaycastAll(m_Camera.transform.position, m_Camera.transform.forward, 1.0f);

            var c = GameObject.Find("Canvas");
            var x = c.GetComponent<ProximityItemManager>();
            var list = all.Select(i => i.collider.gameObject).ToList();
            x.UpdatePointerList(list);
            pointerList = list;
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
            if (e.PropertyName == "Inventory")
            {
                var weapon = actorRef.GetInventory(EquipmentSlot.RightHand);
                Debug.Log("Weapon "+weapon);

                //if (weapon!=null)
                    EquipWeapon(weapon);
            }
        }

        void OnTriggerEnter(Collider other)
        {

        }

        void OnTriggerExit(Collider other)
        {
        }

        void OnGUI()
        {
            var p = new Vec((int) (transform.position.x+0.5f), (int) (transform.position.z+0.5f));
            var f = new Vector3(p.x+0.5f, 0, p.y+0.5f) + transform.forward;
            GUI.Label(new Rect(10, 10, 200, 40), "Positon: "+p);
            GUI.Label(new Rect(10, 20, 200, 40), "Facing: " + new Vec((int)f.x,(int)f.z));
            GUI.Label(new Rect(10, 30, 200, 40), "Current Turn: " + GameManagerScript.turnCount);
        }

        public void EquipWeapon(Item item)
        {
            var gfx = transform.FindChild("WeaponSlot").FindChild("Quad");
            var ren = gfx.GetComponent<MeshRenderer>();

            if (item == null || item.ItemIcon==null)
            {
                ren.enabled = false;
                return;
            }

            ren.material.mainTexture = item.ItemIcon.texture;
            ren.enabled = true;
        }
    }
}
