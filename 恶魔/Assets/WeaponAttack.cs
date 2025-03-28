using Resource.脚本.管理类;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int AttackPressed = Animator.StringToHash("attackPressed");
    private Animator _anim;
    private bool _isAttacking;

    [SerializeField] private Transform 刀光位置;
    [SerializeField] private GameObject 刀光1预制体;
    [SerializeField] private GameObject 刀光2预制体;
    
    private GameObject _刀光1实例;
    private GameObject _刀光2实例;
    private AudioController _audioController;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _audioController = GameObject.FindWithTag("Audio").GetComponent<AudioController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isAttacking)
            {
                StartAttack();
            }
            else
            {
                ContinueCombo();
            }
        }
    }

    private void StartAttack()
    {
        _isAttacking = true;
        _anim.SetBool(IsAttacking, true);
    }

    private void ContinueCombo()
    {
        _anim.SetBool(AttackPressed, true);
    }

    #region 攻击动画事件调用函数

    // 动画事件调用：
    /// <summary>
    /// 攻击动画事件 启用连击窗口
    /// </summary>
    public void EnableCombo()
    {
        _anim.SetBool(AttackPressed, false); // 重置
    }

    
    /// <summary>
    /// 攻击动画事件 攻击结束
    /// </summary>
    public void EndAttack()
    {
        _isAttacking = false;
        _anim.SetBool(IsAttacking, false);
        _anim.SetBool(AttackPressed, false);
    }

    /// <summary>
    /// 攻击动画事件  生成刀光1
    /// </summary>
    public void 生成刀光1预制体()
    {
        if (_刀光1实例 == null) // 防止重复生成
        {
            _刀光1实例 = Instantiate(刀光1预制体, 刀光位置);
        }
    }

    
    /// <summary>
    /// 攻击动画事件  生成刀光2
    /// </summary>
    public void 生成刀光2预制体()
    {
        if (_刀光2实例 == null)
        {
            _刀光2实例 = Instantiate(刀光2预制体, 刀光位置);
        }
    }

    /// <summary>
    /// 攻击动画事件  销毁刀光1
    /// </summary>
    public void 销毁刀光1预制体()
    {
        if (_刀光1实例 != null)
        {
            Destroy(_刀光1实例);
            _刀光1实例 = null;
        }
    }

    
    /// <summary>
    /// 攻击动画事件  销毁刀光2
    /// </summary>
    public void 销毁刀光2预制体()
    {
        if (_刀光2实例 != null)
        {
            Destroy(_刀光2实例);
            _刀光2实例 = null;
        }
    }

    
    /// <summary>
    /// 攻击动画事件  销毁所有刀光
    /// </summary>
    public void 销毁所有刀光()
    {
        if (_刀光1实例 != null)
        {
            Destroy(_刀光1实例);
            _刀光1实例 = null;
        }

        if (_刀光2实例 != null)
        {
            Destroy(_刀光2实例);
            _刀光2实例 = null;
        }
    }
/// <summary>
/// 攻击动画事件
/// </summary>
    public void PlayAttackAudio1()
    {
        _audioController.PlayAttackAudio(_audioController.小剑攻击音效1);
    }
    /// <summary>
    /// 攻击动画事件
    /// </summary>
    public void PlayAttackAudio2()
    {
        _audioController.PlayAttackAudio(_audioController.小剑攻击音效2);
    }
    #endregion
}
