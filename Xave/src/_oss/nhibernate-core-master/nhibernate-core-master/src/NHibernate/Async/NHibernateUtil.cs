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
using NHibernate.Collection;
using NHibernate.Impl;
using NHibernate.Intercept;
using NHibernate.Proxy;
using NHibernate.Type;
using NHibernate.UserTypes;
using NHibernate.Util;

namespace NHibernate
{
	using System.Collections.Generic;
	using System.Reflection;
	using System.Threading.Tasks;
	using System.Threading;

	public static partial class NHibernateUtil
	{


		/// <summary>
		/// Force initialization of a proxy or persistent collection.
		/// </summary>
		/// <param name="proxy">a persistable object, proxy, persistent collection or null</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <exception cref="HibernateException">if we can't initialize the proxy at this time, eg. the Session was closed</exception>
		public static Task InitializeAsync(object proxy, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<object>(cancellationToken);
			}
			try
			{
				if (proxy == null)
				{
					return Task.CompletedTask;
				}
				else if (proxy.IsProxy())
				{
					return ((INHibernateProxy)proxy).HibernateLazyInitializer.InitializeAsync(cancellationToken);
				}
				else if (proxy is IPersistentCollection)
				{
					return ((IPersistentCollection)proxy).ForceInitializationAsync(cancellationToken);
				}
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		/// <summary>
		/// Get the true, underlying class of a proxied persistent class. This operation
		/// will initialize a proxy by side-effect.
		/// </summary>
		/// <param name="proxy">a persistable object or proxy</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>the true class of the instance</returns>
		public static async Task<System.Type> GetClassAsync(object proxy, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (proxy.IsProxy())
			{
				return (await (((INHibernateProxy)proxy).HibernateLazyInitializer.GetImplementationAsync(cancellationToken)).ConfigureAwait(false)).GetType();
			}
			else
			{
				return proxy.GetType();
			}
		}
	}
}
