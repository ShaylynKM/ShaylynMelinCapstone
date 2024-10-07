using UnityEngine;

namespace _PROJECT._2_Scripts.SeanCodeRefs.Projectile
{
    public class WigglyMovementStrategy : MovementStrategy
    {
        [SerializeField]protected float _wiggleSpeed=2f;
        [SerializeField] protected float _wiggleHeight = 0.02f;

        public override void Initialize(Vector3 position, GameObject target)
        {
            base.Initialize(position, target); //could be made
        }

        protected override void Update()
        {
            Vector3 bulletPropulsion = new Vector3(this._direction.x * _speed, this._direction.y * _speed, 0f) * Time.deltaTime; // Horizontal movement

            float wiggleStagger = Mathf.Sin(Time.time * _wiggleSpeed) * _wiggleHeight; // Vertical movement

            transform.position += bulletPropulsion + new Vector3(0f, wiggleStagger, 0f); // Add them together
        }
    }
}