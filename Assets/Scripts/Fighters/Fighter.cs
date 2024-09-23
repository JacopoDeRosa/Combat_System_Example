using System;
using HitDetection;
using Inputs;
using Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Fighters
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private FighterData _fighterData;
        [SerializeField] private Animator _animator;
        [SerializeField] private EFighterSide _fighterSide;
        [SerializeField] private SocketManager _socketManager;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private HurtboxController _hurtboxController;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private bool _possessed;
        [SerializeField] private float _xDepth;

        private InputBuffer _inputBuffer;

        private Vector2 _movementInput;
        
        private float _health;

        private void Awake()
        {
            _inputBuffer = new InputBuffer();
            _inputBuffer.OnActionEnqueued += OnInputActionEnqueued;
            _inputBuffer.OnActionDequeued += OnInputActionDequeued;
        }
        
        private void Start()
        {
            LoadFighterData();
            
            if (_possessed)
            {
                AttachToInputSystem();
            }
            
        }
        
        private void OnInputActionEnqueued(string controlName)
        {
            _animator.SetBool(controlName, true);
        }
        
        private void OnInputActionDequeued(string controlName)
        {
            _animator.SetBool(controlName, false);
        }

        private void LoadFighterData()
        {
           _animator.runtimeAnimatorController = _fighterData.Animator;
           _health = _fighterData.Health;
        }
        
        public void InstantiateHitbox(Object hitboxDataObject)
        {
           HitboxData hitboxData = hitboxDataObject as HitboxData;
           
           Debug.Log("Spawn hitbox at socket: " + hitboxData.AttachmentSocket + ", Hitbox attachment: " + hitboxData.AttachToSocket);
           
           HitboxController hitboxController = Instantiate(hitboxData.HitboxPrefab);
           
           hitboxController.SetLifetime(hitboxData.Duration);
           
           hitboxController.SetTransformsToIgnore(_hurtboxController.HurtboxId);
           
           Transform socket = _socketManager.GetSocket(hitboxData.AttachmentSocket);
           
           if(socket == null)
           {
               Debug.LogError("Socket not found: " + hitboxData.AttachmentSocket);
               hitboxController.transform.position = transform.position + hitboxData.Offset;
           }
           else if(hitboxData.AttachToSocket)
           {
               hitboxController.transform.SetParent(socket, false);
               hitboxController.transform.localPosition = hitboxData.Offset;
               
           }
           else
           {
                hitboxController.transform.position = socket.position + hitboxData.Offset;
           }
        }

        private void Move()
        {
            Vector3 move = new Vector3(0, 0, _movementInput.x) * (_movementSpeed);
            _characterController.Move(transform.TransformDirection(move * Time.deltaTime));
            _animator.SetFloat("Speed", move.z);
            transform.position = new Vector3(_xDepth, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            Move();
        }

        private void FixedUpdate()
        {
            _inputBuffer.TickFrame();
        }

        private void AttachToInputSystem()
        {
            PlayerInput input = FindObjectOfType<PlayerInput>();
            input.actions["Move"].performed += OnMovePerformed;
            input.actions["LightAttack"].started += OnLightAttackStarted;
            input.actions["HeavyAttack"].started += OnHeavyAttackStarted;
            input.actions["Jump"].started += OnJumpStarted;
        }
        
        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            Vector2 move = context.ReadValue<Vector2>();
            _movementInput = move;
        }
        
        private void OnLightAttackStarted(InputAction.CallbackContext context)
        {
            _inputBuffer.AddAction(new InputBufferAction("LightAttack"));
        }  
        
        private void OnHeavyAttackStarted(InputAction.CallbackContext context)
        {
            _inputBuffer.AddAction(new InputBufferAction("HeavyAttack"));
        }
        
        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            _inputBuffer.AddAction(new InputBufferAction("Jump"));
        }
    }
}
