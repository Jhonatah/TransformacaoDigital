using System;
using System.Security.Cryptography;
using System.Text;

namespace TransformacaoDigital.Library
{
    public class Encriptador
    {
        private const string _IV = "@(LJlj19)&djA&*A";

        public Encriptador()
        {
        }

        private AesManaged _aes;

        public static Encriptador Get() => new Encriptador();


        private void _ConfigurarAES(string key)
        {
            if (_aes == null) _aes = new AesManaged();

            _aes.Mode = CipherMode.CBC;
            _aes.BlockSize = 128;
            _aes.KeySize = 256;
            _aes.IV = Encoding.UTF8.GetBytes(_IV);
            _aes.Key = Encoding.UTF8.GetBytes(key);
            _aes.Padding = PaddingMode.PKCS7;
        }

        private string ConverterArrayParaString(byte[] bytes)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));

            return builder.ToString();
        }


        public string Decriptar(string key, string mensagem)
        {
            byte[] dencrypted;
            byte[] mensagemBytes = Convert.FromBase64String(mensagem);
            _ConfigurarAES(key);

            ICryptoTransform encryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            dencrypted = encryptor.TransformFinalBlock(mensagemBytes, 0, mensagemBytes.Length);

            return Encoding.UTF8.GetString(dencrypted);
        }

        public string Encriptar(string key, string mensagem)
        {
            byte[] encrypted;
            byte[] mensagemBytes = Encoding.UTF8.GetBytes(mensagem);
            _ConfigurarAES(key);

            ICryptoTransform encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);
            encrypted = encryptor.TransformFinalBlock(mensagemBytes, 0, mensagemBytes.Length);

            return Convert.ToBase64String(encrypted);
        }

        public string EncriptarSemReversao(string mensagem)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(mensagem));

                return ConverterArrayParaString(bytes);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_aes != null)
                    {
                        _aes.Dispose();
                        _aes = null;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
