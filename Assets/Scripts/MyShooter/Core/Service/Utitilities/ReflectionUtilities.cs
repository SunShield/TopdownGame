using System.Reflection;

namespace MyShooter.Core.Service.Utitilities
{
	public static class ReflectionUtilities
	{
		public static bool IsOverride(MethodInfo method)
		{
			return !method.Equals(method.GetBaseDefinition());
		}
	}
}
