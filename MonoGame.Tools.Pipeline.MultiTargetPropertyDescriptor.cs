    public class MultiTargetPropertyDescriptor : PropertyDescriptor
    {
        private readonly Type _propertyType;
        private readonly Type _componentType;        
        private readonly object[] _targets;
        private readonly PropertyDescriptor _property;

        public MultiTargetPropertyDescriptor(string propertyName, Type propertyType, Type componentType, PropertyDescriptor property, object[] targets)
            : base(propertyName, new Attribute[] { })
        {
            _propertyType = propertyType;
            _componentType = componentType;
            _targets = targets;
            _property = property;
        }

        public override object GetValue(object component)
        {
            var val = _property.GetValue(_targets[0]);
            for (var i = 1; i < _targets.Length; i++)
            {
                var v = _property.GetValue(_targets[i]);
                if (!v.Equals(val))
                    return null;
            }

            return val;                
        }

        public override void SetValue(object component, object value)
        {
            for (var i = 0; i < _targets.Length; i++)
                _property.SetValue(_targets[i], value);         
        }

        public override bool CanResetValue(object component) { return true; }
        public override Type ComponentType { get { return _componentType; } }
        public override bool IsReadOnly { get { return false; } }
        public override Type PropertyType { get { return _propertyType; } }
        public override void ResetValue(object component) { SetValue(component, null); }
        public override bool ShouldSerializeValue(object component) { return true; }            
    }