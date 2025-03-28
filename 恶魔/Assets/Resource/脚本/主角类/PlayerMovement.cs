
using System.Collections;
using UnityEngine;

namespace Resource.脚本.主角类
{
	public class PlayerMovement : MonoBehaviour
	{
		
		[SerializeField] private float _speed;
		[SerializeField] private float backSpeed = 3f;
		[SerializeField] private float backTime = 0.5f;
		private Rigidbody2D _rb;
		private Vector2 _direction;
		private InputControl _inputControl;
		private SpriteRenderer _spriteRenderer;
		private PlayerControl _player; 

		private void OnEnable()
		{
			_inputControl.Enable();
		}

		private void OnDisable()
		{
			_inputControl.Disable();
		}

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
			_inputControl = new InputControl();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_player = GetComponent<PlayerControl>(); // 获取 PlayerControl
		}

		private void Update()
		{
			图片翻转();
		}

		private void FixedUpdate()
		{
			if (!_player.isHurt) // 只有未受伤时才移动
			{
				Move();
			}
		}

		private void Move()
		{
			_direction = _inputControl.Player.Move.ReadValue<Vector2>();
			_rb.velocity = _direction * _speed;
		}

		public void PlayerBack(Vector3 direction)
		{
			StartCoroutine(BackRoutine(direction));
		}
		private IEnumerator BackRoutine(Vector3 direction)
		{
			var targetDirection = (transform.position - direction).normalized;
			_rb.velocity = targetDirection * backSpeed;
			yield return new WaitForSeconds(backTime); // 后退持续 0.2 秒
			_rb.velocity = Vector2.zero; // 停止后退
		}

		private void 图片翻转()
		{
			if (_direction.x < 0)
			{
				_spriteRenderer.flipX = true;
			}
			else
			{
				_spriteRenderer.flipX = false;
			}
		}
	}
	}

