using Authorization;
using Framework;

namespace Client
{
    public class Class1 : Controller
    {
        [RequiredPermissions("Group1", "Open, RunApplication")]
        public string DoIt(Content content)
        {
            return $"{content.Name}: ok.";
        }
    }
}
