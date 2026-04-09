using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyukiSone
{
	public interface IP_MiyukiLocalizable
	{
		string Key { get; set; }
		string Type { get; set; }
		string English { get; set; }
		string Korean { get; set; }
		string Japanese { get; set; }
		string Chinese { get; set; }
		string Chinese_TW { get; set; }
		string AudioFile { get; set; }
	}
}
