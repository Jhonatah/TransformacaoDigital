using System;

namespace TransformacaoDigital.Library.Enumerados
{
    public enum EncriptEnum
    {
        c7b70d19b7db400c84de2b570b49c1fd
    }

    public static class EnumExtension
    {
        public static string GetName(this EncriptEnum queueEnum)
            => Enum.GetName(typeof(EncriptEnum), queueEnum);
    }

}
