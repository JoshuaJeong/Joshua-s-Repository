﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Text;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1521
{
	using System.Threading.Tasks;
	using System.Threading;
	[TestFixture]
	public class FixtureAsync
	{
		private static void CheckDialect(Configuration configuration)
		{
			var dialect = Dialect.Dialect.GetDialect(configuration.Properties);
			if (!(dialect is MsSql2000Dialect))
				Assert.Ignore("Specific test for MsSQL dialects");
		}

		private static Task AssertThatCheckOnTableExistenceIsCorrectAsync(Configuration configuration, CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				var su = new SchemaExport(configuration);
				var sb = new StringBuilder(500);
				su.Execute(x => sb.AppendLine(x), false, false);
				string script = sb.ToString();
				Assert.That(script, Does.Contain("if exists (select * from dbo.sysobjects where id = object_id(N'nhibernate.dbo.Aclass') and OBJECTPROPERTY(id, N'IsUserTable') = 1)"));
				return Task.CompletedTask;
			}
			catch (System.Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task TestForClassWithDefaultSchemaAsync()
		{
			try
			{
				Configuration cfg = TestConfigurationHelper.GetDefaultConfiguration();
				CheckDialect(cfg);
				cfg.AddResource("NHibernate.Test.NHSpecificTest.NH1521.AclassWithNothing.hbm.xml", GetType().Assembly);
				cfg.SetProperty(Environment.DefaultCatalog, "nhibernate");
				cfg.SetProperty(Environment.DefaultSchema, "dbo");
				return AssertThatCheckOnTableExistenceIsCorrectAsync(cfg);
			}
			catch (System.Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task WithDefaultValuesInMappingAsync()
		{
			try
			{
				Configuration cfg = TestConfigurationHelper.GetDefaultConfiguration();
				CheckDialect(cfg);
				cfg.AddResource("NHibernate.Test.NHSpecificTest.NH1521.AclassWithDefault.hbm.xml", GetType().Assembly);
				return AssertThatCheckOnTableExistenceIsCorrectAsync(cfg);
			}
			catch (System.Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task WithSpecificValuesInMappingAsync()
		{
			try
			{
				Configuration cfg = TestConfigurationHelper.GetDefaultConfiguration();
				CheckDialect(cfg);
				cfg.AddResource("NHibernate.Test.NHSpecificTest.NH1521.AclassWithSpecific.hbm.xml", GetType().Assembly);
				return AssertThatCheckOnTableExistenceIsCorrectAsync(cfg);
			}
			catch (System.Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task WithDefaultValuesInConfigurationPriorityToMappingAsync()
		{
			try
			{
				Configuration cfg = TestConfigurationHelper.GetDefaultConfiguration();
				CheckDialect(cfg);
				cfg.AddResource("NHibernate.Test.NHSpecificTest.NH1521.AclassWithDefault.hbm.xml", GetType().Assembly);
				cfg.SetProperty(Environment.DefaultCatalog, "somethingDifferent");
				cfg.SetProperty(Environment.DefaultSchema, "somethingDifferent");
				return AssertThatCheckOnTableExistenceIsCorrectAsync(cfg);
			}
			catch (System.Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}
	}
}
