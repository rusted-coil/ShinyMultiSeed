using System.Text.Json;

namespace ShinyMultiSeed.Infrastructure
{
    public static class Serializer
    {
        /// <summary>
        /// 指定したパスからファイルを読み込み、T型のデータを返します。
        /// <para> * 読み込みに失敗した場合は単にnewしたものを返します。</para>
        /// </summary>
        public static T Deserialize<T>(string filePath) where T : new()
        {
            string content;
            try
            {
                content = File.ReadAllText(filePath);
            }
            catch (Exception)
            {
                return new T();
            }

            var nullable = JsonSerializer.Deserialize<T>(content);
            if (nullable != null)
            {
                return nullable;
            }
            return new T();
        }

        /// <summary>
        /// 指定したパスにデータを保存します。
        /// <para> * 保存に成功した場合はtrueを返し、失敗した場合はfalseを返してerrorMessageにエラーメッセージを格納します。</para>
        /// </summary>
        public static bool Serialize<T>(string filePath, T data, out string errorMessage)
        {
            string content = JsonSerializer.Serialize(data);

            try
            {
                var directoryName = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                File.WriteAllText(filePath, content);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
