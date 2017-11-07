﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using NHibernate.Dialect;
using NHibernate.SqlCommand;
using NHibernate.Type;
using NUnit.Framework;

namespace NHibernate.Test.DialectTest
{
	using System.Threading.Tasks;
	/// <summary>
	/// Summary description for MsSqlDialectFixture.
	/// </summary>
	[TestFixture]
	public class MsSqlDialectFixtureAsync : DialectFixtureAsync
	{
		[SetUp]
		public override void SetUp()
		{
			// Generic Dialect inherits all of the Quoting functions from
			// Dialect (which is abstract)
			d = new MsSql2000Dialect();
			tableWithNothingToBeQuoted = new string[] {"plainname", "[plainname]"};
			tableAlreadyQuoted = new string[] {"[Quote[d[Na]]$`]", "[Quote[d[Na]]$`]", "Quote[d[Na]$`"};
			tableThatNeedsToBeQuoted = new string[] {"Quote[d[Na]$`", "[Quote[d[Na]]$`]", "Quote[d[Na]$`"};
		}
	}
}