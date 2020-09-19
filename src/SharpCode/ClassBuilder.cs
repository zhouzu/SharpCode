using System.Linq;
using Optional;

namespace SharpCode
{
    public class ClassBuilder
    {
        private readonly Class _class = new Class();

        internal ClassBuilder() { }

        internal ClassBuilder(AccessModifier accessModifier, string name)
        {
            _class.AccessModifier = accessModifier;
            _class.Name = name;
        }

        public ClassBuilder WithNamespace(string @namespace)
        {
            _class.Namespace = @namespace;
            return this;
        }

        public ClassBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            _class.AccessModifier = accessModifier;
            return this;
        }

        public ClassBuilder WithName(string name)
        {
            _class.Name = name;
            return this;
        }

        public ClassBuilder WithInheritedClass(string name)
        {
            _class.InheritedClass = Option.Some(name);
            return this;
        }

        public ClassBuilder WithImplementedInterface(string name)
        {
            _class.ImplementedInterfaces.Add(name);
            return this;
        }

        public ClassBuilder WithField(FieldBuilder builder)
        {
            _class.Fields.Add(builder.Build());
            return this;
        }

        public ClassBuilder WithProperty(PropertyBuilder builder)
        {
            _class.Properties.Add(builder.Build());
            return this;
        }

        public ClassBuilder WithConstructor(ConstructorBuilder builder)
        {
            _class.Constructors.Add(builder.Build());
            return this;
        }

        public string ToSourceCode(bool formatted = true)
        {
            return Build().ToSourceCode(formatted);
        }

        public override string ToString()
        {
            return ToSourceCode();
        }

        internal Class Build()
        {
            _class.Namespace ??= "Generated";
            _class.Constructors.ForEach(ctor => ctor.ClassName = _class.Name ?? string.Empty);
            return _class;
        }
    }
}
