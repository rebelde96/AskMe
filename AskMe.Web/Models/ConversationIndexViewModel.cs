using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
    public class ConversationIndexViewModel
    {
        public ConversationIndexViewModel()
        {
            this.Conversations = new List<ConversationViewModel>();
        }

        public ICollection<ConversationViewModel> Conversations { get; set; }
    }
}
