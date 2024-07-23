

namespace SaveLoaderProject
{
    public class Encrypter
    {

        public string EncryptDecrypt(string data)
        {
            if (string.IsNullOrEmpty(data)) return null;

            string result = "";

            for(int i = 0; i < data.Length; i++)
            {
                result += (char)(data[i] ^ ConstVars.keyWord[i % ConstVars.keyWord.Length]);
            }

            return result;
        }
    }

}
