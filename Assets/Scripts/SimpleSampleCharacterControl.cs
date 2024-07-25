using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

namespace Supercyan.FreeSample
{
    public class SimpleSampleCharacterControl : MonoBehaviour
    {

        [SerializeField]private float m_moveSpeed = 10;
        [SerializeField] private float m_turnSpeed = 200;
        [SerializeField]private float m_jumpForce = 10;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private VariableJoystick _variableJoystick;
        [SerializeField] private Button _buttonJump;

        private bool _isDesktop;

        private Animator m_animator = null;
        [SerializeField] private Rigidbody m_rigidBody = null;

        private float m_currentV = 0;
        private float m_currentH = 0;

        private readonly float m_interpolation = 10;
        private readonly float m_walkScale = 0.33f;
        private Vector3 m_currentDirection = Vector3.zero;

        private float m_jumpTimeStamp = 0;
        private float m_minJumpInterval = 0.25f;
        private bool m_jumpInput = false;

        private bool m_isGrounded = true;

        private List<Collider> m_collisions = new List<Collider>();

        public float CurrentVelocity => m_currentV;

        private void Awake()
        {
            if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }

            _isDesktop = YandexGame.EnvironmentData.isDesktop;

            if (_isDesktop)
            {
                _variableJoystick.gameObject.SetActive(false);
                _buttonJump.gameObject.SetActive(false);
            }
            else
            {
                if (_buttonJump != null)
                {
                    _buttonJump.onClick.AddListener(OnJumpButtonPressed);
                }
            }
        }

        public void Init(Animator animator)
        {
            m_animator = animator;
            m_isGrounded = false;
        }

        public void Upgrade(float speed, float forceJump)
        {
            m_moveSpeed = speed;
            m_jumpForce = forceJump;
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    ContactPoint[] contactPoints = collision.contacts;
        //    for (int i = 0; i < contactPoints.Length; i++)
        //    {
        //        if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.1f)
        //        {
        //            if (!m_collisions.Contains(collision.collider))
        //            {
        //                m_collisions.Add(collision.collider);
        //            }
        //            m_isGrounded = true;
        //        }
        //    }
        //}

        //private void OnCollisionStay(Collision collision)
        //{
        //    ContactPoint[] contactPoints = collision.contacts;
        //    bool validSurfaceNormal = false;
        //    for (int i = 0; i < contactPoints.Length; i++)
        //    {
        //        if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.1f)
        //        {
        //            validSurfaceNormal = true; break;
        //        }
        //    }

        //    if (validSurfaceNormal)
        //    {
        //        m_isGrounded = true;
        //        if (!m_collisions.Contains(collision.collider))
        //        {
        //            m_collisions.Add(collision.collider);
        //        }
        //    }
        //    else
        //    {
        //        if (m_collisions.Contains(collision.collider))
        //        {
        //            m_collisions.Remove(collision.collider);
        //        }
        //        if (m_collisions.Count == 0) { m_isGrounded = false; }
        //    }
        //}

        //private void OnCollisionExit(Collision collision)
        //{
        //    if (m_collisions.Contains(collision.collider))
        //    {
        //        m_collisions.Remove(collision.collider);
        //    }
        //    if (m_collisions.Count == 0) { m_isGrounded = false; }
        //}

        private void Update()
        {
            if (_isDesktop)
            {
                if (!m_jumpInput && Input.GetKey(KeyCode.Space))
                {
                    m_jumpInput = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (m_animator != null)
                m_animator.SetBool("Grounded", m_isGrounded);
            m_isGrounded = ReyCastDown(0.2f);

            DirectUpdate();
            m_jumpInput = false;
        }

        public bool ReyCastDown(float distanse)
        {
            Debug.DrawLine(transform.position,transform.position + -Vector3.up * distanse, Color.red, 2);
            bool grounds = Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hitInfo, distanse, _mask);
            Debug.Log(hitInfo.collider);
            return grounds;
        }

        private void DirectUpdate()
        {
            // float v = Input.GetAxis("Vertical");
            // float h = Input.GetAxis("Horizontal");
            
            float v, h;
            
            if (_isDesktop)
            {
                v = Input.GetAxis("Vertical");
                h = Input.GetAxis("Horizontal");
            }
            else
            {
                v = _variableJoystick.Vertical;
                h = _variableJoystick.Horizontal;
            }

            Transform camera = Camera.main.transform;

            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    v *= m_walkScale;
            //    h *= m_walkScale;
            //}

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                m_rigidBody.rotation = Quaternion.LookRotation(m_currentDirection);
                m_rigidBody.position += m_currentDirection * m_moveSpeed * Time.deltaTime;
                if (m_animator != null)
                    m_animator.SetFloat("MoveSpeed", direction.magnitude);
            }
            
            if (m_jumpInput)
            {
                PerformJump();
            }
        }
        
        private void OnJumpButtonPressed()
        {
            m_jumpInput = true;
        }
        
        private void PerformJump()
        {
            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

            if (jumpCooldownOver && m_isGrounded)
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                m_jumpInput = false;
            }
        }
    }
}
