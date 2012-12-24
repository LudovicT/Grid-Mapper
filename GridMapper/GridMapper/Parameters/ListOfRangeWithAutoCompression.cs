using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GridMapper
{
	class Item
	{
		public int From { get; private set; }
		public int To { get; private set; }

		public Item( int from, int to )
		{
			From = from;
			To = to;
		}
	}
	public class ListOfRangeOfIntWithAutoCompression : IEnumerable<int>
	{
		readonly List<Item> storage = new List<Item>();

		public void Add( int valueToAdd )
		{
			bool added = false;
			if ( storage.Count > 0 )
			{
				int count;
				do
				{
					count = storage.Count;
					for ( int i = 0; i < storage.Count; i++ )
					{
						Item RangeOfInt = storage[i];
						if ( IsBetween( valueToAdd, RangeOfInt.From, RangeOfInt.To ) ) return;
						if ( IsNextOfFrom( valueToAdd, RangeOfInt.From, RangeOfInt.To ) || IsNextOfFrom( valueToAdd, RangeOfInt.From ) )
						{
							Remove( RangeOfInt.From, RangeOfInt.To );
							Add( valueToAdd, RangeOfInt.To );
							added = true;
						}
						if ( IsNextOfTo( valueToAdd, RangeOfInt.From, RangeOfInt.To ) || IsNextOfTo( valueToAdd, RangeOfInt.To ) )
						{
							Remove( RangeOfInt.From, RangeOfInt.To );
							Add( RangeOfInt.From, valueToAdd );
							added = true;
						}
					}
				} while ( storage.Count != count );
			}
			if ( !added )
			{
				storage.Add( new Item( valueToAdd, valueToAdd ) );
			}
			storage.Sort( ( Item a, Item b ) => a.From.CompareTo( b.From ) );
		}

		public void Add( int from, int to )
		{
			bool added = false;
			if ( storage.Count > 0 )
			{
				int count;
				do
				{
					count = storage.Count;
					for ( int i = 0; i < storage.Count; i++ )
					{
						Item RangeOfInt = storage[i];
						//already in the storage
						// Disposition
						// -----------------		RangeOfInt
						//      --------			from, to
						if ( IsBetween( from, RangeOfInt.From, RangeOfInt.To ) && IsBetween( to, RangeOfInt.From, RangeOfInt.To ) ) return;

						// Disposition
						// ------------				RangeOfInt
						//      --------------		from, to
						if ( IsBetween( from, RangeOfInt.From, RangeOfInt.To ) && !IsBetween( to, RangeOfInt.From, RangeOfInt.To ) )
						{
							Remove( RangeOfInt.From, RangeOfInt.To );
							storage.Add( new Item( RangeOfInt.From, to ) );
							added = true;
						}

						// Disposition
						//      --------------		RangeOfInt
						// ------------				from, to
						if ( !IsBetween( from, RangeOfInt.From, RangeOfInt.To ) && IsBetween( to, RangeOfInt.From, RangeOfInt.To ) )
						{
							Remove( RangeOfInt.From, RangeOfInt.To );
							storage.Add( new Item( from, RangeOfInt.To ) );
							added = true;
						}
						if ( IsNextOfFrom( to, RangeOfInt.From, RangeOfInt.To ) || IsNextOfFrom( to, RangeOfInt.From ) )
						{
							Remove( RangeOfInt.From, RangeOfInt.To );
							Add( from, RangeOfInt.To );
							added = true;
						}
						if ( IsNextOfTo( from, RangeOfInt.From, RangeOfInt.To ) || IsNextOfTo( from, RangeOfInt.To ) )
						{
							Remove( RangeOfInt.From, RangeOfInt.To );
							Add( RangeOfInt.From, to );
							added = true;
						}
					}
				} while ( storage.Count != count );
			}
			if ( !added )
			{
				storage.Add( new Item( from, to ) );
			}
			storage.Sort( ( Item a, Item b ) => a.From.CompareTo( b.From ) );
		}

		public void Remove( int valueToAdd )
		{
			int tmpFrom;
			int tmpTo;

			//exclusive border of the exclusion
			tmpFrom = valueToAdd;
			tmpTo = valueToAdd;
			tmpFrom--;
			tmpTo++;

			if ( storage.Count > 0 )
			{
				int count;
				do
				{
					count = storage.Count;
					for ( int i = 0; i < storage.Count; i++ )
					{
						Item RangeOfInt = storage[i];
						if ( IsBetween( valueToAdd, RangeOfInt.From, RangeOfInt.To ) )
						{
							Remove( RangeOfInt.From, RangeOfInt.To );
							//check if the range is not a single Int
							if ( RangeOfInt.From != RangeOfInt.To && valueToAdd != RangeOfInt.From && valueToAdd != RangeOfInt.To )
							{
								Add( RangeOfInt.From, tmpFrom );
								Add( tmpTo, RangeOfInt.To );
							}
							if ( RangeOfInt.From != RangeOfInt.To && valueToAdd == RangeOfInt.From )
							{
								Add( tmpTo, RangeOfInt.To );
							}
							if ( RangeOfInt.From != RangeOfInt.To && valueToAdd == RangeOfInt.To)
							{
								Add( RangeOfInt.From, tmpFrom );
							}
						}
					}
				} while ( storage.Count != count );
			}
		}

		public void Remove( int from, int to )
		{
			int tmpFrom;
			int tmpTo;

			//for the exclusive exlusion
			tmpFrom = from;
			tmpTo = to;
			tmpFrom--;
			tmpTo++;

			if ( storage.Count > 0 )
			{
				int count;
				do
				{
					count = storage.Count;
					for ( int i = 0; i < storage.Count; i++ )
					{
						Item RangeOfInt = storage[i];
						// Disposition
						// -----------------		RangeOfInt
						//      --------			from, to
						if ( IsBetween( from, RangeOfInt.From, RangeOfInt.To ) && IsBetween( to, RangeOfInt.From, RangeOfInt.To ) )
						{
							storage.Remove( RangeOfInt );
							//if not a full range deletion then add the remaining
							if ( RangeOfInt.From != from && RangeOfInt.To != to )
							{
								Add( RangeOfInt.From, tmpFrom );
								Add( tmpTo, RangeOfInt.To );
							}
							//inclusive left
							if ( RangeOfInt.From == from && RangeOfInt.To != to )
							{
								Add( tmpTo, RangeOfInt.To );
							}
							//inclusive right
							if ( RangeOfInt.From != from && RangeOfInt.To == to )
							{
								Add( RangeOfInt.From, tmpFrom );
							}
						}

						// Disposition
						// ------------				RangeOfInt
						//      --------------		from, to
						if ( IsBetween( from, RangeOfInt.From, RangeOfInt.To ) && !IsBetween( to, RangeOfInt.From, RangeOfInt.To ) )
						{
							storage.Remove( RangeOfInt );
							Add( RangeOfInt.From, tmpFrom );
						}

						// Disposition
						//      --------------		RangeOfInt
						// ------------				from, to
						if ( !IsBetween( from, RangeOfInt.From, RangeOfInt.To ) && IsBetween( to, RangeOfInt.From, RangeOfInt.To ) )
						{
							storage.Remove( RangeOfInt );
							Add( tmpTo, RangeOfInt.To );
						}


						// Disposition
						//		----				RangeOfInt
						//   ---------------		from, to
						if ( IsBetween( RangeOfInt.From, from, to ) && IsBetween( RangeOfInt.To, from, to ) )
						{
							storage.Remove( RangeOfInt );
						}
					}
				} while ( storage.Count != count );
			}
		}


		public IEnumerator<int> GetEnumerator()
		{
			foreach ( Item intRange in storage )
			{
				for ( int i = intRange.From; i <= intRange.To; i++ )
				{
					yield return i;
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		/// <summary>
		/// Return a bool depending if search is between from and to
		/// </summary>
		/// <param name="search">the search</param>
		/// <param name="from">the left side of the range</param>
		/// <param name="to">the right side of the range</param>
		/// <returns>true if search is between from and to</returns>
		bool IsBetween( int search, int from, int to )
		{
			if ( from > to )
			{
				return from >= search && search >= to;
			}
			return from <= search && search <= to;
		}

		// single Int range
		bool IsNextOfFrom( int search, int from, int to )
		{
			return search + 1 == from && search + 1 == to;
		}
		// range of Int
		bool IsNextOfFrom( int search, int from )
		{
			return search + 1 == from;
		}

		// single Int range
		bool IsNextOfTo( int search, int from, int to )
		{
			return search - 1 == from && search - 1 == to;
		}
		//range of Int
		bool IsNextOfTo( int search, int to )
		{
			return search - 1 == to;
		}
	}
}
