﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1904
{
	using System.Threading.Tasks;
	[TestFixture]
	public class StructFixtureAsync : BugTestCase
	{
		protected override IList Mappings =>
			new string[]
			{
				"NHSpecificTest." + BugNumber + ".StructMappings.hbm.xml"
			};

		[Test]
		public async Task ExecuteQueryAsync()
		{
			using (ISession session = OpenSession())
			using (ITransaction transaction = session.BeginTransaction())
			{
				var invoice = new InvoiceWithAddress
				{
					Issued = DateTime.Now,
					BillingAddress = new Address { Line = "84 rue du 22 septembre", City = "Courbevoie", ZipCode = "92400", Country = "France" }
				};
				await (session.SaveAsync(invoice));
				await (transaction.CommitAsync());
			}

			using (ISession session = OpenSession())
			{
				var invoices = await (session.CreateCriteria<Invoice>().ListAsync<Invoice>());
			}
		}

		protected override void OnTearDown()
		{
			base.OnTearDown();
			using (ISession session = OpenSession())
			{
				session.CreateQuery("delete from InvoiceWithAddress").ExecuteUpdate();
				session.Flush();
			}
		}
	}
}
