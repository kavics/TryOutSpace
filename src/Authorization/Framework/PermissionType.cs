using System.Linq;

namespace Framework
{
    public class PermissionType
    {
        public static PermissionType See = new PermissionType(nameof(See));
        public static PermissionType Open = new PermissionType(nameof(Open));
        public static PermissionType Save = new PermissionType(nameof(Save));
        public static PermissionType RunApplication = new PermissionType(nameof(RunApplication));

        public static PermissionType[] All = new[] { See, Open, Save, RunApplication };

        public string Name { get; }
        private PermissionType(string name)
        {
            Name = name;
        }

        public static PermissionType GetByName(string name)
        {
            return All.FirstOrDefault(x => x.Name == name);
        }
    }
}
