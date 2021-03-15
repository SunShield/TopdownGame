namespace MyShooter.Core.Entities.States.Modified
{
	public class ValueModifierStack
	{
		public int Id; // to tell this stack from other ones
		public float InnerValue;
		public int TimeStamp;

		public ValueModifierStack(float innerValue, int stackId)
		{
			InnerValue = innerValue;
			Id = stackId;
		}
	}
}
