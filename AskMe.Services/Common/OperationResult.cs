using System.Collections.Generic;

namespace AskMe.Services.Common
{
	public class OperationResult
	{
		public OperationResult()
		{
			this.ErrorMessages = new List<string>();
			this.WarningMessages = new List<string>();
			this.SuccessMessages = new List<string>();
			this.IsSuccessfull = true;
		}

		public bool IsSuccessfull { get; set; }

		public IList<string> ErrorMessages { get; set; }

		public IList<string> WarningMessages { get; set; }

		public IList<string> SuccessMessages { get; set; }
	}
}
