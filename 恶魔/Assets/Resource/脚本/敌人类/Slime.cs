using System.Collections;
using Resource.脚本.主角类;
using Resource.脚本.管理类;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resource.脚本.敌人类
{
    public class Slime : MonoBehaviour
    {
        private enum SlimeState
        {
            Move,   // 移动
            Attack, // 攻击
            Hurt,   // 受伤
            Dead    // 死亡
        }

        [SerializeField] private float speed = 2f;           // 移动速度
        [SerializeField] private float attackRange = 1.5f;   // 攻击范围
        [SerializeField] private float attackDashSpeed = 4f; // 冲刺速度
        [SerializeField] private float hurtBackSpeed = 2f;
        [SerializeField] private float deadBackSpeed = 6f;
        [FormerlySerializedAs("_currentState")] [SerializeField] private SlimeState currentState = SlimeState.Move;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;
        private Animator _anim;
        private PlayerControl _player;
        private bool _isAttacking;
        private EnemyHurt _hurt;
        private int _slimeHeal = 3;
        private bool _isDead;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
            _hurt = GetComponent<EnemyHurt>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                _slimeHeal -= 1;
                if (_slimeHeal > 0)
                {
                    currentState = SlimeState.Hurt;
                    _hurt?.TakeDamage();
                }
                else
                {
                    currentState = SlimeState.Dead;
                }
            }
        }

        private void Update()
        {
            if (_player == null) return;

            if (currentState != SlimeState.Hurt && currentState != SlimeState.Dead)
            {
                if (!_isAttacking)
                {
                    currentState = Vector2.Distance(transform.position, _player.transform.position) < attackRange ? SlimeState.Attack : SlimeState.Move;
                }
            }

            switch (currentState)
            {
                case SlimeState.Move:
                    MoveToPlayer();
                    break;
                case SlimeState.Attack:
                    if (!_isAttacking)
                    {
                        OnAttack();
                    }
                    break;
                case SlimeState.Hurt:
                    OnSlimeHurt();
                    break;
                case SlimeState.Dead:
                    SlimeDead();
                    break;
            }
        }

        private void SlimeDead()
        {
            if (_isDead) return;
            _isDead = true;
            _anim.Play("Dead");
            _rb.velocity = Vector2.zero;
            SlimeBack(deadBackSpeed);
            //AudioController.Instance.PlayDeathAudio(); // 需实现
            StartCoroutine(SlimeDestroy());
        }

        private IEnumerator SlimeDestroy()
        {
            yield return new WaitForSeconds(1f); // 调整为死亡动画时长
            Destroy(gameObject);
        }

        private void OnSlimeHurt()
        {
            SlimeBack(hurtBackSpeed);
            print("收到攻击后退！");
            StartCoroutine(HurtRecovery());
        }

        private void SlimeBack(float backSpeed)
        {
            var direction = (transform.position - _player.transform.position).normalized;
            _rb.velocity = direction * backSpeed;
        }
        private IEnumerator HurtRecovery()
        {
            yield return new WaitForSeconds(0.2f);
            _rb.velocity = Vector2.zero;
            _isAttacking = false;
            currentState = SlimeState.Move;
        }

        private void MoveToPlayer()
        {
            _anim.Play("Move");
            FlipSprite(_player.transform.position);
            _rb.velocity = (_player.transform.position - transform.position).normalized * speed;
        }

        private void OnAttack()
        {
            _isAttacking = true;
            _anim.Play("Attack");
        }

        public void DashToPlayer()
        {
            if (Vector2.Distance(transform.position, _player.transform.position) < attackRange)
            {
                var targetPos = (_player.transform.position - transform.position).normalized * attackDashSpeed;
                print("冲刺");
                FlipSprite(targetPos);
                _rb.velocity = targetPos;
                AudioController.Instance.PlayAttackAudio(AudioController.Instance.slimeAttackAudio);
            }
            else
            {
                print("玩家已离开，取消冲刺");
                _rb.velocity = Vector2.zero;
                _isAttacking = false;
                _anim.Play("Move");
            }
        }

        private void FlipSprite(Vector2 direction)
        {
            if (_spriteRenderer == null) return;
            _spriteRenderer.flipX = direction.x < 0;
        }
    }
}