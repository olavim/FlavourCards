using System;

namespace VanillaFlavour
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class CardAttribute : Attribute
	{
	}
}