
using UnityEngine;

namespace Resource.脚本.主角类
{
    public class WeaponRotation : MonoBehaviour
    {
        [SerializeField] private float rotationOffset = -90f; // 默认朝上时偏移 -90°
        private Camera _cam;


        private void Awake()
        {
            _cam = Camera.main;

        }

        private void Update()
        {
            RotateToMouse();
        }

        public void RotateToMouse()
        {
            // 获取鼠标在屏幕上的位置，并转换为世界坐标
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;

            // 计算从当前物体到鼠标的方向向量
            Vector3 direction = (mouseWorldPos - transform.position).normalized;

            // 计算角度并加上偏移（注意：如果默认朝上则需要 -90°）
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;

            // 构造目标旋转
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = targetRotation;
        }
        /// <summary>
        /// 恢复初始旋转角度
        /// 
        /// </summary>
        public void CurrentRotate()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
