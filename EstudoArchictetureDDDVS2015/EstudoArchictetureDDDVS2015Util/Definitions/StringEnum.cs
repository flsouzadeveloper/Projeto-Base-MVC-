using EstudoArchictetureDDDVS2015Util.Mensagens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EstudoArchictetureDDDVS2015Util.Definitions
{
    public abstract class StringEnum
    {
        private readonly string _value;

        protected StringEnum(string value)
        {
            if (value == null) throw new ArgumentNullException(string.Format(MensagensGenericas.NaoEPossivelCriarTipo0ComValorNulo, GetType().FullName));

            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is StringEnum)) return false;

            return obj.GetHashCode() == GetHashCode();
        }

        private static FieldInfo GetField(Type type, string value)
        {
            return GetFields(type).FirstOrDefault(f => ((StringEnum)f.GetValue(null))._value == value);
        }

        private static IEnumerable<FieldInfo> GetFields(Type type)
        {
            return type
                .GetFields()
                .Where(w => w.IsPublic && w.IsStatic && w.IsInitOnly)
                .Where(w => w.FieldType == type);
        }

        public virtual string ToDescriptionString()
        {
            var field = GetField(GetType(), _value);

            if (field == null) return _value;

            var description = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();

            return description == null ? _value : description.Description;
        }

        public static object Parse(Type enumType, string value)
        {
            if (!typeof(StringEnum).IsAssignableFrom(enumType))
                throw new FormatException(string.Format(MensagensGenericas.OTipoNaoHerdaDeStringEnumPorIssoNaoPodeSerConvertidoComMetodoParse, enumType.FullName));

            var field = GetField(enumType, value);

            if (field == null) throw new InvalidOperationException(string.Format(MensagensGenericas.OTipo0NaoContemValorValidoPara1, enumType, value));

            return field.GetValue(null);
        }

        public static T Parse<T>(string value) where T : StringEnum
        {
            return (T)Parse(typeof(T), value);
        }

        public static string[] GetNames(Type type)
        {
            return GetFields(type).Select(s => ((StringEnum)s.GetValue(null))._value).ToArray();
        }

        public static T[] GetValues<T>() where T : StringEnum
        {
            return GetFields(typeof(T)).Select(s => (T)s.GetValue(null)).ToArray();
        }

        public static bool operator ==(StringEnum left, StringEnum right)
        {
            if ((object)left == null) return (object)right == null;
            if ((object)right == null) return false;

            if (left.GetType() != right.GetType()) return false;

            return left._value == right._value;
        }

        public static bool operator !=(StringEnum left, StringEnum right)
        {
            return !(left == right);
        }

        public static bool operator ==(string left, StringEnum right)
        {
            if ((object)right == null) return left == null;

            return right._value == left;
        }

        public static bool operator !=(string left, StringEnum right)
        {
            return !(left == right);
        }
    }
}
