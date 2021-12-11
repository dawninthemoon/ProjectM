using UnityEngine;

namespace UI
{
    public class UIBehaviourFollow : UIBehaviour
    {
        private Transform cameraTrans;

        protected override void Awake()
        {
            base.Awake();

            cameraTrans = Camera.main.transform;
        }

        private void Update()
        {
            Vector3 dir = cameraTrans.position - transform.position;
            dir.Normalize();

            Quaternion look = Quaternion.LookRotation(dir);

            Quaternion result = Quaternion.Euler(-look.eulerAngles.x, 0f, 0f);

            transform.rotation = result;
        }
    }
}
