﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Linq;
using System.Linq.Expressions;

namespace NHibernate.Linq
{
	using System.Threading.Tasks;
	using System.Threading;
	/// <content>
	/// Contains generated async methods
	/// </content>
	public static partial class DmlExtensionMethods
	{
		/// <summary>
		/// Delete all entities selected by the specified query. The delete operation is performed in the database without reading the entities out of it.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <param name="source">The query matching the entities to delete.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>The number of deleted entities.</returns>
		public static Task<int> DeleteAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				var provider = source.GetNhProvider();
				return provider.ExecuteDmlAsync<TSource>(QueryMode.Delete, source.Expression, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}

		/// <summary>
		/// Update all entities selected by the specified query. The update operation is performed in the database without reading the entities out of it.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <param name="source">The query matching the entities to update.</param>
		/// <param name="expression">The update setters expressed as a member initialization of updated entities, e.g.
		/// <c>x => new Dog { Name = x.Name, Age = x.Age + 5 }</c>. Unset members are ignored and left untouched.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>The number of updated entities.</returns>
		public static Task<int> UpdateAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource>> expression, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				return ExecuteUpdateAsync(source, DmlExpressionRewriter.PrepareExpression(source.Expression, expression), false, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}

		/// <summary>
		/// Update all entities selected by the specified query, using an anonymous initializer for specifying setters. The update operation is performed
		/// in the database without reading the entities out of it.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <param name="source">The query matching the entities to update.</param>
		/// <param name="expression">The assignments expressed as an anonymous object, e.g.
		/// <c>x => new { Name = x.Name, Age = x.Age + 5 }</c>. Unset members are ignored and left untouched.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>The number of updated entities.</returns>
		public static Task<int> UpdateAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, object>> expression, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				return ExecuteUpdateAsync(source, DmlExpressionRewriter.PrepareExpressionFromAnonymous(source.Expression, expression), false, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}

		/// <summary>
		/// Perform an <c>update versioned</c> on all entities selected by the specified query. The update operation is performed in the database without 
		/// reading the entities out of it.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <param name="source">The query matching the entities to update.</param>
		/// <param name="expression">The update setters expressed as a member initialization of updated entities, e.g.
		/// <c>x => new Dog { Name = x.Name, Age = x.Age + 5 }</c>. Unset members are ignored and left untouched.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>The number of updated entities.</returns>
		public static Task<int> UpdateVersionedAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource>> expression, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				return ExecuteUpdateAsync(source, DmlExpressionRewriter.PrepareExpression(source.Expression, expression), true, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}

		/// <summary>
		/// Perform an <c>update versioned</c> on all entities selected by the specified query, using an anonymous initializer for specifying setters. 
		/// The update operation is performed in the database without reading the entities out of it.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <param name="source">The query matching the entities to update.</param>
		/// <param name="expression">The assignments expressed as an anonymous object, e.g.
		/// <c>x => new { Name = x.Name, Age = x.Age + 5 }</c>. Unset members are ignored and left untouched.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>The number of updated entities.</returns>
		public static Task<int> UpdateVersionedAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, object>> expression, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				return ExecuteUpdateAsync(source, DmlExpressionRewriter.PrepareExpressionFromAnonymous(source.Expression, expression), true, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}

		internal static Task<int> ExecuteUpdateAsync<TSource>(this IQueryable<TSource> source, Expression updateExpression, bool versioned, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				var provider = source.GetNhProvider();
				return provider.ExecuteDmlAsync<TSource>(versioned ? QueryMode.UpdateVersioned : QueryMode.Update, updateExpression, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}

		/// <summary>
		/// Insert all entities selected by the specified query. The insert operation is performed in the database without reading the entities out of it.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TTarget">The type of the entities to insert.</typeparam>
		/// <param name="source">The query matching entities source of the data to insert.</param>
		/// <param name="expression">The expression projecting a source entity to the entity to insert.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>The number of inserted entities.</returns>
		public static Task<int> InsertIntoAsync<TSource, TTarget>(this IQueryable<TSource> source, Expression<Func<TSource, TTarget>> expression, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				return ExecuteInsertAsync<TSource, TTarget>(source, DmlExpressionRewriter.PrepareExpression(source.Expression, expression), cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}

		/// <summary>
		/// Insert all entities selected by the specified query, using an anonymous initializer for specifying setters. <typeparamref name="TTarget"/>
		/// must be explicitly provided, e.g. <c>source.InsertInto&lt;Cat, Dog&gt;(c => new {...})</c>. The insert operation is performed in the
		/// database without reading the entities out of it.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TTarget">The type of the entities to insert. Must be explicitly provided.</typeparam>
		/// <param name="source">The query matching entities source of the data to insert.</param>
		/// <param name="expression">The expression projecting a source entity to an anonymous object representing 
		/// the entity to insert.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>The number of inserted entities.</returns>
		public static Task<int> InsertIntoAsync<TSource, TTarget>(this IQueryable<TSource> source, Expression<Func<TSource, object>> expression, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				return ExecuteInsertAsync<TSource, TTarget>(source, DmlExpressionRewriter.PrepareExpressionFromAnonymous(source.Expression, expression), cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}

		internal static Task<int> ExecuteInsertAsync<TSource, TTarget>(this IQueryable<TSource> source, Expression insertExpression, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			try
			{
				var provider = source.GetNhProvider();
				return provider.ExecuteDmlAsync<TTarget>(QueryMode.Insert, insertExpression, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<int>(ex);
			}
		}
	}
}
