using Helper;
using UnityEngine;


namespace Model
{
    public class CharacterModel : BaseObjectScene
    {
        #region Fields

        [HideInInspector] public Collider Collider { get; private set; }

        [SerializeField] private float _maxHeals = 100.0f;
        [SerializeField] private float _heals;

        public float Speed = 0.3f;
        public float JumpForce = 10.0f;
        public Vector3 MaxVelocity = new Vector3(1, 100, 1);

        public LayerMask GroundLayer = 1; // 1 == "Default" защита от дурака

        #endregion


        #region Properties

        public bool IsGrounded
        {
            get
            {//todo сделать проверку нахождения на земле
                return true;
                return Physics.CheckCapsule(Collider.bounds.center, Collider.bounds.size, GroundLayer);
                
                var bottomCenterPoint = new Vector3(Collider.bounds.center.x, Collider.bounds.min.y,
                    Collider.bounds.center.z);
                
                //создаем невидимую физическую капсулу и проверяем не пересекает ли она обьект который относится к полу
                
                //_collider.bounds.size.x / 2 * 0.9f -- эта странная конструкция берет радиус обьекта.
                // был бы обязательно сферой -- брался бы радиус напрямую, а так пишем по-универсальнее
                
                return Physics.CheckCapsule(Collider.bounds.center, bottomCenterPoint,
                    Collider.bounds.size.x / 2 * 0.9f, GroundLayer);
                // если можно будет прыгать в воздухе, то нужно будет изменить коэфициент 0.9 на меньший.
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            Collider = GetComponent<Collider>();

            Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _heals = _maxHeals;

            //  Защита от дурака
            if (GroundLayer == gameObject.layer)
                Debug.LogError("Player SortingLayer must be different from Ground SortingLayer!");
        }

        #endregion

        #region Methods

        public void AddHeals(float valuePoint)
        {
            _heals = +valuePoint;
            if (_heals > _maxHeals)
            {
                _heals = _maxHeals;
            }
        }

        public float GetHeals()
        {
            return _heals;
        }

        #endregion

        public void NormalizeVelocity()
        {
            var flag = false;
            var velocity = Rigidbody.velocity;

            while (
                Mathf.Abs(velocity.x) > MaxVelocity.x ||
                Mathf.Abs(velocity.y) > MaxVelocity.y ||
                Mathf.Abs(velocity.z) > MaxVelocity.z)
            {
                flag = true;
                velocity *= 0.9f;
            }

            if (flag)
            {
                Rigidbody.velocity = velocity;
            }
        }
    }
}