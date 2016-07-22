using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using dotless.Core.Input;

namespace dotless.Core.Remote
{
	public class RemoteReader : IFileReader
	{
		public bool UseCacheDependencies
		{
			get
			{
				return true;
			}
		}

		public bool DoesFileExist(string fileName)
		{
			return true; //don't waste a trip, just make it error out
		}

		public byte[] GetBinaryFileContents(string url)
		{
			try
			{
				WebClient wc = new WebClient();
				wc.BaseAddress = url;
				return wc.DownloadData(url);
			}
			catch
			{
				throw;
			}
		}

		public string GetFileContents(string url)
		{
			try
			{
				WebClient wc = new WebClient();
				wc.BaseAddress = url;
				return wc.DownloadString(url);
			}
			catch
			{
				throw;
			}
		}
	}
}
