using System.Collections.Generic;

namespace determining_dna_health
{
	public class Vertex
	{
		public bool IsGen { get; private set; }

		public readonly char Symbol;

		public Vertex Parent;

		public Vertex SuffLink;

		public Vertex GoodSuffLink;

		private SortedDictionary<char, Vertex> _nextVertex;

		private SortedDictionary<char, Vertex> _autoMove;

		private List<Pattern> Gens;

		public Vertex(Vertex parent, char symbol)
		{
			Parent = parent;
			Symbol = symbol;
		}

		public bool NextVertexExist(char ch) => _nextVertex is object && _nextVertex.ContainsKey(ch);

		public Vertex NextVertex(char ch) => _nextVertex[ch];

		public void AddNextVertex(Vertex nextVertex)
		{
			if (_nextVertex is null)
				_nextVertex = new SortedDictionary<char, Vertex>();

			_nextVertex.Add(nextVertex.Symbol, nextVertex);
		}

		public Vertex AddGen(uint number, int cost)
		{
			IsGen = true;

			if (Gens is null)
			{
				Gens = new List<Pattern>(1);
			}

			Gens.Add(new Pattern(number, cost));
			return this;
		}

		public bool AutoMoveExist(char ch) => _autoMove is object && _autoMove.ContainsKey(ch);

		public Vertex AutoMove(char ch) => _autoMove[ch];

		public void AddAutoMove(Vertex nextVertex) => AddAutoMove(nextVertex, nextVertex.Symbol);

		public void AddAutoMove(Vertex nextVertex, char symbol)
		{
			if (_autoMove is null)
				_autoMove = new SortedDictionary<char, Vertex>();

			_autoMove.Add(symbol, nextVertex);
		}

		public long GetCost(uint from, uint to)
		{
			if (Gens is null)
			{
				return 0;
			}

			long totalCost = 0;
			foreach (var gen in Gens)
			{
				if (gen.PatternNumber >= from && gen.PatternNumber <= to)
				{
					totalCost += gen.Cost;
				}
			}

			return totalCost;
		}
	}
}
