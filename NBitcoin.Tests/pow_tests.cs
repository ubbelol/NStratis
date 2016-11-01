using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NBitcoin.Tests
{
	using NBitcoin.BitcoinCore;

	public class pow_tests
	{

		[Fact]
		[Trait("UnitTest", "UnitTest")]
		public static void CanCalculatePowCorrectly()
		{
			var store = new BlockStore(@"C:\StratisData", Network.Main);
			ConcurrentChain chain = store.GetChain();
			//EnsureDownloaded("main.headers.dat", "https://bytyng.bn1302.livefilestore.com/y3miHC-eZsNz89GuLViA3-8dTT5BmYBgq6H1VdmtDAhv0XQpaWoBh85lPDEswbgP_kqLXKLJSIGrO1SpyzFszbmXqL5EhcwcYKf6WM9St9Z_wOIx38sC4NfEQKjI8QzwNdr5sNnyI_OxVHWiee14-H5rw/main.headers.dat?download&psid=1");
			//chain.Load(File.ReadAllBytes("main.headers.dat"));
			foreach(var block in chain.EnumerateAfter(chain.Genesis))
			{
				var thisWork = block.GetWorkRequired(Network.Main);
				var thisWork2 = block.Previous.GetNextWorkRequired(Network.Main);
				Assert.Equal(thisWork, thisWork2);
				Assert.True(block.CheckPowPosAndTarget(Network.Main));
			}
		}

		private static void EnsureDownloaded(string file, string url)
		{
			if(File.Exists(file))
				return;
			HttpClient client = new HttpClient();
			var data = client.GetByteArrayAsync(url).GetAwaiter().GetResult();
			File.WriteAllBytes(file, data);
		}
	}
}
