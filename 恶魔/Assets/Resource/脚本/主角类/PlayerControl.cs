
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


namespace Resource.脚本.主角类
{
    public class PlayerControl : MonoBehaviour
    {
        private Animator _anim;
        private PlayerMovement _movement;
        private Rigidbody2D _rb;
        [FormerlySerializedAs("_isHurt")] public bool isHurt;
        private float _hurtTime = 1f;
        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _anim = GetComponent<Animator>();
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Hurt();
                //后退
                _movement.PlayerBack(other.gameObject.transform.position);
            }
        }

        /// <summary>
        /// 受伤动画方法
        /// </summary>
        private void Hurt()
        {
            print("收到伤害 播放动画");
           // _anim.Play("Hurt");
            isHurt = true;
           StartCoroutine(WaitHurtAnimation());
        }

        private IEnumerator WaitHurtAnimation()
        {
           yield return new WaitForSeconds(_hurtTime);
           isHurt = false;
        }
    }
}
