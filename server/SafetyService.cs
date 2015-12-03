using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace server
{
	public class SafetyService:Service
	{
		/// <summary>
		/// 使用DES算法进行加密
		/// </summary>
		/// <param name="Data"></param>
		/// <param name="Key"></param>
		/// <param name="IV"></param>
		/// <returns></returns>
		static String encrypt( String Data,  byte[] Key, byte[] IV )
		{
			try
			{
				MemoryStream ms = new MemoryStream();
				CryptoStream cStream = new CryptoStream(ms,new DESCryptoServiceProvider().CreateEncryptor( Key, IV ),
					CryptoStreamMode.Write );// 使用内存流和秘钥、向量创建一个复制流				
				StreamWriter sWriter = new StreamWriter( cStream );// 创建流写入器											   
				sWriter.Write( Data );//写入欲加密数据
				sWriter.Close();
				cStream.Close();
				byte[] buffer = ms.ToArray();
				ms.Close();
				return Encoding.Unicode.GetString(buffer);
			}
			catch ( CryptographicException e )
			{
				Console.WriteLine( "A Cryptographic error occurred: {0}", e.Message );
			}
			catch ( UnauthorizedAccessException e )
			{
				Console.WriteLine( "A file error occurred: {0}", e.Message );
			}
			return null;
		}
		/// <summary>
		/// 使用DES算法解密
		/// </summary>
		/// <param name="FileName"></param>
		/// <param name="Key"></param>
		/// <param name="IV"></param>
		/// <returns></returns>
		static String decrypt( String FileName, byte[] Key, byte[] IV )
		{
			try
			{
				MemoryStream ms = new MemoryStream();
				CryptoStream cStream = new CryptoStream( ms,
					new DESCryptoServiceProvider().CreateDecryptor( Key, IV ),
					CryptoStreamMode.Read );
				StreamReader sReader = new StreamReader( cStream );
				String val = sReader.ReadToEnd();
				sReader.Close();
				cStream.Close();
				ms.Close();
				return val;
			}
			catch ( CryptographicException e )
			{
				Console.WriteLine( "A Cryptographic error occurred: {0}", e.Message );
			}
			catch ( UnauthorizedAccessException e )
			{
				Console.WriteLine( "A file error occurred: {0}", e.Message );
			}
			return null;
		}
	}
}
