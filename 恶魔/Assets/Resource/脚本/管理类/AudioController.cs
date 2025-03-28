using UnityEngine;

namespace Resource.脚本.管理类
{
	public class AudioController : MonoBehaviour
	{
		public static AudioController Instance { get; private set; }  //  **单例实例**
        
		[SerializeField] private AudioSource bgm;
		[SerializeField] private AudioSource attackAudio;
		public AudioClip 小剑攻击音效1;
		public AudioClip 小剑攻击音效2;
		public AudioClip slimeAttackAudio;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);  // ✅ **确保切换场景不会销毁**
			}
			else
			{
				Destroy(gameObject);  // ✅ **防止场景中出现多个 AudioController**
			}
		}

		public void PlayAttackAudio(AudioClip au)
		{
			attackAudio.PlayOneShot(au);
		}
	}
}