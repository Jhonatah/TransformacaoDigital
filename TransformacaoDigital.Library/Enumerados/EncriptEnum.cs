using System;

namespace TransformacaoDigital.Library.Enumerados
{
    public enum EncriptEnum
    {
        /// <summary>
        ///     Criptografia para Login
        /// </summary>
        c7b70d19b7db400c84de2b570b49c1fd,

        /// <summary>
        ///     Criptografia para Token
        /// </summary>
        ea16acb359604973ba2b17498ba2d8dc,

        /// <summary>
        ///     Criptografia para alteração de senhas
        /// </summary>
        d8a49e8e450842848509f40ff9b3851b
    }

    public static class EnumExtension
    {
        public static string GetName(this EncriptEnum queueEnum)
            => Enum.GetName(typeof(EncriptEnum), queueEnum);
    }

}
