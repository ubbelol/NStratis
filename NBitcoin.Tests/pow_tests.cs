using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
			//EnsureDownloaded(@"download\blocks\blk0001.dat", "https://n5uqoa.bn1302.livefilestore.com/y3mPnoFPfm1JaRLEnsmhCXceQD9E_udAEL6scZ9NaI4AApzCMOyZlRtkKAGxGqmL-0OkjdadX_odPnn70izPPrDQd9lAh20q0_sbASCyn83U_it_niIR5IC7lKMFeLVP5rk9NPfgYcubzkDly3C9cJzkA/blk0001.dat?download&psid=1");
			//var store = new BlockStore(@"download\blocks", Network.Main);
			//ConcurrentChain chain = store.GetChain();

			var chain = new ConcurrentChain(File.ReadAllBytes(@"C:\StratisData\Headers.dat"));

			foreach (var block in chain.EnumerateAfter(chain.Genesis))
			{
				Assert.True(block.CheckPowPosAndTarget(Network.Main));
			}
		}

		private static void EnsureDownloaded(string file, string url)
		{
			// todo: move this to a common area
			if(File.Exists(file))
				return;

			if (!Directory.Exists(Path.GetDirectoryName(file)))
				Directory.CreateDirectory(Path.GetDirectoryName(file));
			
			WebClient client = new WebClient();
			client.DownloadFile(url, file);
		}
	}
}
