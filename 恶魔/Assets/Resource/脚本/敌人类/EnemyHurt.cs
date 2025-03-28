using UnityEngine;

namespace Resource.脚本.敌人类
{
	public class EnemyHurt : MonoBehaviour
	{
		private Animator _anim;
		
		private void Awake()
		{
			_anim = GetComponent<Animator>();
		}

		public void TakeDamage()
		{
			if (_anim != null)
			{
				_anim.Play("Hurt"); // 播放受伤动画
			}
			// 可以在这里添加其他逻辑，如减少生命值
		}
	}
}